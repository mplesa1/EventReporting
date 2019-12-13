using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.DataTransferObjects.Event;
using EventReporting.Shared.Infrastructure.Constants;
using EventReporting.Shared.Infrastructure.Models;
using EventReporting.Shared.Infrastructure.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EventReporting.BusinessLayer.Services
{
    public class QueueInputSubscriber : IHostedService
    {
        private readonly IConnection _connection;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private IModel _model;
        private readonly IServiceProvider _serviceProvider;

        public QueueInputSubscriber(IServiceProvider serviceProvider, IOptions<RabbitMqSettings> options)
        {
            _rabbitMqSettings = options.Value;
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = _rabbitMqSettings.Host;
            connectionFactory.Port = _rabbitMqSettings.Port;
            connectionFactory.Password = _rabbitMqSettings.Password;
            connectionFactory.UserName = _rabbitMqSettings.Username;
            try
            {
                _connection = connectionFactory.CreateConnection();
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine(ex.Message);
            }
            _serviceProvider = serviceProvider;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var outputQueueMessage = new OutputQueueMessage();
            if (!_rabbitMqSettings.StartInputListener || _connection == null)
            {
                return Task.CompletedTask;
            }

            _model = _connection.CreateModel();
            var consumer = new EventingBasicConsumer(_model);
                _model.QueueDeclare(_rabbitMqSettings.InputQueueName, false, false, true, null);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);

                try
                {
                    CreateEventDto dto = JsonConvert.DeserializeObject<CreateEventDto>(message);
                    outputQueueMessage.Md5 = dto.Md5;
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
                        await eventService.CreateAsync(dto);
                    }
                    outputQueueMessage.EventStatus = EventStatusConstants.SUCCESSFULLY_RECEIVED;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    outputQueueMessage.EventStatus = EventStatusConstants.UNSUCCESSFULLY_RECEIVED;
                }

                var sended = true;
                using (var scope = _serviceProvider.CreateScope())
                {
                    var queueSenderService = scope.ServiceProvider.GetRequiredService<IQueueSenderService>();
                    sended = queueSenderService.SendToQueueOutput(outputQueueMessage);
                }

                using (var scope = _serviceProvider.CreateScope())
                {
                    var eventService = scope.ServiceProvider.GetRequiredService<IEventService>();
                    await eventService.UpdateSendedToOutputAsync(outputQueueMessage.Md5, sended);
                }
            };
             
            _model.BasicConsume(queue: _rabbitMqSettings.InputQueueName,
                                 autoAck: true,
                                 consumer: consumer);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _model.Close();
            _connection.Close();

            return Task.CompletedTask;
        }
    }
}

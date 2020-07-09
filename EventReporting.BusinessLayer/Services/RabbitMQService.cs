using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Text;

namespace EventReporting.BusinessLayer.Services
{
    public class RabbitMQService : IRabbitMQService
    {
        private readonly RabbitMqSettings _rabbitMqSettings;

        public RabbitMQService(IOptions<RabbitMqSettings> options)
        {
            _rabbitMqSettings = options.Value;
        }

        public void CreateQueues()
        {
            IConnection connection = null;
            IModel model = null;
            try
            {
                connection = GetConnection();
                model = connection.CreateModel();
                //model.QueueDeclare(queue: _rabbitMqSettings.InputQueueName, 
                //                 durable: true,
                //                 exclusive: false,
                //                 autoDelete: false,
                //                 arguments: null);
                model.QueueDeclare(queue: _rabbitMqSettings.OutputQueueName, 
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
                model.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (model != null)
                {
                    model.Close();
                }

                if (connection != null)
                {
                    connection.Close();
                }

                Console.WriteLine(ex.Message);
            }
        }

        public IConnection GetConnection()
        {
            ConnectionFactory connectionFactory = new ConnectionFactory();
            connectionFactory.HostName = _rabbitMqSettings.Host;
            connectionFactory.Port = _rabbitMqSettings.Port;
            connectionFactory.Password = _rabbitMqSettings.Password;
            connectionFactory.UserName = _rabbitMqSettings.Username;
            return connectionFactory.CreateConnection();
        }

        public bool SendMessage(string message, string queueName)
        {
            IConnection connection = null;
            IModel model = null;
            bool sended = true;
            try
            {
                connection = GetConnection();
                model = connection.CreateModel();
                var body = Encoding.UTF8.GetBytes(message);
                IBasicProperties basicProperties = model.CreateBasicProperties();
                basicProperties.ContentType = "application/json";
                basicProperties.Priority = 0;
                basicProperties.ContentEncoding = "UTF-8";
                basicProperties.DeliveryMode = 2;
                model.BasicPublish(string.Empty, queueName, basicProperties, body);
                model.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                if (model != null)
                {
                    model.Close();
                }

                if (connection != null)
                {
                    connection.Close();
                }

                Console.WriteLine(ex.Message);
                sended = false;
            }

            return sended;
        }
    }
}

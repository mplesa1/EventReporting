using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
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
                model.QueueDeclare(_rabbitMqSettings.InputQueueName, false, false, true, null);
                model.QueueDeclare(_rabbitMqSettings.OutputQueueName, false, false, true, null);
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
                throw;
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

        public void SendEventMessage(string message)
        {
            IConnection connection = null; ;
            IModel model = null;
            try
            {
                connection = GetConnection();
                model = connection.CreateModel();

                var body = Encoding.UTF8.GetBytes(message);

                IBasicProperties basicProperties = model.CreateBasicProperties();
                basicProperties.ContentType = "application/json";
            //    basicProperties.Headers = new Dictionary<string, object>
            //{
            //    { "__TypeId__", "import url" }
            //};
                basicProperties.Priority = 0;
                basicProperties.ContentEncoding = "UTF-8";
                basicProperties.DeliveryMode = 2;


                model.BasicPublish(string.Empty, _rabbitMqSettings.InputQueueName, basicProperties, body);

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
                throw;
            }
        }
    }
}

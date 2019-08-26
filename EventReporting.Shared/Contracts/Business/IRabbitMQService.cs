using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.Shared.Contracts.Business
{
    public interface IRabbitMQService
    {
        IConnection GetConnection();

        void CreateQueues();

        bool SendMessage(string message, string queueName);
    }
}

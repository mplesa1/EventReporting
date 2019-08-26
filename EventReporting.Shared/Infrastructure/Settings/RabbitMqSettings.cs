using System;
using System.Collections.Generic;
using System.Text;

namespace EventReporting.Shared.Infrastructure.Settings
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }

        public int Port { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string OutputQueueName { get; set; }

        public string InputQueueName { get; set; }

        public bool StartInputListener { get; set; }
    }
}

using AutoMapper;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.DataTransferObjects.Event;
using EventReporting.Shared.Infrastructure.Models;
using EventReporting.Shared.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Text;

namespace EventReporting.BusinessLayer.Services
{
    public class QueueSenderService : BaseService, IQueueSenderService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly RabbitMqSettings _rabbitMqSettings;
        public QueueSenderService(IRabbitMQService rabbitMQService, IOptions<RabbitMqSettings> options,
            IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _rabbitMQService = rabbitMQService;
            _rabbitMqSettings = options.Value;
        }

        public bool SendToQueueInput(CreateEventDto createEventDto)
        {
            string md5Hash;
            StringBuilder sb = new StringBuilder();
            sb.Append(createEventDto.Address)
                .Append(createEventDto.Description)
                .Append(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss.fff"));

            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(sb.ToString());
                var hashBytes = md5.ComputeHash(inputBytes);
                sb.Clear();

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                md5Hash = sb.ToString();
            }

            createEventDto.Md5 = md5Hash;
            string json = JsonConvert.SerializeObject(createEventDto, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return _rabbitMQService.SendMessage(json, _rabbitMqSettings.InputQueueName);
        }

        public bool SendToQueueOutput(OutputQueueMessage outputQueueMessage)
        {
            string json = JsonConvert.SerializeObject(outputQueueMessage, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return _rabbitMQService.SendMessage(json, _rabbitMqSettings.OutputQueueName);
        }
    }
}

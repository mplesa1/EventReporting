using AutoMapper;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.DataTransferObjects.Event;
using EventReporting.Shared.DataTransferObjects.Settlement;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventReporting.BusinessLayer.Services
{
    public class QueueSenderService : BaseService, IQueueSenderService
    {
        private readonly IRabbitMQService _rabbitMQService;

        public QueueSenderService(IRabbitMQService rabbitMQService,
            IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _rabbitMQService = rabbitMQService;
        }

        public void Send(CreateEventDto createEventDto)
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

            bool sended = true;

            try
            {
                _rabbitMQService.SendEventMessage(json);
            }
            catch (Exception ex)
            {
                sended = false;
            }
        }
    }
}

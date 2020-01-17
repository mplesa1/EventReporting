using AutoMapper;
using EventReporting.Shared.Contracts.Business;
using EventReporting.Shared.Contracts.DataAccess;
using EventReporting.Shared.DataTransferObjects.Event;
using EventReporting.Shared.Infrastructure.Models;
using EventReporting.Shared.Infrastructure.Settings;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Text;
using JwtConstants = EventReporting.Shared.Infrastructure.Constants.JwtConstants;

namespace EventReporting.BusinessLayer.Services
{
    public class QueueSenderService : BaseService, IQueueSenderService
    {
        private readonly IRabbitMQService _rabbitMQService;
        private readonly RabbitMqSettings _rabbitMqSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QueueSenderService(IRabbitMQService rabbitMQService, IOptions<RabbitMqSettings> options, IHttpContextAccessor httpContextAccessor,
            IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
        {
            _rabbitMQService = rabbitMQService;
            _rabbitMqSettings = options.Value;
            _httpContextAccessor = httpContextAccessor;
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
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            if (httpContext?.User != null && httpContext.User.Identity.IsAuthenticated && 
                int.TryParse(httpContext.User.Claims.FirstOrDefault(claim => claim.Type == JwtConstants.ID_CLAIM_NAME).Value, out int userId))
            {
                createEventDto.UserId = userId;
            }

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

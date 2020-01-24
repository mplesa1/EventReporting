using EventReporting.Shared.Infrastructure.Constants;
using EventReporting.Shared.Infrastructure.Exceptions;
using EventReporting.Shared.Infrastructure.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Npgsql;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EventReporting.Shared.Middlerwares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        public ExceptionMiddleware(
            RequestDelegate next)
        {
            this._next = next;
            DefaultContractResolver contractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            };
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next.Invoke(httpContext);

            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException as PostgresException;
                string message = string.Empty;
                if (innerException != null)
                {
                    var humanizedMessage = GetReadableErrorMessage(innerException);
                    message = humanizedMessage;
                }

                await SendRepsonse(ApiResponse.CreateErrorResponse(HttpStatusCode.NotFound, message), httpContext);
            }
            catch (ResourceNotFoundException ex)
            {
                await SendRepsonse(ApiResponse.CreateErrorResponse(HttpStatusCode.NotFound, "Resource not found"), httpContext);
            }
            catch (BusinessException ex)
            {
                await SendRepsonse(ApiResponse.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message), httpContext);
            }
            catch (Exception ex)
            {
                await SendRepsonse(ApiResponse.CreateErrorResponse(HttpStatusCode.InternalServerError, "Interal server error:" + ex.Message), httpContext);
            }
        }

        private async Task SendRepsonse(ApiResponse response, HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)response.Code;
            httpContext.Response.ContentType = "application/json";
            var json = JsonConvert.SerializeObject(response, _jsonSerializerSettings);
            await httpContext.Response.WriteAsync(json);
        }

        private string GetReadableErrorMessage(PostgresException innerException)
        {
            switch (innerException.SqlState)
            {
                case DbErrorCodes.UNIQUE_VIOLATION:
                    return "Unique violation error";
                case DbErrorCodes.FOREIGN_KEY_VIOLATION:
                    return "Foreign key violation error";
                default:
                    return null;
            }
        }
    }
}

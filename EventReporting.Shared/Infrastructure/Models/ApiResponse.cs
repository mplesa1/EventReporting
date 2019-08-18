using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace EventReporting.Shared.Infrastructure.Models
{
    public class ApiResponse
    {
        public HttpStatusCode Code { get; set; }
        public object Errors { get; set; }
        public object Response { get; set; }

        public static ApiResponse CreateResponse(object data)
        {
            ApiResponse response = new ApiResponse();
            response.Response = data;

            return response;
        }

        public static ApiResponse CreateResponse(HttpStatusCode statusCode, object data)
        {
            var response = CreateResponse(data);
            response.Code = statusCode;
            return response;
        }

        public static ApiResponse CreateErrorResponse(HttpStatusCode statusCode, object errors)
        {
            ApiResponse response = new ApiResponse();
            response.Code = statusCode;
            response.Errors = errors;

            return response;
        }
    }
}

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PreciImageReader.CustomExceptionHandler
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleCustomExceptionAsync(context, ex);
            }
        }

        private async Task HandleCustomExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var customException = exception as BaseExceptionHandler;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var errorMessage = "Unexpected error";
            var errorDescription = "Unexpected error";

            if (null != customException)
            {
                errorMessage = customException.Message;
                errorDescription = customException.ErrorDescription;
                statusCode = customException.ErrorCode;
            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponseData
            {
                Message = errorMessage,
                Description = errorDescription
            }));
        }
    }
}

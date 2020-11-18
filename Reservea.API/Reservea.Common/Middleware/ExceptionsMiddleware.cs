using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Reservea.Common.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Reservea.Common.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly Formatting _formatting = Formatting.Indented;
        private readonly JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment webHostEnvironment)
        {
            _next = next;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (!(exception is ApiException)) throw exception;

            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;

            switch (exception)
            {
                case AuthenticationException _:
                {
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                }
                case UserCreationException _:
                {
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            if (_webHostEnvironment.IsProduction())
            {
                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new
                    {
                        exception.Message,
                    }, _formatting, _jsonSerializerSettings));
            }
            else
            {
                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new
                    {
                        exception.Message,
                        exception.StackTrace,
                        exception.InnerException
                    }, _formatting, _jsonSerializerSettings));
            }
        }
    }
}

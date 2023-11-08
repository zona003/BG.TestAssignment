using System.Net.Http;
using System.Text.Json;
using BGNet.TestAssignment.Common.WebApi.Models;
using Microsoft.AspNetCore.Http;

namespace BGNet.TestAssignment.Common.WebApi.Handlers
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var errors = new List<string>
            {
                new (exception.Message)
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(
                    new ResponseWrapper<object> { Errors = errors }));
        }
    }
}
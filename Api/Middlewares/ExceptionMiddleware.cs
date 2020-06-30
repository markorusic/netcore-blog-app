using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using ResourceException;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context /* other dependencies */)
        {
            try
            {
                await next(context);
            }
            catch (HttpException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception exceptionObj)
            {
                await HandleExceptionAsync(context, exceptionObj);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, HttpException exception)
        {
            var message = "Runtime error";
            context.Response.ContentType = "application/json";
            if (exception is HttpException)
            {
                message = exception.Message;
                context.Response.StatusCode = (int)exception.StatusCode;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { message }));
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var message = exception.Message;
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { message }));
        }
    }
}
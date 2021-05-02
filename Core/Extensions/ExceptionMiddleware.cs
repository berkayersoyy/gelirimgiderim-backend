  
using System;
using System.Net;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Core.Extensions
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(httpContext, e);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

            string message = "Internal Server Error agdgsdg";
            if (exception.GetType()==typeof(ValidationException))
            {
                message = exception.Message;
            }
            return httpContext.Response.WriteAsync(new ErrorDetails
            {
                Message = message,
                StatusCode = httpContext.Response.StatusCode
            }.ToString());
        }
    }
}

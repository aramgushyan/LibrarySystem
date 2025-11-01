using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Applications.Exceptions;

namespace Library.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        

        public ExceptionHandler(RequestDelegate next) 
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = "";
                (context.Response.StatusCode, message) = ex switch
                {
                    KeyNotFoundException => (404, ex.Message),
                    ArgumentNullException => (400, ex.Message),
                    ArgumentException => (400, ex.Message),
                    MyValidationException => (400,ex.Message),
                    DuplicateNameException => (400, ex.Message),
                    _ => (500, "Ошибка на сервере")
                };

                context.Response.ContentType = "application/json";

                await context.Response.WriteAsJsonAsync(new
                {
                    status = context.Response.StatusCode,
                    exceptionType = ex.GetType().Name,
                    detail = context.Response.StatusCode >= 500 ? null : ex.Message,
                    Error = context.Response.StatusCode >= 500 ? null : ex.Message
                });
            }
        }
    }
}
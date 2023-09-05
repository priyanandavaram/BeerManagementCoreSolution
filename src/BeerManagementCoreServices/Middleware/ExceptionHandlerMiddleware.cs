using BeerManagement.Web.Error;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
namespace BeerManagement.Web.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate next;
        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            next = requestDelegate;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var errorResponse = new ErrorDetails
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "An internal server error occurred.",
                    ExceptionMessage = ex.Message
                };
                var jsonResponse = JsonConvert.SerializeObject(errorResponse);
                await context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
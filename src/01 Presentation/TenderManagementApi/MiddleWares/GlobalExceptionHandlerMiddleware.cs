using System.Text.Json;
using TenderManagementApi.DTOs.Abstractions;

namespace TenderManagementApi.MiddleWares
{
    public class GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        IHostEnvironment hostEnvironment,
        ILogger<GlobalExceptionHandlerMiddleware> logger)
    {

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await this.HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            logger.LogError(exception.Message, exception);
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";
            string text = JsonSerializer.Serialize(ApiResponse<object>.Failed(hostEnvironment.IsDevelopment()  ? exception.Message : "General Error Happened."));
            return context.Response.WriteAsync(text);
        }
    }
}

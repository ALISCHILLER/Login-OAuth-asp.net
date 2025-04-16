using System.Net;
using System.Text.Json;

namespace Login_OAuth.WebAPI.Middleware
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
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = "خطای داخلی سرور";

            // شناسایی نوع خطا
            if (ex is UnauthorizedAccessException)
            {
                statusCode = (int)HttpStatusCode.Unauthorized;
                message = ex.Message;
            }
            else if (ex is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                message = ex.Message;
            }

            // اگر در حالت توسعه هستیم، می‌توانیم StackTrace را نیز اضافه کنیم
            if (context.RequestServices.GetService<IHostEnvironment>().IsDevelopment())
            {
                message += $" | StackTrace: {ex.StackTrace}";
            }

            context.Response.StatusCode = statusCode;

            var response = new
            {
                StatusCode = statusCode,
                Message = message,
                // می‌توانید اطلاعات اضافی دیگری نیز اضافه کنید
                Error = ex.GetType().Name
            };

            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}

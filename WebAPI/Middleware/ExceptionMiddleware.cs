using System.Net;

namespace Login_OAuth.WebAPI.Middleware
{
    // میدل‌ویر برای مدیریت خطاها
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
                await HandleExceptionAsync(context, ex); // مدیریت خطا
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // وضعیت خطا
            return context.Response.WriteAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Message = ex.Message // پیام خطا
            }.ToString());
        }
    }
}

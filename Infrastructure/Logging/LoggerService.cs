using Serilog;

namespace Login_OAuth.Infrastructure.Logging
{
    // کلاس تنظیمات لاگ‌گذاری با Serilog
    public class LoggerService
    {
        public static void ConfigureLogging()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console() // نوشتن در کنسول
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // نوشتن در فایل
                .CreateLogger(); // ایجاد لاگر
        }
    }
}

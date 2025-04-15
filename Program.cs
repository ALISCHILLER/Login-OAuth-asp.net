using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Login_OAuth.Infrastructure.Data;
using Login_OAuth.Infrastructure.Logging;
using Login_OAuth.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// تنظیمات لاگ‌گذاری با Serilog
LoggerService.ConfigureLogging();

// استفاده از Autofac به جای DI پیش‌فرض
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(containerBuilder =>
    {
        containerBuilder.AddApplicationServices(builder.Configuration); // ثبت سرویس‌ها
    });

// اضافه کردن سرویس‌های مورد نیاز
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // اتصال به دیتابیس

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// تنظیمات پایپلاین HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // صفحه خطا برای محیط توسعه
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // هدایت به HTTPS
app.UseRouting(); // مدیریت مسیریابی
app.UseAuthentication(); // احراز هویت
app.UseAuthorization(); // مجوزدهی
app.MapControllers(); // ثبت کنترلرها

app.Run(); // اجرای برنامه
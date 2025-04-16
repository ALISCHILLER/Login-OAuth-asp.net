using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Login_OAuth.Infrastructure.Data;
using Login_OAuth.Infrastructure.Logging;
using Login_OAuth.WebAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using Login_OAuth.Core.Validators;
using Login_OAuth.Core.DTOs;
using Login_OAuth.WebAPI.Middleware;

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



builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>());

builder.Services.AddFluentValidation();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))); // اتصال به دیتابیس

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Login OAuth API", Version = "v1" });
});

var app = builder.Build();

// ثبت middleware
app.UseMiddleware<ExceptionMiddleware>();

// تنظیمات پایپلاین HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // صفحه خطا برای محیط توسعه
    app.UseSwagger(); // فعال‌سازی Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Login OAuth API v1"); // تعیین نقطه دسترسی Swagger
        c.RoutePrefix = string.Empty; // تنظیم برای نمایش Swagger در ریشه
    });
}

app.UseHttpsRedirection(); // هدایت به HTTPS
app.UseRouting(); // مدیریت مسیریابی
app.UseAuthentication(); // احراز هویت
app.UseAuthorization(); // مجوزدهی
app.MapControllers(); // ثبت کنترلرها

app.Run(); // اجرای برنامه
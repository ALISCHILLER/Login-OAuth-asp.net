using Autofac;
using Login_OAuth.Application.Services;
using Login_OAuth.Core.Interfaces;
using Login_OAuth.Infrastructure.Repositories;

namespace Login_OAuth.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddApplicationServices(this ContainerBuilder builder, ConfigurationManager configuration)
        {
            builder.RegisterType<AuthService>().As<IAuthService>(); // ثبت AuthService
            builder.RegisterType<UserRepository>().As<IUserRepository>(); // ثبت UserRepository
            // سایر سرویس‌ها و رجیستری‌ها می‌توانند در اینجا اضافه شوند
        }
    }
}

using Login_OAuth.Core.DTOs;

namespace Login_OAuth.Application.Services
{
    // اینترفیس برای سرویس احراز هویت
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request); // متد لاگین کاربر
    }
}

using Login_OAuth.Core.DTOs;
using Login_OAuth.Core.Interfaces;
using Login_OAuth.Infrastructure.Security;
using BCrypt.Net;
namespace Login_OAuth.Application.Services;

// پیاده‌سازی سرویس احراز هویت
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository; // Repository کاربران
    private readonly IConfiguration _configuration;  // تنظیمات برنامه

    public AuthService(IUserRepository userRepository, IConfiguration configuration)
    {
        _userRepository = userRepository;
        _configuration = configuration;
    }

    // متد لاگین کاربر
    public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
    {
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);

        // بررسی وجود کاربر و اعتبار رمز عبور
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            throw new UnauthorizedAccessException("نام کاربری یا رمز عبور اشتباه است.");

        // تولید توکن JWT
        var token = JwtHelper.GenerateJwtToken(user, _configuration);

        return new LoginResponseDto { Token = token, Role = user.Role };
    }
}

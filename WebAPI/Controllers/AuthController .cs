using FluentValidation;
using Login_OAuth.Application.Services;
using Login_OAuth.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Login_OAuth.WebAPI.Controllers
{
    // کنترلر برای احراز هویت
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService; // سرویس احراز هویت
        private readonly IValidator<LoginRequestDto> _validator; // اعتبارسنجی

        public AuthController(IAuthService authService, IValidator<LoginRequestDto> validator)
        {
            _authService = authService;
            _validator = validator;
        }

        // متد برای لاگین
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var validationResult = await _validator.ValidateAsync(request); // اعتبارسنجی
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors); // بازگرداندن خطاهای اعتبارسنجی

            try
            {
                var response = await _authService.LoginAsync(request); // فراخوانی متد لاگین
                return Ok(response); // بازگرداندن پاسخ موفق
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message); // بازگرداندن خطای عدم دسترسی
            }
        }
    }
}

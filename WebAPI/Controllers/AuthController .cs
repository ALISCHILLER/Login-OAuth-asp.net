using FluentValidation;
using Login_OAuth.Application.Services;
using Login_OAuth.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Login_OAuth.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IValidator<LoginRequestDto> _validator;

        public AuthController(IAuthService authService, IValidator<LoginRequestDto> validator)
        {
            _authService = authService;
            _validator = validator;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApiResponse<LoginResponseDto>>> Login([FromBody] LoginRequestDto request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = "خطا در اعتبارسنجی",
                    Data = validationResult.Errors
                });
            }

            try
            {
                var response = await _authService.LoginAsync(request);
                return Ok(new ApiResponse<LoginResponseDto>
                {
                    StatusCode = StatusCodes.Status200OK,
                    Message = "ورود موفقیت‌آمیز بود.",
                    Data = response
                });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<object>
                {
                    StatusCode = StatusCodes.Status500InternalServerError,
                    Message = "خطای داخلی سرور",
                    Data = ex.Message
                });
            }
        }
    }
}
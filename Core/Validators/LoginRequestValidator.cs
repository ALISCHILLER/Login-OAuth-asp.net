using FluentValidation;
using Login_OAuth.Core.DTOs;

namespace Login_OAuth.Core.Validators
{
    // اعتبارسنجی برای درخواست لاگین
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("نام کاربری نباید خالی باشد."); // بررسی خالی نبودن نام کاربری
            RuleFor(x => x.Password).NotEmpty().WithMessage("رمز عبور نباید خالی باشد."); // بررسی خالی نبودن رمز عبور
        }
    }
}

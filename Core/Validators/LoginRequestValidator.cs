using FluentValidation;
using Login_OAuth.Core.DTOs;

namespace Login_OAuth.Core.Validators
{
    // اعتبارسنجی برای درخواست لاگین
    public class LoginRequestValidator : AbstractValidator<LoginRequestDto>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("نام کاربری نباید خالی باشد.")
                .Length(3, 20).WithMessage("نام کاربری باید بین ۳ تا ۲۰ کاراکتر باشد."); // طول نام کاربری

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("رمز عبور نباید خالی باشد.")
                .MinimumLength(6).WithMessage("رمز عبور باید حداقل ۶ کاراکتر باشد."); // حداقل طول رمز عبور
        }
    }
}

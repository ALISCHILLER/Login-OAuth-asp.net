using FluentValidation;

namespace Login_OAuth.Core.DTOs
{
    // مدل داده‌ای برای درخواست لاگین
    public class LoginRequestDto
    {
        public string Username { get; set; } // نام کاربری
        public string Password { get; set; } // رمز عبور
    }

    //public class LoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    //{
    //    public LoginRequestDtoValidator()
    //    {
    //        RuleFor(x => x.Username)
    //            .NotEmpty()
    //            .WithMessage("نام کاربری الزامی است.");

    //        RuleFor(x => x.Password)
    //            .NotEmpty()
    //            .WithMessage("رمز عبور الزامی است.");
    //    }
    //}
}

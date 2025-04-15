namespace Login_OAuth.Core.DTOs
{
    // مدل داده‌ای برای درخواست لاگین
    public class LoginRequestDto
    {
        public string Username { get; set; } // نام کاربری
        public string Password { get; set; } // رمز عبور
    }
}

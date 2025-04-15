namespace Login_OAuth.Core.DTOs
{
    // مدل داده‌ای برای پاسخ لاگین
    public class LoginResponseDto
    {
        public string Token { get; set; } // توکن JWT برای احراز هویت
        public string Role { get; set; }  // نقش کاربر
    }
}

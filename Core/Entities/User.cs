namespace Login_OAuth.Core.Entities
{
    // کلاس موجودیت کاربر
    public class User
    {
        public int Id { get; set; }               // شناسه منحصر به فرد کاربر
        public string Username { get; set; }     // نام کاربری
        public string Email { get; set; }        // ایمیل کاربر
        public string PasswordHash { get; set; } // رمز عبور کاربر (رمزگذاری شده)
        public string Role { get; set; }         // نقش کاربر (Admin, User)
    }
}

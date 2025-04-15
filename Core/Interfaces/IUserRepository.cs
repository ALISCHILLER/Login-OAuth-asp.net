using Login_OAuth.Core.Entities;

namespace Login_OAuth.Core.Interfaces
{
    // اینترفیس برای دسترسی به اطلاعات کاربران
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username); // دریافت کاربر بر اساس نام کاربری
        Task AddUserAsync(User user);                      // افزودن کاربر جدید
    }
}

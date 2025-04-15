using Login_OAuth.Core.Entities;
using Login_OAuth.Core.Interfaces;
using Login_OAuth.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Login_OAuth.Infrastructure.Repositories
{
    // پیاده‌سازی Repository برای کاربران
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context; // کانتکست دیتابیس

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        // دریافت کاربر بر اساس نام کاربری
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        // افزودن کاربر جدید
        public async Task AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync(); // ذخیره تغییرات در دیتابیس
        }
    }
}

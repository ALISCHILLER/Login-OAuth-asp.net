using Login_OAuth.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Login_OAuth.Infrastructure.Data
{
    // کانتکست دیتابیس
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // جدول کاربران

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(u => u.Id); // کلید اصلی
            modelBuilder.Entity<User>().Property(u => u.Username).IsRequired(); // الزامی بودن نام کاربری
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired(); // الزامی بودن ایمیل
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired(); // الزامی بودن رمز عبور
            modelBuilder.Entity<User>().Property(u => u.Role).HasDefaultValue("User"); // نقش پیش‌فرض
        }
    }
}

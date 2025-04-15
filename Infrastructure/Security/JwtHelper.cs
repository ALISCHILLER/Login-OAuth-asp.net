using Login_OAuth.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Login_OAuth.Infrastructure.Security
{
    // کلاس کمک برای تولید توکن JWT
    public static class JwtHelper
    {
        public static string GenerateJwtToken(User user, IConfiguration configuration)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username), // اضافه کردن نام کاربری به توکن
                new Claim(ClaimTypes.Role, user.Role)      // اضافه کردن نقش کاربر به توکن
            };

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],      // صادرکننده توکن
                audience: configuration["Jwt:Audience"],  // مخاطب توکن
                claims: claims,                           // ادعاهای توکن
                expires: DateTime.Now.AddHours(1),        // انقضا توکن (1 ساعت)
                signingCredentials: credentials           // امضای دیجیتال
            );

            return new JwtSecurityTokenHandler().WriteToken(token); // تبدیل توکن به رشته
        }
    }
}

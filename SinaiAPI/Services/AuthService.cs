using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SinaiAPI.Data;
using SinaiAPI.DTOs;
using SinaiAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SinaiAPI.Services
{
    public class AuthService(SinaiDbContext context, IConfiguration configuration)
    {
        public async Task<bool> EmailExists(string email)
        {
            return await context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> Register(RegisterDTO request)
        {
            if (await EmailExists(request.Email))
            {
                throw new ArgumentException("Email is already taken.");
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = passwordHash,
                Role = User.RoleType.User
            };

            context.Users.Add(user);
            await context.SaveChangesAsync();

            return user;
        }

        public async Task<(string token, object user)?> Login(LoginDTO request)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Username == request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return null;
            }

            string token = GenerateJwtToken(user);

            return ( token, new { id = user.Id, user.Username, user.Role });
        }

        private string GenerateJwtToken(User user)
        {
            var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!);
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
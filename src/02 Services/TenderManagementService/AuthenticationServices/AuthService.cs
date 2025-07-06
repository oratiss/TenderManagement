using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TenderManagementDAL.Models;

namespace TenderManagementService.AuthenticationServices
{
    public class AuthService(UserManager<User> userManager, IConfiguration config) : IAuthService
    {
        public async Task<string> RegisterAsync(string email, string password, bool? isAdmin)
        {
            var user = new User { UserName = email, Email = email };

            var result = await userManager.CreateAsync(user, password);
            if (!result.Succeeded) throw new Exception("User registration failed.");

            switch (isAdmin)
            {
                case null or false:
                    await userManager.AddToRoleAsync(user, "vendor");
                    break;
                case true:
                    await userManager.AddToRoleAsync(user, "admin");
                    break;
            }

            return "User registered.";
        }

        public async Task<string> LoginAsync(string email, string password)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null && await userManager.CheckPasswordAsync(user, password))
            {
                return await GenerateJwt(user);
            }

            throw new Exception("Invalid credentials.");
        }

        private async Task<string> GenerateJwt(User user)
        {
            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email!)
            };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                expires: DateTime.Now.AddMinutes(60),
                claims: authClaims,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

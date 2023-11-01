using BG.TestAssignment.DataAccess.DataContext;
using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BG.TestAssignment.AuthApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BG.TestAssignment.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserDataContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(UserDataContext context, UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<ActionResult> Login(AuthRequest request)
        {
            if (request == null)
            {
                return BadRequestResult(ModelState);
            }

            var managedUser = await _userManager.FindByNameAsync(request.UserName);

            if (managedUser == null)
            {
                return BadRequest("Bad credentials");
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
            {
                return BadRequest("Bad credentials");
            }

            var userRoles = await _userManager.GetRolesAsync(managedUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, managedUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var claim in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Name, claim));
            }

            string token = GenerateToken(claims);
            return Ok(new { managedUser.UserName, token });

        }


        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

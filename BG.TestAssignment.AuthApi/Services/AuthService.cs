using BG.TestAssignment.DataAccess.DataContext;
using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BG.TestAssignment.AuthApi.Services.Interfaces;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BG.TestAssignment.Business.Validators;

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

        public async Task<AuthResponce> Login(AuthRequest request)
        {
            if (request == null)
            {
                return new AuthResponce();
            }

            LoginValidator validator = new LoginValidator();
            var result = validator.Validate(request);

            if (result.IsValid)
            {
                return new AuthResponce();
            }

            var managedUser = await _userManager.FindByNameAsync(request.UserName);

            if (managedUser == null)
            {
                return new AuthResponce();
            }

            bool isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPasswordValid)
            {
                return new AuthResponce();
            }

            var userRoles = await _userManager.GetRolesAsync(managedUser);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,  managedUser.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var claim in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Name, claim));
            }

            string token = GenerateToken(claims);

            AuthResponce authResponce = managedUser.Adapt<AuthResponce>();
            authResponce.Token = token;
            
            return authResponce;

        }

        public async Task<bool> Register(RegisterRequest request)
        {
            if (request == null)
            {
                return false;
            }



            //if (!ModelState.IsValid) return BadRequest(request);
            //var userExist = await _userManager.FindByNameAsync(request.UserName);
            //if (userExist != null)
            //    return BadRequest("User already exist");

            //AppUser user = new AppUser
            //{
            //    UserName = request.UserName,
            //    FirstName = request.FirstName,
            //    LastName = request.LastName,
            //    BirthDate = request.BirthDate,
            //    Address = request.Address,
            //};

            //var createUserResult = await _userManager.CreateAsync(user, request.Password);
            //if (!createUserResult.Succeeded)
            //    return BadRequest(request);

            //var findUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            //if (findUser == null) throw new Exception($"User {request.UserName} not found");

            //return await Login(new AuthRequest
            //{
            //    UserName = request.UserName,
            //    Password = request.Password
            //});
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

using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BG.TestAssignment.AuthApi.Services.Interfaces;
using BG.TestAssignment.DataAccess.DataContext;

namespace BG.TestAssignment.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly UserDataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(UserDataContext context, UserManager<AppUser> userManager, IConfiguration configuration, IAuthService authService)
        {
            _context = context;
            _userManager = userManager;
            _configuration = configuration;
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] AuthRequest request)
        {
            AuthResponce logedUser = await _authService.Login(request);

            if (logedUser.Token == null)
            {
                return BadRequest();
            }

            return Ok(logedUser);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);
            var userExist = await _userManager.FindByNameAsync(request.UserName);
            if (userExist != null)
                return BadRequest("User already exist");

            AppUser user = new AppUser
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Address = request.Address,
            };

            var createUserResult = await _userManager.CreateAsync(user, request.Password);
            if (!createUserResult.Succeeded)
                return BadRequest(request);

            var findUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.UserName);

            if (findUser == null) throw new Exception($"User {request.UserName} not found");

            return await Login(new AuthRequest
            {
                UserName = request.UserName,
                Password = request.Password
            });
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

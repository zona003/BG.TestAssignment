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
using BG.TestAssignment.DataAccess.Entities;

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
                return BadRequest("Wrong credentials");
            }

            return Ok(logedUser);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);

            bool registerResult = await _authService.Register(request);
            if (!registerResult)
            {
                return BadRequest("User already exist");
            }

            return Ok();
        }
    }
}

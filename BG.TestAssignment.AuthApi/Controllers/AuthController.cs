using BG.TestAssignment.DataAccess;
using BG.TestAssignment.DataAccess.Entities;
using BG.TestAssignment.Models;
using BGNet.TestAssignment.Api.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly BookAuthorsDataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;

        public AuthController(BookAuthorsDataContext context, UserManager<AppUser> userManager, IConfiguration configuration, IAuthService authService)
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

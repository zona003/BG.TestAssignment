using BG.TestAssignment.DataAccess;
using BGNet.TestAssignment.Api.Services.Interfaces;
using BGNet.TestAssignment.DataAccess;
using BGNet.TestAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Auth.Controllers
{
    [Authorize]
    [Route("api/Auth/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly BookAuthorsDataContext _context;
        private readonly IUserService _userService;

        public UserController(BookAuthorsDataContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult> GetCurrentUser()
        {
            UserDto currentUserDto = await _userService.GetCurrentUser(User.Identity.Name);

            return Ok(currentUserDto);
        }
    }
}

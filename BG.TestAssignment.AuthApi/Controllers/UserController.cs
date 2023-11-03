using BG.TestAssignment.DataAccess;
using BG.TestAssignment.Models;
using BGNet.TestAssignment.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BGNet.TestAssignment.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
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
            UserDTO currentUserDto = await _userService.GetCurrentUser(User.Identity.Name);
            
            return Ok(currentUserDto);
        }
    }
}

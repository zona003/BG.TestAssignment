using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using BG.TestAssignment.AuthApi.Services;
using BG.TestAssignment.AuthApi.Services.Interfaces;
using BG.TestAssignment.DataAccess;
using Microsoft.IdentityModel;
using BG.TestAssignment.DataAccess.Entities;

namespace BG.TestAssignment.AuthApi.Controllers
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

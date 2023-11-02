using BG.TestAssignment.AuthApi.Services.Interfaces;
using BG.TestAssignment.DataAccess;
using BG.TestAssignment.DataAccess.Entities;
using BG.TestAssignment.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.AuthApi.Services
{
    public class UserService : IUserService
    {
        private readonly BookAuthorsDataContext _context;

        public UserService(BookAuthorsDataContext context)
        {
            _context = context;
        }

        public async Task<UserDTO> GetCurrentUser(string username)
        {
            AppUser currentUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (currentUser == null)
                return new UserDTO();

            UserDTO currentUserDto = currentUser.Adapt<UserDTO>();
            return currentUserDto;
        }
    }
}

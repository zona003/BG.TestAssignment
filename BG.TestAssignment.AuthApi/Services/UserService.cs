﻿using BG.TestAssignment.DataAccess;
using BGNet.TestAssignment.Api.Services.Interfaces;
using BGNet.TestAssignment.DataAccess;
using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.Api.Services
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

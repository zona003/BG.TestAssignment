using BG.TestAssignment.DataAccess.Entities;
using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.DataAccess.DataContext
{

    public class UserDataContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public UserDataContext(DbContextOptions<UserDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}


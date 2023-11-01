using BG.TestAssignment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.AuthApi.Data
{

    public class DataContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
            
        }
    }
}


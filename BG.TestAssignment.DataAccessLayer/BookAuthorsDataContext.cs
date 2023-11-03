using BG.TestAssignment.DataAccess.Entities;
using BG.TestAssignment.DataAccess.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.DataAccess
{
    public class BookAuthorsDataContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookAuthorsDataContext(DbContextOptions<BookAuthorsDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

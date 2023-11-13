using BGNet.TestAssignment.DataAccess.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.DataAccess
{
    public class BookAuthorsDataContext : IdentityDbContext<AppUser, IdentityRole<long>, long>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public DbSet<AuthorBooks> AuthorBooks { get; set; }

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

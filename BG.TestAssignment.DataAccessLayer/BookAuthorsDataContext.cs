using BG.TestAssignment.DataAccess.Entities;
using BG.TestAssignment.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.DataAccess
{
    public class BookAuthorsDataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookAuthorsDataContext(DbContextOptions<BookAuthorsDataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BooksConfiguration());
        }
    }
}

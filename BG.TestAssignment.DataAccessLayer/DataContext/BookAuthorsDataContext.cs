using BG.TestAssignment.DataAccess.Entities;
using BG.TestAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.DataAccess.DataContext
{
    public class BookAuthorsDataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookAuthorsDataContext(DbContextOptions<BookAuthorsDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}

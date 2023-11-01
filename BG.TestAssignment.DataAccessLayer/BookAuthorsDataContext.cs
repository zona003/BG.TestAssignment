using BG.TestAssignment.Models;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.DataAccessLayer.DataContext
{
    public class BookAuthorsDataContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public BookAuthorsDataContext(DbContextOptions<BookAuthorsDataContext> options)
            : base(options)
        {
        }
    }
}

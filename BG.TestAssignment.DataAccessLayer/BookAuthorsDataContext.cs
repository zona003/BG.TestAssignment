using BG.TestAssignment.DataAccess.Entities;
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
    }
}

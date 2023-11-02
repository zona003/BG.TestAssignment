using BG.TestAssignment.DataAccess;
using BG.TestAssignment.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.Migrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //using (var context = new BookAuthorsDataContext(options =>
            //           options.UseNpgsql("Host=localhost;Port=5432;Database=unionBookDb;Username=postgres;Password=123456")))
            //{
            //    var author = new Author
            //    {
            //        FirstName = "William",
            //        LastName = "Shakespeare",
            //        Books = new List<Book>
            //        {
            //            new Book { Title = "Hamlet"},
            //            new Book { Title = "Othello" },
            //            new Book { Title = "MacBeth" }
            //        }
            //    };
            //    context.Add(author);
            //    context.SaveChanges();
            //}
        }
    }
}
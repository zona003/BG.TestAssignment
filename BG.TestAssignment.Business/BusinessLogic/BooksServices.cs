using BG.TestAssignment.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using BG.TestAssignment.Business.BusinessLogic.Interfaces;
using BG.TestAssignment.Business.Validators;
using BG.TestAssignment.DataAccess;
using BG.TestAssignment.DataAccess.Entities;

namespace BG.TestAssignment.Business.BusinessLogic
{
    public class BooksServices : IBooksService
    {
        private BookAuthorsDataContext Context { get; set; }

        public BooksServices(BookAuthorsDataContext dbContext)
        {
            Context = dbContext;
        }

        public List<BookDTO> GetBooks()
        {
            return Context.Books.AsQueryable().Adapt<List<BookDTO>>();
        }

        public BookDTO GetBook(int id)
        {
            var result = Context.Books.FirstOrDefault(a => a.Id == id);
            if (result == null) return new BookDTO();

            return result.Adapt<BookDTO>();
        }

        public bool PutBook(int id, BookDTO bookDto)
        {
            if (id != bookDto.Id)
            {
                return false;
            }

            var book = bookDto.Adapt<Book>();

            BookValidator validator = new BookValidator();
            var validationResult = validator.Validate(book);

            if (!validationResult.IsValid) return false;

            Context.Entry(book).State = EntityState.Modified;

            try
            {
                Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        public bool PostBook(BookDTO? bookDto)
        {
            
            if (bookDto == null)
            {
                return false;
            }

            var book = bookDto.Adapt<Book>();
            BookValidator validator = new BookValidator();
            var validationResult = validator.Validate(book);

            if (!validationResult.IsValid) return false;

            Context.Books.Add(book);
            Context.SaveChanges();

            return true;
        }

        public bool DeleteBook(int id)
        {

            if (Context.Books == null)
            {
                return false;
            }
            var book = Context.Books.FindAsync(id).Result;
            if (book == null)
            {
                return false;
            }

            Context.Books.Remove(book);
            Context.SaveChangesAsync();

            return true;
        }

        private bool BookExists(int id)
        {
            return (Context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

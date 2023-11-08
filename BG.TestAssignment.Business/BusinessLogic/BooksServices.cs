using BGNet.TestAssignment.Business.BusinessLogic.Interfaces;
using BGNet.TestAssignment.Business.Validators;
using BGNet.TestAssignment.Common.WebApi.Models;
using BGNet.TestAssignment.DataAccess;
using BGNet.TestAssignment.DataAccess.Entities;
using BGNet.TestAssignment.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace BGNet.TestAssignment.Business.BusinessLogic
{
    public class BooksServices : IBooksService
    {
        private BookAuthorsDataContext Context { get; set; }

        public BooksServices(BookAuthorsDataContext dbContext)
        {
            Context = dbContext;
        }

        public async Task<ResponseWrapper<PagedResponce<List<BookDTO>>>> GetBooks(int skip, int take, CancellationToken token)
        {
            var count = Context.Books.Count();
            var data = Context.Books.AsQueryable().Skip(skip).Take(take).Adapt<List<BookDTO>>(); ;
            PagedResponce<List<BookDTO>> pagedResult = new(count, data);
            if (!pagedResult.Items.Any())
            {
                return new ResponseWrapper<PagedResponce<List<BookDTO>>>(errors: new List<string>() { "Collection is empty" });
            }
            return ResponseWrapper<PagedResponce<List<BookDTO>>>.WrapToResponce(pagedResult);

        }

        public async Task<ResponseWrapper<BookDTO>> GetBook(int id, CancellationToken token)
        {
            ResponseWrapper<BookDTO> response = new(errors: new List<string>());
            var result = await Context.Books.FirstOrDefaultAsync(a => a.Id == id, token);
            if (result == null)
            {
                response?.Errors.Add("Not found");
                return response;
            }

            return ResponseWrapper<BookDTO>.WrapToResponce(result.Adapt<BookDTO>());
        }

        public async Task<ResponseWrapper<BookDTO>> PutBook(int id, BookDTO bookDto, CancellationToken token)
        {
            ResponseWrapper<BookDTO> response = new(errors: new List<string>());
            if (id != bookDto.Id)
            {
                response.Errors?.Add("Bad request!");
                return response;
            }

            var book = bookDto.Adapt<Book>();

            BookValidator validator = new BookValidator();
            var validationResult = validator.Validate(book);
            response.Errors?.AddRange(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            if (!validationResult.IsValid) return response;
            if (!BookExists(id))
            {
                response.Errors?.Add("Object not exist");
                return response;
            }
            if (!AuthorExist(bookDto.AuthorId))
            {
                response.Errors?.Add("Author not exist");
                return response;
            }

            Context.Entry(book).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors?.Add(ex.Message);
                return response;
            }

            return ResponseWrapper<BookDTO>.WrapToResponce(bookDto);
        }

        public async Task<ResponseWrapper<BookDTO>> PostBook(BookDTO? bookDto, CancellationToken token)
        {
            ResponseWrapper<BookDTO> response = new(errors: new List<string>());
            if (bookDto == null)
            {
                response.Errors?.Add("Bad request");
                return response;
            }

            var book = bookDto.Adapt<Book>();
            BookValidator validator = new BookValidator();
            var validationResult = validator.Validate(book);
            response.Errors?.AddRange(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            if (!validationResult.IsValid) return response;

            if (AuthorExist(bookDto.AuthorId))
            {
                response.Errors?.Add("Author not exist");
                return response;
            }

            Context.Books.Add(book);
            try
            {
                await Context.SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors?.Add(ex.Message);
                return response;

            }

            return ResponseWrapper<BookDTO>.WrapToResponce(bookDto); ;
        }

        public async Task<ResponseWrapper<BookDTO>> DeleteBook(int id, CancellationToken token)
        {
            ResponseWrapper<BookDTO> response = new(errors: new List<string>());
            var book = await Context.Books.FindAsync(id, token);
            if (book == null)
            {
                response.Errors?.Add("Not found");
                return response;
            }

            Context.Books.Remove(book);
            try
            {
                await Context.SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors?.Add(ex.Message);
                return response;

            }

            return ResponseWrapper<BookDTO>.WrapToResponce(new BookDTO()); ;
        }

        private bool BookExists(int id)
        {
            return (Context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool AuthorExist(int id)
        {
            return (Context.Authors?.Find(id) == null);
        }
    }
}
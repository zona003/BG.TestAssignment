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

        public async Task<ResponseWrapper<PagedResponce<List<BookDto>>>> GetBooks(int? skip, int? take, CancellationToken token)
        {
            var count = Context.Books.Count();
            int skipValue = skip ?? 0;
            int takeValue = take ?? count;
            var data = Context.Books.Include(A => A.Authors).AsNoTracking().AsQueryable().Skip(skipValue).Take(takeValue).Adapt<List<BookDto>>();
            PagedResponce<List<BookDto>> pagedResult = new(count, data);
            if (!pagedResult.Items.Any())
            {
                return new ResponseWrapper<PagedResponce<List<BookDto>>>(errors: new List<string>() { "Collection is empty" });
            }
            return ResponseWrapper<PagedResponce<List<BookDto>>>.WrapToResponce(pagedResult);

        }

        public async Task<ResponseWrapper<BookDto>> GetBook(int id, CancellationToken token)
        {
            ResponseWrapper<BookDto> response = new(errors: new List<string>());
            var result = await Context.Books.FirstOrDefaultAsync(a => a.Id == id, token);
            if (result == null)
            {
                response.Errors?.Add("Not found");
                return response;
            }

            return ResponseWrapper<BookDto>.WrapToResponce(result.Adapt<BookDto>());
        }

        public async Task<ResponseWrapper<BookDto>> PutBook(int id, BookDto bookDto, CancellationToken token)
        {
            ResponseWrapper<BookDto> response = new(errors: new List<string>());
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
            foreach (var author in bookDto.Authors)
            {
                if (!AuthorExist(author.Id))
                {
                    response.Errors?.Add($"Author {author.Id} not exist");
                    return response;
                }
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

            return ResponseWrapper<BookDto>.WrapToResponce(bookDto);
        }

        public async Task<ResponseWrapper<AddBookRequest>> PostBook(AddBookRequest bookDto, CancellationToken token)
        {
            ResponseWrapper<AddBookRequest> response = new(errors: new List<string>());
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

            if (BookExists(bookDto.Id))
            {
                response.Errors?.Add("Object already exist");
                return response;
            }

            if (bookDto.AuthorsInBooks == null)
            {
                response.Errors?.Add("Author not exist");
                return response;
            }
            foreach (var author in bookDto.AuthorsInBooks)
            {
                
                if (!AuthorExist(author))
                {
                    response.Errors?.Add("Author not exist");
                    return response;
                }

            }

            // var authorsIds = bookDto.Authors.Select(s => s.Id);
            //  book.Authors = await Context.Authors.Where(w => authorsIds.Contains(w.Id)).ToListAsync();
            Context.Authors.AttachRange(book.Authors!);
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

            

            return ResponseWrapper<AddBookRequest>.WrapToResponce(bookDto);
        }

        public async Task<ResponseWrapper<BookDto>> DeleteBook(int id, CancellationToken token)
        {
            ResponseWrapper<BookDto> response = new(errors: new List<string>());
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

            return ResponseWrapper<BookDto>.WrapToResponce(new BookDto());
        }

        private bool BookExists(int id)
        {
            return (Context.Books?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool AuthorExist(int id)
        {
            return (Context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
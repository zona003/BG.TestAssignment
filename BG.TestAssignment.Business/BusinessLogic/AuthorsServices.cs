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
    public class AuthorsServices : IAuthorService
    {
        private BookAuthorsDataContext Context { get; set; }

        public AuthorsServices(BookAuthorsDataContext dbContext)
        {
            Context = dbContext;
        }


        public async Task<ResponseWrapper<PagedResponce<List<AuthorDto>>>> GetAuthors(int skip, int take, CancellationToken token)
        {
            var count = Context.Authors.Count();
            var data = Context.Authors.Include(b => b.Books).AsQueryable().Skip(skip).Take(take).Adapt<List<AuthorDto>>(); ;
            PagedResponce<List<AuthorDto>> pagedResult = new(count, data);

            if (!pagedResult.Items.Any())
            {
                return new ResponseWrapper<PagedResponce<List<AuthorDto>>>(errors: new List<string>() { "Collection is empty" });
            }
            return ResponseWrapper<PagedResponce<List<AuthorDto>>>.WrapToResponce(pagedResult);
        }

        public async Task<ResponseWrapper<AuthorDto>> GetAuthor(int id, CancellationToken token)
        {
            ResponseWrapper<AuthorDto> response = new(errors: new List<string>());

            var result = await Context.Authors.FirstOrDefaultAsync(a => a.Id == id, token);
            if (result == null)
            {
                response?.Errors.Add("Not found");
                return response;
            }

            return ResponseWrapper<AuthorDto>.WrapToResponce(result.Adapt<AuthorDto>());
        }

        public async Task<ResponseWrapper<AuthorDto>> PutAuthor(int id, AuthorDto authorDto, CancellationToken token)
        {
            ResponseWrapper<AuthorDto> response = new ResponseWrapper<AuthorDto>(errors: new List<string>());
            if (id != authorDto.Id)
            {
                response.Errors.Add("Bad request!");
                return response;
            }

            var author = authorDto.Adapt<Author>();

            AuthorValidator validator = new AuthorValidator();
            var validationResult = validator.Validate(author);
            response.Errors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            if (!validationResult.IsValid) return response;
            if (!AuthorExists(id))
            {
                response.Errors.Add("Object not exist");
                return response;
            }

            Context.Entry(author).State = EntityState.Modified;

            try
            {
                await Context.SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors.Add(ex.Message);
                return response;

            }

            return ResponseWrapper<AuthorDto>.WrapToResponce(authorDto);
        }

        public async Task<ResponseWrapper<AuthorDto>> PostAuthor(AuthorDto? authorDto, CancellationToken token)
        {
            ResponseWrapper<AuthorDto> response = new(errors: new List<string>());
            if (authorDto == null)
            {
                response.Errors.Add("Bad request");
                return response;
            }

            var author = authorDto.Adapt<Author>();

            AuthorValidator validator = new AuthorValidator();
            var validationResult = validator.Validate(author);
            response.Errors.AddRange(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            if (!validationResult.IsValid) return response;

            if (AuthorExists(authorDto.Id))
            {
                response.Errors.Add("Already exist");
                return response;
            }
            Context.Authors.Add(author);
            try
            {
                await Context.SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors.Add(ex.Message);
                return response;

            }


            return ResponseWrapper<AuthorDto>.WrapToResponce(authorDto);
        }

        public async Task<ResponseWrapper<AuthorDto>> DeleteAuthor(int id, CancellationToken token)
        {
            ResponseWrapper<AuthorDto> response = new(errors: new List<string>());

            var author = await Context.Authors.FindAsync(id, token);
            if (author == null)
            {
                response.Errors.Add("Not found");
                return response;
            }

            Context.Authors.Remove(author);
            try
            {
                await Context.SaveChangesAsync(token);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors.Add(ex.Message);
                return response;

            }

            return ResponseWrapper<AuthorDto>.WrapToResponce(new AuthorDto());
        }

        private bool AuthorExists(int id)
        {
            return (Context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
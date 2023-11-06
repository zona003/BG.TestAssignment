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

        public ResponseWrapper<List<AuthorDTO>> GetAuthors()
        {
            var result = Context.Authors.AsQueryable().Adapt<List<AuthorDTO>>();
            if (!result.Any())
            {
                return new ResponseWrapper<List<AuthorDTO>>(errors: new List<string>() { "Collection is empty" });
            }
            return ResponseWrapper<List<AuthorDTO>>.WrapToResponce(result);
        }

        public async Task<ResponseWrapper<AuthorDTO>> GetAuthor(int id)
        {
            ResponseWrapper<AuthorDTO> response = new(errors: new List<string>()); 
            var result = await Context.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (result == null)
            {
                response?.Errors.Add("Not found");
                return response;
            }

            return ResponseWrapper<AuthorDTO>.WrapToResponce(result.Adapt<AuthorDTO>());
        }

        public ResponseWrapper<AuthorDTO> PutAuthor(int id, AuthorDTO authorDto)
        {
            ResponseWrapper<AuthorDTO> response = new ResponseWrapper<AuthorDTO>(errors: new List<string>());
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
                Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors.Add(ex.Message);
                    return response;

            }

            return ResponseWrapper<AuthorDTO>.WrapToResponce(authorDto);
        }

        public ResponseWrapper<AuthorDTO> PostAuthor(AuthorDTO? authorDto)
        {
            ResponseWrapper<AuthorDTO> response = new(errors: new List<string>());
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
                Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors.Add(ex.Message);
                return response;

            }


            return ResponseWrapper<AuthorDTO>.WrapToResponce(authorDto);
        }

        public async Task<ResponseWrapper<AuthorDTO>> DeleteAuthor(int id)
        {
            ResponseWrapper<AuthorDTO> response = new (errors: new List<string>());
            var author = await Context.Authors.FindAsync(id);
            if (author == null)
            {
                response.Errors.Add("Not found");
                return response;
            }

            Context.Authors.Remove(author);
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                response.Errors.Add(ex.Message);
                return response;

            }

            return ResponseWrapper<AuthorDTO>.WrapToResponce(new AuthorDTO());
        }

        private bool AuthorExists(int id)
        {
            return (Context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

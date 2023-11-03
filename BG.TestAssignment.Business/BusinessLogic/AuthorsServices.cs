using BGNet.TestAssignment.Business.BusinessLogic.Interfaces;
using BGNet.TestAssignment.Business.Validators;
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

        public List<AuthorDTO> GetAuthors()
        {
            return Context.Authors.AsQueryable().Adapt<List<AuthorDTO>>();
        }

        public AuthorDTO? GetAuthor(int id)
        {
            var result = Context.Authors.FirstOrDefault(a => a.Id == id);
            if (result == null) return null;

            return result.Adapt<AuthorDTO>();
        }

        public bool PutAuthor(int id, AuthorDTO authorDto)
        {
            if (id != authorDto.Id)
            {
                return false;
            }

            var author = authorDto.Adapt<Author>();

            AuthorValidator validator = new AuthorValidator();
            var validationResult = validator.Validate(author);

            if (!validationResult.IsValid) return false;


            Context.Entry(author).State = EntityState.Modified;

            try
            {
                Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
                {
                    return false;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool PostAuthor(AuthorDTO? authorDto)
        {
            if (authorDto == null)
            {
                return false;
            }

            var author = authorDto.Adapt<Author>();

            AuthorValidator validator = new AuthorValidator();
            var validationResult = validator.Validate(author);

            if (!validationResult.IsValid) return false;

            Context.Authors.Add(author);
            Context.SaveChanges();

            return true;
        }

        public bool DeleteAuthor(int id)
        {

            if (Context.Authors == null)
            {
                return false;
            }
            var author =  Context.Authors.FindAsync(id).Result;
            if (author == null)
            {
                return false;
            }

            Context.Authors.Remove(author);
            Context.SaveChangesAsync();

            return true;
        }

        private bool AuthorExists(int id)
        {
            return (Context.Authors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

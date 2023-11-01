using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.TestAssignment.Business.BusinessLogic.Interfaces;
using BG.TestAssignment.Business.Validators;
using BG.TestAssignment.Models;
using BG.TestAssignment.DataAccessLayer.DataContext;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace BG.TestAssignment.Business.BusinessLogic
{
    public class AuthorsBL : IAuthorBL
    {
        private BookAuthorsDataContext Context { get; set; }

        public AuthorsBL(BookAuthorsDataContext dbContext)
        {
            Context = dbContext;
        }

        public List<AuthorDTO> GetAuthors()
        {
            return Context.Authors.AsQueryable().Adapt<List<AuthorDTO>>();
        }

        public AuthorDTO GetAuthor(int id)
        {
            var result = Context.Authors.FirstOrDefault(a => a.Id == id);
            if (result == null) return new AuthorDTO();

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

            if (validationResult.IsValid) return false;


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
                    throw;
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

            if (validationResult.IsValid) return false;

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

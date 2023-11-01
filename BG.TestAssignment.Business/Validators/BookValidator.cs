using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.TestAssignment.DataAccess.Entities;
using BG.TestAssignment.Models;
using FluentValidation;

namespace BG.TestAssignment.Business.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.PublishedDate).NotNull().NotEmpty();
            RuleFor(x => x.BookGenre).NotNull().NotEmpty();
            RuleFor(x => x.AuthorId).NotNull().NotEmpty();
        }
    }
}

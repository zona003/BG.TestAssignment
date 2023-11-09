using BGNet.TestAssignment.DataAccess.Entities;
using FluentValidation;

namespace BGNet.TestAssignment.Business.Validators
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty();
            RuleFor(x => x.PublishedDate).NotNull().NotEmpty();
            RuleFor(x => x.BookGenre).NotNull().NotEmpty();
            //RuleFor(x => x.AuthorId).NotNull().NotEmpty();
        }
    }
}

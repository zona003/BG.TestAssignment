using BGNet.TestAssignment.DataAccess.Entities;
using FluentValidation;

namespace BGNet.TestAssignment.Business.Validators
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty();
            RuleFor(x => x.LastName).NotNull().NotEmpty();
            RuleFor(x => x.BirthDate).NotNull().NotEmpty();
        }
    }
}

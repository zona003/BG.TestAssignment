using BGNet.TestAssignment.Models;
using FluentValidation;

namespace BGNet.TestAssignment.Business.Validators
{
    public class RegisterValidator: AbstractValidator<RegisterRequest>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.FirstName).NotNull().NotEmpty().Length(1, 250);
            RuleFor(x => x.LastName).NotNull().NotEmpty().Length(1, 250);
            RuleFor(x => x.BirthDate).NotNull().NotEmpty();
            RuleFor(x => x.Address).NotNull().NotEmpty();
        }
    }
}

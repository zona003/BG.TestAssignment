using BGNet.TestAssignment.Models;
using FluentValidation;

namespace BGNet.TestAssignment.Business.Validators
{
    public class LoginValidator : AbstractValidator<AuthRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UserName).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}

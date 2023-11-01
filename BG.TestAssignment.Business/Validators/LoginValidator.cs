using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.TestAssignment.Models;
using FluentValidation;

namespace BG.TestAssignment.Business.Validators
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

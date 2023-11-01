using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BG.TestAssignment.Models;
using FluentValidation;

namespace BG.TestAssignment.Business.Validators
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

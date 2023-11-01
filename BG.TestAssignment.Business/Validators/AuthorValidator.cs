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

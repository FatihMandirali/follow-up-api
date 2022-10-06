using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Follow_Up.Application.Features.Commands.PostLogin
{
    public class PostLoginQueryValidator : AbstractValidator<PostLoginQuery>
    {
        public PostLoginQueryValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().NotNull().WithMessage("Lütfen email alanını uygun doldurun");
        }
    }
}

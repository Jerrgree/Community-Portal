using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPortal.Models
{
    public class CreateUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(e => e.UserName)
                .NotEmpty();

            RuleFor(e => e.Password)
                .NotEmpty();
        }
    }
}

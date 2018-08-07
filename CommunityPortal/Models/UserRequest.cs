using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPortal.Models
{
    public class UserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(e => e.UserName)
                .NotEmpty();

            RuleFor(e => e.Password)
                .NotEmpty();
        }
    }
}
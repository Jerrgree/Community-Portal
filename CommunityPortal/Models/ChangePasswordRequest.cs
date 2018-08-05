using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace CommunityPortal.Models
{
    public class ChangePasswordRequest
    {
        public string UserName { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordRequestValidator : AbstractValidator<ChangePasswordRequest>
    {
        public ChangePasswordRequestValidator()
        {
            RuleFor(e => e.UserName)
                .NotEmpty();

            RuleFor(e => e.CurrentPassword)
                .NotEmpty();

            RuleFor(e => e.NewPassword)
                .NotEmpty()
                .Must((request, newPassword) =>
                request.CurrentPassword != newPassword)
                .WithMessage("New password must not match the old password");
        }
    }
}

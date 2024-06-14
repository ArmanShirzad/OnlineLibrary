using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

using OnlineLibrary.Domain.DTOs;
namespace OnlineLibrary.Application.Validators.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("UserName is required");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required");
        }
    }
}

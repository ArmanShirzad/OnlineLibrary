using FluentValidation;
using OnlineLibrary.Domain.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Validators.User
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(user => user.Username).NotEmpty().WithMessage("UserName is required");
            RuleFor(user => user.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(user => user.Password).NotEmpty().WithMessage("password is required");
        }
    }
}

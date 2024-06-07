using OnlineLibrary.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineLibrary.Core.Entities;

namespace OnlineLibrary.Application.Validators.Book
{
    public class CreateBookValidator : AbstractValidator<BookDto>
    {
        public CreateBookValidator()
        {
      
            RuleFor(x => x.ISBN).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.PublishedDate).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.IsAvailable).NotNull();

        }
    }
}

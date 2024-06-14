using FluentValidation;
using OnlineLibrary.Domain.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Validators.Book
{
    public class BookValidator : AbstractValidator<BookDto>
    {
        public BookValidator()
        {

            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.ISBN).NotEmpty();
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.PublishedDate).NotNull().LessThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.IsAvailable).NotNull();

        }
    }
}

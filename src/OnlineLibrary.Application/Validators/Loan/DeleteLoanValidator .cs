using FluentValidation;
using OnlineLibrary.Application.DTOs;
using OnlineLibrary.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Validators.Loan
{
    public class DeleteLoanValidator : AbstractValidator<LoanDto>
    {
        public DeleteLoanValidator()
        {
                
            RuleFor(loan => loan.Id).GreaterThan(0).WithMessage("Loan ID must be greater than zero.");
        }

    }
}

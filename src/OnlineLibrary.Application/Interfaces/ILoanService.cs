using Ardalis.Result;
using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Interfaces
{
    public interface ILoanService
    {
        Task<Result<LoanDto>> BorrowBookAsync(CreateLoanDto createLoanDto);
        Task<Result> ReturnBookAsync(int loanId);
        Task<Result<IEnumerable<LoanDto>>> GetAllLoansAsync();
        Task<Result<LoanDto>> GetLoanByIdAsync(int id);
        Task<Result<IEnumerable<LoanDto>>> GetLoansByUserIdAsync(int userId);
        Task<Result> RemoveLoanAsync(int loanId);
    }
}

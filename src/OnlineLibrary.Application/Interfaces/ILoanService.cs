using Ardalis.Result;

using OnlineLibrary.Application.DTOs;
using OnlineLibrary.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Interfaces
{
    public interface ILoanService
    {
        Task<Result<LoanDto>> BorrowBookAsync(int bookId, int userId);
        Task<Result> ReturnBookAsync(int loanId);
        Task<Result<IEnumerable<LoanDto>>> GetAllLoansAsync();
        Task<Result<LoanDto>> GetLoanByIdAsync(int id);
        Task<Result<IEnumerable<LoanDto>>> GetLoansByUserIdAsync(int userId);
        Task<Result> RemoveLoanAsync(int loanId);
    }
}

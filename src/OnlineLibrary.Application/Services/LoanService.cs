using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using FluentValidation;
using OnlineLibrary.Domain.Interfaces;
using OnlineLibrary.Domain.DTOs;
using Mapster;

using MapsterMapper;

using OnlineLibrary.Domain.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.Application.Services
{
    public class LoanService : ILoanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<LoanDto> _validator;

        public LoanService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<LoanDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<LoanDto>> BorrowBookAsync(CreateLoanDto createLoanDto)
        {
            // Check if the book is available
            var  book = await _unitOfWork.Books.GetByIdAsync(createLoanDto.BookId);
            if (book ==null)
            {
                return Result<LoanDto>.Error("Book not found");
            }

            
            if (!book.IsAvailable)
            {
                return Result<LoanDto>.Error("Book is not available for borrowing");
            }

            // Check if the user exists
            var user= await _unitOfWork.Users.GetByIdAsync(createLoanDto.UserId);
            if (user == null)
            {
                return Result<LoanDto>.Error("User not found");
            }


            // Create a new loan entity from the DTO
            var loan = new Loan
            {
                BookId = createLoanDto.BookId,
                UserId = createLoanDto.UserId,
                BorrowedDate = DateTime.Now,
                Book = book,
                User = user
            };
            await _unitOfWork.Loans.AddAsync(loan);

            // Update the book availability
            book.IsAvailable = false;
            user.Loans.Add(loan);
            _unitOfWork.Books.Update(book);
            _unitOfWork.Users.Update(user);

            var completeResult = await _unitOfWork.CompleteAsync();
            return completeResult > 0 ? Result<LoanDto>.Success(_mapper.Map<LoanDto>(loan)) : Result<LoanDto>.Error("Failed to borrow book");
        }

        public async Task<Result<LoanDto>> GetLoanByIdAsync(int id)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(id);
            if (loan == null)
            {
                return Result<LoanDto>.Error("Loan not found");
            }

            return Result<LoanDto>.Success(_mapper.Map<LoanDto>(loan));
        }

        public async Task<Result> ReturnBookAsync(int loanId)
        {
            // Get the loan record
            var loan = await _unitOfWork.Loans.GetByIdAsync(loanId);
            if (loan == null)
            {
                return Result.Error("Loan not found");
            }

            loan.ReturnedDate = DateTime.Now;
            _unitOfWork.Loans.Update(loan);

            // Update the book availability
            var book = await _unitOfWork.Books.GetByIdAsync(loan.BookId);
            if (book == null)
            {
                return Result.Error("Book not found");
            }

            book.IsAvailable = true;
            _unitOfWork.Books.Update(book);

            var completeResult = await _unitOfWork.CompleteAsync();
            return completeResult > 0 ? Result.Success() : Result.Error("Failed to return book");
        }

        public async Task<Result<IEnumerable<LoanDto>>> GetAllLoansAsync()
        {
            var loans = await _unitOfWork.Loans.ListAllAsync();
            var loanDtos = loans.Adapt<IEnumerable<LoanDto>>();
            return Result<IEnumerable<LoanDto>>.Success(loanDtos);
        }

        public async Task<Result<IEnumerable<LoanDto>>> GetLoansByUserIdAsync(int userId)
        {
            var loans = await _unitOfWork.Loans.GetLoansByUserIdAsync(userId);
            var loanDtos = loans.Adapt<IEnumerable<LoanDto>>();
            return Result<IEnumerable<LoanDto>>.Success(loanDtos);
        }

        public async Task<Result> RemoveLoanAsync(int loanId)
        {
            var loan = await _unitOfWork.Loans.GetByIdAsync(loanId);
            if (loan == null)
            {
                return Result.Error("Loan not found");
            }

            _unitOfWork.Loans.Delete(loan);
            var completeResult = await _unitOfWork.CompleteAsync();
            return completeResult > 0 ? Result.Success() : Result.Error("Failed to delete loan");
        }
    }
}

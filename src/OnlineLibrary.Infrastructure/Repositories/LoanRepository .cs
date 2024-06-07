using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ardalis.Result;

using Microsoft.EntityFrameworkCore;

using OnlineLibrary.Core.Entities;
using OnlineLibrary.Core.Interfaces;
using OnlineLibrary.Infrastructure.Data;

namespace OnlineLibrary.Infrastructure.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext _context;

        public LoanRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }

        public void Delete(Loan loan)
        {
            _context.Loans.Remove(loan);
        }

        public async Task<Loan> GetByIdAsync(int id)
        {
            return await _context.Loans.FindAsync(id);
        }

        public async Task<IEnumerable<Loan>> ListAllAsync()
        {
            return await _context.Loans.ToListAsync();
        }

        public void Update(Loan loan)
        {
            _context.Loans.Update(loan);
        }

        public async Task<IEnumerable<Loan>> GetLoansByUserIdAsync(int userId)
        {
            return await _context.Loans.Where(l => l.UserId == userId).ToListAsync();
        }


    }
}

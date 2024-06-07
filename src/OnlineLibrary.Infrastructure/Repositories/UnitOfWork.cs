using OnlineLibrary.Core.Interfaces;
using OnlineLibrary.Infrastructure.Data;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext _context;
        private readonly IBookRepository _bookRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepository _loanRepository;

        public UnitOfWork(LibraryDbContext context, IBookRepository bookRepository, IUserRepository userRepository, ILoanRepository loanRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _loanRepository = loanRepository;
        }

        public IBookRepository Books => _bookRepository;
        public IUserRepository Users => _userRepository;
        public ILoanRepository Loans => _loanRepository;

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IBookRepository Books { get; }
        ILoanRepository Loans { get; }
        Task<int> CompleteAsync();
    }
}

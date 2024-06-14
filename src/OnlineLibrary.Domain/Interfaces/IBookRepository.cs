using OnlineLibrary.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetBookByISBNAsync(string isbn);

    }
}

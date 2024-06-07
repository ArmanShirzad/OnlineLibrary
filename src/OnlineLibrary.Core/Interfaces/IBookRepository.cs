using OnlineLibrary.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Core.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> GetBookByISBNAsync(string isbn);

    }
}

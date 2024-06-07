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
    public interface IBookService
    {
        Task<Result<BookDto>> AddBookAsync(BookDto bookdto);
        Task<Result<IEnumerable<BookDto>>> ListAllBooksAsync();  // Ensure this method is defined
        Task<Result<BookDto>> GetBookByIdAsync(int id);
        Task<Result> UpdateBookAsync(BookDto bookdto);
        Task<Result> DeleteBookAsync(int id);

    }
}
    
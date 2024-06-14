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
    public interface IBookService
    {
        Task<Result<BookDto>> AddBookAsync(CreateBookDto bookdto);
        Task<Result<IEnumerable<BookDto>>> ListAllBooksAsync();  // Ensure this method is defined
        Task<Result<BookDto>> GetBookByIdAsync(int id);
        Task<Result> UpdateBookAsync(BookDto bookdto);
        Task<Result> DeleteBookAsync(int id);

    }
}
    
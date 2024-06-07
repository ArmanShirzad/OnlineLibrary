using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.Core.Entities;
using OnlineLibrary.Core.Interfaces;
using OnlineLibrary.Infrastructure.Data;
using Serilog;

namespace OnlineLibrary.Infrastructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        private readonly ILogger _logger;

        public BookRepository(LibraryDbContext context, Serilog.ILogger logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);

            _logger.Information("Book added: {BookTitle}", book.Title);
        }
        



        public async Task<Book> GetBookByISBNAsync(string isbn)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> ListAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }


    }
}

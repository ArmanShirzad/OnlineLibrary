//using Microsoft.EntityFrameworkCore;

//using OnlineLibrary.Domain.Entities;
//using OnlineLibrary.Infrastructure.Data;
//using OnlineLibrary.Infrastructure.Repositories;
//using OnlineLibrary.Infrastructure.Tests.Data;

//using System.Threading.Tasks;

//using Xunit;

//namespace OnlineLibrary.Infrastructure.Tests
//{
//    public class BookRepositoryTests
//    {
//        private readonly LibraryDbContextTests _context;
//        private readonly BookRepository _repository;

//        public BookRepositoryTests()
//        {
//            var options = new DbContextOptionsBuilder<LibraryDbContext>()
//                .UseInMemoryDatabase(databaseName: "TestDatabase")
//                .Options;

//            _context = new LibraryDbContextTests(options);
//            _repository = new BookRepository(_context);
//        }

//        [Fact]
//        public async Task AddBookAsync_ShouldAddBook()
//        {
//            // Arrange
//            var book = new Book { Title = "Test Book", Author = "Author", ISBN = "1234567890" };

//            // Act
//            await _repository.AddAsync(book);
//            await _context.SaveChangesAsync(); // Ensure changes are saved to the database

//            // Assert
//            var retrievedBook = await _repository.GetByIdAsync(book.Id);
//            Assert.NotNull(retrievedBook);
//            Assert.Equal("Test Book", retrievedBook.Value.Title);
//        }

//        [Fact]
//        public async Task GetAllBooksAsync_ShouldReturnBooks()
//        {
//            // Arrange
//            var book1 = new Book { Title = "Test Book 1", Author = "Author 1", ISBN = "1234567891" };
//            var book2 = new Book { Title = "Test Book 2", Author = "Author 2", ISBN = "1234567892" };
//            await _repository.AddAsync(book1);
//            await _repository.AddAsync(book2);
//            await _context.SaveChangesAsync();

//            // Act
//            var books = await _repository.ListAllAsync();

//            // Assert
//            Assert.NotNull(books);
//            Assert.Equal(2, books.Value.Count());
//        }
//    }
//}

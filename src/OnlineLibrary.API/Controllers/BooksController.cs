using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Application.Services;
using Ardalis.Result;
using OnlineLibrary.Domain.Interfaces;

namespace OnlineLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook([FromBody] CreateBookDto bookDto)
        {
            var result = await _bookService.AddBookAsync(bookDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById(int id)
        {
            var result = await _bookService.GetBookByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var result = await _bookService.ListAllBooksAsync();
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookDto bookDto)
        {
            bookDto.Id = id;
            var result = await _bookService.UpdateBookAsync(bookDto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            return HandleResult(result);
        }

        private IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);

            if (result.Status == ResultStatus.NotFound)
                return NotFound(result.Errors);

            return BadRequest(result.Errors);
        }
    }
}

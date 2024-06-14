using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Ardalis.Result;

using FluentValidation;

using Mapster;

using MapsterMapper;

using Microsoft.Extensions.Caching.Distributed;

using Newtonsoft.Json;

using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Domain.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Interfaces;

namespace OnlineLibrary.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IValidator<BookDto> _validator;
        private readonly IValidator<CreateBookDto> _createValidator;

        public BookService(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache cache, IValidator<BookDto> validator, IValidator<CreateBookDto> createvalidator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
            _validator = validator;
            _createValidator = createvalidator;
        }

        public async Task<Result<BookDto>> AddBookAsync(CreateBookDto bookDto)
        {
            var validationResult = await _createValidator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                return Result.Error(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
            }
            var existingBook = await _unitOfWork.Books.GetBookByISBNAsync(bookDto.ISBN);
            if (existingBook != null)
            {
                return Result<BookDto>.Error("A book with the same ISBN already exists.");
            }
            var book = _mapper.Map<Book>(bookDto);
            await _unitOfWork.Books.AddAsync(book);
            var completeResult = await _unitOfWork.CompleteAsync();

            if (completeResult > 0)
            {
                return Result<BookDto>.Success(_mapper.Map<BookDto>(book));
            }
            return Result<BookDto>.Error("Failed to add book"); 
        }

        public async Task<Result<BookDto>> GetBookByIdAsync(int id)
        {
            //var cacheKey = $"Book_{id}";
            //var cachedBook = await _cache.GetStringAsync(cacheKey);
            //if (!string.IsNullOrEmpty(cachedBook))
            //{
            //    return Result<BookDto>.Success(JsonConvert.DeserializeObject<BookDto>(cachedBook));
            //}

            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                return Result<BookDto>.Error("Book not found");
            }

            var bookDto = _mapper.Map<BookDto>(book);
            //await _cache.SetStringAsync(cacheKey, JsonConvert.SerializeObject(bookDto));
            return Result<BookDto>.Success(bookDto);
        }

        public async Task<Result> UpdateBookAsync(BookDto bookDto)
        {
            var validationResult = await _validator.ValidateAsync(bookDto);
            if (!validationResult.IsValid)
            {
                return Result.Error(string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));

            }

            var book = _mapper.Map<Book>(bookDto);
            _unitOfWork.Books.Update(book);
            var completeResult = await _unitOfWork.CompleteAsync();

            return completeResult > 0 ? Result.Success() : Result.Error("Failed to update book");
        }

        public async Task<Result> DeleteBookAsync(int id)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(id);
            if (book == null)
            {
                return Result.Error("Book not found");
            }

            _unitOfWork.Books.Delete(book);
            var completeResult = await _unitOfWork.CompleteAsync();

            return completeResult > 0 ? Result.Success() : Result.Error("Failed to delete book");
        }

        public async Task<Result<IEnumerable<BookDto>>> ListAllBooksAsync()
        {
            var books = await _unitOfWork.Books.ListAllAsync();
            var bookDtos = books.Adapt<IEnumerable<BookDto>>();
            return Result<IEnumerable<BookDto>>.Success(bookDtos);
        }
    }
}

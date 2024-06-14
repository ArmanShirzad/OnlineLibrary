using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using OnlineLibrary.Blazor.Models;

namespace OnlineLibrary.Blazor.Services
{
    public class BookService
    {
        private readonly HttpClient _httpClient;

        public BookService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<BookDto>> GetAllBooksAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<BookDto>>("api/books");
        }

        public async Task<BookDto> GetBookByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<BookDto>($"api/books/{id}");
        }

        public async Task<HttpResponseMessage> CreateBookAsync(BookDto book)
        {
            return await _httpClient.PostAsJsonAsync("api/books", book);
        }

        public async Task<HttpResponseMessage> UpdateBookAsync(BookDto book)
        {
            return await _httpClient.PutAsJsonAsync($"api/books/{book.Id}", book);
        }

        public async Task<HttpResponseMessage> DeleteBookAsync(int id)
        {
            return await _httpClient.DeleteAsync($"api/books/{id}");
        }
    }
}

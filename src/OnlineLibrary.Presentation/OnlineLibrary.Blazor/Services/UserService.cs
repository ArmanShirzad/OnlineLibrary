using OnlineLibrary.Blazor.Models;
using System.Net.Http.Json;

namespace OnlineLibrary.Blazor.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var result = await _httpClient.GetFromJsonAsync<IEnumerable<UserDto>>("api/Users/GetAllUsers");
            return result;

        }

        //public async Task<UserDto> GetUserByIdAsync(int id)
        //{
        //    return await _httpClient.GetFromJsonAsync<UserDto>($"api/Users/{id}");
        //}

        //public async Task<HttpResponseMessage> RegisterUserAsync(UserDto user)
        //{
        //    return await _httpClient.PostAsJsonAsync("api/Users", user);
        //}

        //public async Task<HttpResponseMessage> UpdateUserAsync(UserDto user)
        //{
        //    return await _httpClient.PutAsJsonAsync($"api/Users/{user.Id}", user);
        //}

        //public async Task<HttpResponseMessage> DeleteUserAsync(int id)
        //{
        //    return await _httpClient.DeleteAsync($"api/Users/{id}");
        //}
    }
}

using OnlineLibrary.Blazor.Models;
using System.Net.Http.Json;
using System.Net.Http;

namespace OnlineLibrary.Blazor.Services
{
    public class LoanService
    {
        private readonly HttpClient _httpClient;

        public LoanService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<LoanDto>> GetAllLoansAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<LoanDto>>("api/loans");
        }

        public async Task<LoanDto> GetLoanByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<LoanDto>($"api/loans/{id}");
        }

        public async Task<HttpResponseMessage> CreateLoanAsync(LoanDto loan)
        {
            return await _httpClient.PostAsJsonAsync("api/loans", loan);
        }

        public async Task<HttpResponseMessage> ReturnBookAsync(int loanId)
        {
            return await _httpClient.PutAsJsonAsync($"api/loans/{loanId}/return", loanId );
        }
    }

}

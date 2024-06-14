using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Application.Services;
using Ardalis.Result;
using OnlineLibrary.Domain.Interfaces;

namespace OnlineLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanService _loanService;
        private readonly IHttpContextAccessor _httpcontext;
        public LoansController(ILoanService loanService, IHttpContextAccessor httpcontext)
        {
            _loanService = loanService;
            _httpcontext = httpcontext;
        }

        [HttpPost("CreateLoan")]
        public async Task<IActionResult> BorrowBook([FromBody] CreateLoanDto borrowBookDto)
        {

            var result = await _loanService.BorrowBookAsync(borrowBookDto);
            return HandleResult(result);
        }

        [HttpGet("loan/{id}")]
        public async Task<IActionResult> GetLoanById(int id)
        {
            var result = await _loanService.GetLoanByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("gettallloans")]
        public async Task<IActionResult> GetAllLoans()
        {
            var result = await _loanService.GetAllLoansAsync();
            return HandleResult(result);
        }

        [HttpPut("returnloan/{id}")]
        public async Task<IActionResult> ReturnBook(int loanId)
        {
            var result = await _loanService.ReturnBookAsync(loanId);
            return HandleResult(result);
        }

        [HttpGet("alluserloans/{userId}")]
        public async Task<IActionResult> GetLoansByUserId(int userId)
        {
            var result = await _loanService.GetLoansByUserIdAsync(userId);
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

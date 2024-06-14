using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Application.Services;
using OnlineLibrary.Domain.Interfaces;
using Ardalis.Result;

namespace OnlineLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            return HandleResult(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await _userService.AuthenticateUserAsync(username,password);
            return HandleResult(result);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateUserDto userDto)
        {
            var result = await _userService.UpdateUserAsync(userDto);
            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUserAsync(id);
            return HandleResult(result);
        }
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            return HandleResult(result);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;
using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Application.Services;
using OnlineLibrary.Domain.Interfaces;
using Ardalis.Result;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Application.DTOs;

namespace OnlineLibrary.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        private readonly ICurrentUserService _currentUserService;

        public UsersController(IUserService userService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _currentUserService = currentUserService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateUserDto userDto)
        {
            var result = await _userService.RegisterUserAsync(userDto);
            return HandleResult(result);

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto user)
        {
            var result = await _userService.AuthenticateUserAsync(user);
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
        [HttpGet("userinfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            if (!await _currentUserService.IsUserAuthenticatedAsync())
            {
                return Unauthorized("User is not authenticated.");
            }

            var userId = await _currentUserService.GetUserIdAsync();
            if (userId == null)
            {
                return NotFound("User ID not found.");
            }

            var user = await _currentUserService.GetUserByIdAsync(int.Parse(userId));
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var userInfo = new UserInfo
            {
                 Id = user.Id,
                Username = user.Username,
                isAuthenticated= true
            };

            return Ok(userInfo);
        }
    }
}

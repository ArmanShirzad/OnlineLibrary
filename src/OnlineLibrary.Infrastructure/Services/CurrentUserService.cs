using Microsoft.AspNetCore.Http;

using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Domain.Interfaces;

using System.Security.Claims;
using System.Threading.Tasks;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserService _userService;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<UserDto> GetUserByIdAsync(int userId)
    {
        return await _userService.GetUserByIdAsync(userId);


    }

    public Task<string> GetUserIdAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Task.FromResult(userId);
    }

    public Task<bool> IsUserAuthenticatedAsync()
    {
        var isAuthenticated = _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated;
        return Task.FromResult(isAuthenticated ?? false);
    }
}

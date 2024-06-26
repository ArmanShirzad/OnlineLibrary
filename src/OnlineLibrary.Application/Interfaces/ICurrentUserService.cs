using OnlineLibrary.Domain.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Task<string> GetUserIdAsync();  // Retrieves the current user's ID
        Task<bool> IsUserAuthenticatedAsync();
        Task<UserDto> GetUserByIdAsync(int userId); // Add this method to get user details

    }
}

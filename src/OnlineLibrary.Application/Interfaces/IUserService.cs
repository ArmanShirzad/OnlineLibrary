using Ardalis.Result;

using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Domain.Interfaces 
{
    public interface IUserService
    {
        Task<Result<UserDto>> RegisterUserAsync(CreateUserDto user);
        Task<Result<UserDto>> AuthenticateUserAsync(UserLoginDto userDto);
        Task<Result<UserDto>> GetUserByIdAsync(int id);
        Task<Result> UpdateUserAsync(UpdateUserDto user);
        Task<Result> DeleteUserAsync(int id);
        Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync();

    }
}

using Ardalis.Result;

using OnlineLibrary.Application.DTOs;
using OnlineLibrary.Core.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Application.Interfaces 
{
    public interface IUserService
    {
        Task<Result<UserDto>> RegisterUserAsync(UserDto user);
        Task<Result<UserDto>> AuthenticateUserAsync(string username, string password);
        Task<Result<UserDto>> GetUserByIdAsync(int id);
        Task<Result> UpdateUserAsync(UserDto user);
        Task<Result> DeleteUserAsync(int id);
    }
}

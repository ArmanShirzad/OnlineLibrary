using System.Linq;
using System.Threading.Tasks;

using Ardalis.Result;

using FluentValidation;

using MapsterMapper;

using OnlineLibrary.Application.DTOs;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Core.Entities;
using OnlineLibrary.Core.Interfaces;

namespace OnlineLibrary.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UserDto> _validator;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UserDto> validator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<UserDto>> RegisterUserAsync(UserDto userDto)
        {
            var validationResult = await _validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                return Result<UserDto>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList().ToString());
            }

            var user = _mapper.Map<User>(userDto);
            await _unitOfWork.Users.AddAsync(user);

            var completeResult = await _unitOfWork.CompleteAsync();
            if (completeResult <= 0)
            {
                return Result<UserDto>.Error("Failed to register user");
            }

            var registeredUserDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(registeredUserDto);
        }

        public async Task<Result<UserDto>> AuthenticateUserAsync(string username, string password)
        {
            var users = await _unitOfWork.Users.ListAllAsync();
            var user = users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user == null)
            {
                return Result<UserDto>.Error("Invalid credentials");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto);
        }

        public async Task<Result<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return Result<UserDto>.Error("User not found");
            }

            var userDto = _mapper.Map<UserDto>(user);
            return Result<UserDto>.Success(userDto);
        }

        public async Task<Result> UpdateUserAsync(UserDto userDto)
        {
            var validationResult = await _validator.ValidateAsync(userDto);
            if (!validationResult.IsValid)
            {
                return Result.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList().ToString());
            }

            var user = _mapper.Map<User>(userDto);
            _unitOfWork.Users.Update(user);

            var completeResult = await _unitOfWork.CompleteAsync();
            return completeResult > 0 ? Result.Success() : Result.Error("Failed to update user");
        }

        public async Task<Result> DeleteUserAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null)
            {
                return Result.Error("User not found");
            }

            _unitOfWork.Users.Delete(user);
            var completeResult = await _unitOfWork.CompleteAsync();
            return completeResult > 0 ? Result.Success() : Result.Error("Failed to delete user");
        }
    }
}

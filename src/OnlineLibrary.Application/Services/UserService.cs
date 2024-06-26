using System.Linq;
using System.Threading.Tasks;
using Ardalis.Result;
using FluentValidation;
using Mapster;
using MapsterMapper;
using OnlineLibrary.Domain.DTOs;
using OnlineLibrary.Domain.Interfaces;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Domain.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication;

namespace OnlineLibrary.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<UpdateUserDto> _updateValidator;
        private readonly IValidator<CreateUserDto> _createValidator;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateUserDto> validator, IValidator<CreateUserDto> createValidator, IHttpContextAccessor httpContext)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _updateValidator = validator;
            _createValidator = createValidator;
            _httpContext = httpContext;
        }

        public async Task<Result<UserDto>> RegisterUserAsync(CreateUserDto userDto)
        {
            var validationResult = await _createValidator.ValidateAsync(userDto);
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

        public async Task<Result<UserDto>> AuthenticateUserAsync(UserLoginDto userLoginDto)
        {
            var users = await _unitOfWork.Users.ListAllAsync();
            var user = users.FirstOrDefault(u => u.Username == userLoginDto.Username && u.Password == userLoginDto.Password);
            if (user == null)
            {
                return Result<UserDto>.Error("Invalid credentials");
            }
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        // Add other claims as needed
    };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties();
            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

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

        public async Task<Result> UpdateUserAsync(UpdateUserDto userDto)
        {
            var validationResult = await _updateValidator.ValidateAsync(userDto);
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

        public async Task<Result<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _unitOfWork.Users.ListAllAsync();
            var userDtos = users.Adapt<IEnumerable<UserDto>>();
            return Result<IEnumerable<UserDto>>.Success(userDtos);
        }
    }
}

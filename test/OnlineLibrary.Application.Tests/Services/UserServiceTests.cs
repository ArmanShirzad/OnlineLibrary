using OnlineLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Ardalis.Result;
using OnlineLibrary.Domain.Entities;
using OnlineLibrary.Application.Services;
using OnlineLibrary.Domain.Interfaces;

namespace OnlineLibrary.Application.Tests.Services
{
    //public class UserServiceTests
    //{
    //    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    //    private readonly UserService _userService;

    //    public UserServiceTests()
    //    {
    //        _unitOfWorkMock = new Mock<IUnitOfWork>();
    //        _userService = new UserService(_unitOfWorkMock.Object);
    //    }

    //    [Fact]
    //    public async Task RegisterUserAsync_ShouldReturnSuccess_WhenUserIsValid()
    //    {
    //        var user = new User { Id = 1, Username = "testuser", Email = "test@example.com" };
    //        _unitOfWorkMock.Setup(uow => uow.Users.AddAsync(user)).ReturnsAsync(Result<User>.Success(user));
    //        _unitOfWorkMock.Setup(uow => uow.CompleteAsync()).ReturnsAsync(1);

    //        var result = await _userService.RegisterUserAsync(user);

    //        Assert.True(result.IsSuccess);
    //        Assert.Equal(user, result.Value);
    //    }

    //    [Fact]
    //    public async Task AuthenticateUserAsync_ShouldReturnUser_WhenCredentialsAreValid()
    //    {
    //        var users = new List<User> { new User { Username = "testuser", Password = "password" } };
    //        _unitOfWorkMock.Setup(uow => uow.Users.ListAllAsync()).ReturnsAsync(Result<IEnumerable<User>>.Success(users));

    //        var result = await _userService.AuthenticateUserAsync("testuser", "password");

    //        Assert.True(result.IsSuccess);
    //        Assert.Equal(users.First(), result.Value);
    //    }

    //    [Fact]
    //    public async Task AuthenticateUserAsync_ShouldReturnError_WhenCredentialsAreInvalid()
    //    {
    //        var users = new List<User> { new User { Username = "testuser", Password = "password" } };
    //        _unitOfWorkMock.Setup(uow => uow.Users.ListAllAsync()).ReturnsAsync(Result<IEnumerable<User>>.Success(users));

    //        var result = await _userService.AuthenticateUserAsync("testuser", "wrongpassword");

    //        Assert.False(result.IsSuccess);
    //        Assert.Equal("Invalid credentials", result.Errors.First());
    //    }
    //    // other unit test

    //}
    }


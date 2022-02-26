using System.Linq;
using BankApp.Entities.UserTypes;
using BankApp.Exceptions;
using BankApp.Exceptions.RequestExceptions;
using BankApp.Services.UserService;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Xunit;

namespace BankApp.UnitTests;

public class UserServiceTests
{
    private readonly RoleManager<IdentityRole> _fakeRoleManager;
    private readonly UserManager<AppUser> _fakeUserManager;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _fakeRoleManager = A.Fake<RoleManager<IdentityRole>>();
        _fakeUserManager = A.Fake<UserManager<AppUser>>();
        _userService = new UserService(_fakeUserManager, _fakeRoleManager);
    }

    [Fact]
    public void GetAllUsers_NoUsers_ReturnsEmptyCollection()
    {
        // arrange
        var fakeAppUserQueryable = A.CollectionOfDummy<AppUser>(0).AsQueryable();
        A.CallTo(() => _fakeUserManager.Users).Returns(fakeAppUserQueryable);

        // act
        var result = _userService.GetAllUsers().ToList();

        // assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetAllUsers_UsersExist_ReturnsUsersCollection()
    {
        // arrange
        var fakeAppUserQueryable = A.CollectionOfDummy<AppUser>(3).AsQueryable();
        A.CallTo(() => _fakeUserManager.Users).Returns(fakeAppUserQueryable);

        // act
        var result = _userService.GetAllUsers().ToList();

        // assert
        result.Should().HaveSameCount(fakeAppUserQueryable);
    }

    [Fact]
    public async void GetUserByIdAsync_IdExists_ReturnsUser()
    {
        // arrange
        var appUser = A.Dummy<AppUser>();
        A.CallTo(() => _fakeUserManager.FindByIdAsync(A<string>.Ignored)).Returns(appUser);

        // act
        var result = await _userService.GetUserByIdAsync("1");

        // assert
        result.Should().Be(appUser);
    }

    [Fact]
    public async void GetUserByIdAsync_IdDoesntExist_ThrowsNotFoundException()
    {
        // arrange
        AppUser appUser = null!;
        A.CallTo(() => _fakeUserManager.FindByIdAsync(A<string>.Ignored)).Returns(appUser);

        // act
        var action = async () => await _userService.GetUserByIdAsync("1");

        // assert
        await action.Should().ThrowExactlyAsync<NotFoundException>();
    }

    [Fact]
    public async void CreateUserAsync_ForValidData_DoesNotThrowAnyException()
    {
        // arrange
        var appUser = A.Dummy<AppUser>();
        A.CallTo(() => _fakeRoleManager.RoleExistsAsync(A<string>.Ignored)).Returns(true);

        var fakeIdentityResult = IdentityResult.Success;
        A.CallTo(() =>
            _fakeUserManager.CreateAsync(A<AppUser>.Ignored, A<string>.Ignored)).Returns(fakeIdentityResult);

        // act
        var action = async () => await _userService.CreateUserAsync(appUser, "password", "roleName");

        // assert
        await action.Should().NotThrowAsync();
    }

    [Fact]
    public async void CreateUserAsync_RoleNameDoesntExist_ThrowsAppException()
    {
        // arrange
        var appUser = A.Dummy<AppUser>();
        A.CallTo(() => _fakeRoleManager.RoleExistsAsync(A<string>.Ignored)).Returns(false);

        // act
        var action = async () => await _userService.CreateUserAsync(appUser, "password", "roleName");

        // assert
        await action.Should().ThrowExactlyAsync<AppException>();
    }

    [Fact]
    public async void CreateUserAsync_ForInvalidData_ThrowsBadRequestException()
    {
        // arrange
        var appUser = A.Dummy<AppUser>();
        A.CallTo(() => _fakeRoleManager.RoleExistsAsync(A<string>.Ignored)).Returns(true);

        var fakeIdentityResult = IdentityResult.Failed(new IdentityError());
        A.CallTo(() =>
            _fakeUserManager.CreateAsync(A<AppUser>.Ignored, A<string>.Ignored)).Returns(fakeIdentityResult);

        // act
        var action = async () => await _userService.CreateUserAsync(appUser, "password", "roleName");

        // assert
        await action.Should().ThrowExactlyAsync<BadRequestException>();
    }

    [Fact]
    public async void DeleteUserByIdAsync_IdDoesntExist_ThrowsNotFoundException()
    {
        // arrange
        AppUser appUser = null!;
        A.CallTo(() => _fakeUserManager.FindByIdAsync(A<string>.Ignored)).Returns(appUser);

        // act
        var action = async () => await _userService.DeleteUserByIdAsync("1");

        // assert
        await action.Should().ThrowExactlyAsync<NotFoundException>();
    }

    [Fact]
    public async void DeleteUserAsync_CallsUserManagerDeleteAsyncMethod()
    {
        // arrange
        AppUser appUser = A.Dummy<AppUser>();

        // act
        await _userService.DeleteUserAsync(appUser);

        // assert
        A.CallTo(() => _fakeUserManager.DeleteAsync(A<AppUser>.Ignored)).MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async void DeleteUserByIdAsync_IdExists_CallsUserManagerDeleteAsyncMethod()
    {
        // arrange
        AppUser appUser = A.Dummy<AppUser>();
        A.CallTo(() => _fakeUserManager.FindByIdAsync(A<string>.Ignored)).Returns(appUser);

        // act
        await _userService.DeleteUserByIdAsync("1");

        // assert
        A.CallTo(() => _fakeUserManager.DeleteAsync(A<AppUser>.Ignored)).MustHaveHappenedOnceExactly();
    }
}
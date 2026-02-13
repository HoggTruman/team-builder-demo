using API.DTOs.Account;
using API.Models.User;
using API.Services;
using Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class AccountControllerTests
{
    private readonly Mock<UserManager<AppUser>> _userManagerStub;
    private readonly Mock<SignInManager<AppUser>> _signInManagerStub;
    private readonly Mock<ITokenService> _tokenServiceStub;

    public AccountControllerTests()
    {
        _userManagerStub = MockHelpers.CreateMockUserManager();
        _signInManagerStub = MockHelpers.CreateMockSignInManager();
        _tokenServiceStub = new();
    }




    [Fact]
    public async void Login_WithInvalidModelState_ReturnsBadRequest()
    {
        // Arrange
        LoginDTO loginDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword"            
        };

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );

        accountController.ModelState.AddModelError("test", "test");


        // Act
        var result = await accountController.Login(loginDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }


    [Fact]
    public async void Login_WithoutUserNameMatch_ReturnsUnauthorized()
    {
        // Arrange
        LoginDTO loginDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword"            
        };

        _userManagerStub.Setup(x => x.FindByNameAsync(loginDTO.UserName))
            .Returns(Task.FromResult<AppUser?>(null));

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );


        // Act
        var result = await accountController.Login(loginDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }


    [Fact]
    public async void Login_WithInvalidPassword_ReturnsUnauthorized()
    {
        // Arrange
        LoginDTO loginDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword"            
        };

        AppUser testAppUser = new()
        {
            UserName = loginDTO.UserName,
        };

        _userManagerStub.Setup(x => x.FindByNameAsync(loginDTO.UserName))
            .Returns(Task.FromResult<AppUser?>(testAppUser));

        _signInManagerStub.Setup(x => x.CheckPasswordSignInAsync(testAppUser, loginDTO.Password, false))
            .Returns(Task.FromResult(SignInResult.Failed));

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );


        // Act
        var result = await accountController.Login(loginDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }


    [Fact]
    public async void Login_WithValidLogin_ReturnsOk()
    {
        // Arrange
        LoginDTO loginDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword"            
        };

        AppUser testAppUser = new()
        {
            UserName = loginDTO.UserName,
        };

        _userManagerStub.Setup(x => x.FindByNameAsync(loginDTO.UserName))
            .Returns(Task.FromResult<AppUser?>(testAppUser));

        _signInManagerStub.Setup(x => x.CheckPasswordSignInAsync(testAppUser, loginDTO.Password, false))
            .Returns(Task.FromResult(SignInResult.Success));

        _tokenServiceStub.Setup(x => x.CreateToken(testAppUser))
            .Returns("testTokenString");

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );


        // Act
        var result = await accountController.Login(loginDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }




    [Fact]
    public async void Register_WithInvalidModelState_ReturnsBadRequest()
    {
        // Arrange 
        RegisterDTO registerDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword",
            ConfirmPassword = "testPassword"            
        };

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );

        accountController.ModelState.AddModelError("test", "test");


        // Act
        var result = await accountController.Register(registerDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }


    [Fact]
    public async void Register_WithUserCreationFailure_ReturnsServerError()
    {
        // Arrange 
        RegisterDTO registerDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword",
            ConfirmPassword = "testPassword"            
        };

        _userManagerStub.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), registerDTO.Password))
            .Returns(Task.FromResult(IdentityResult.Failed()));

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );


        // Act
        var result = await accountController.Register(registerDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }


    [Fact]
    public async void Register_WithAddToRoleFailure_ReturnsServerError()
    {
        // Arrange 
        RegisterDTO registerDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword",
            ConfirmPassword = "testPassword"            
        };

        _userManagerStub.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), registerDTO.Password))
            .Returns(Task.FromResult(IdentityResult.Success));

        _userManagerStub.Setup(x => x.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .Returns(Task.FromResult(IdentityResult.Failed()));

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );


        // Act
        var result = await accountController.Register(registerDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status500InternalServerError);
    }


    [Fact]
    public async void Register_WithEverythingValid_ReturnsOk()
    {
        // Arrange 
        RegisterDTO registerDTO = new()
        {
            UserName = "testUserName",
            Password = "testPassword",
            ConfirmPassword = "testPassword"            
        };

        _userManagerStub.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), registerDTO.Password))
            .Returns(Task.FromResult(IdentityResult.Success));

        _userManagerStub.Setup(x => x.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
            .Returns(Task.FromResult(IdentityResult.Success));

        _tokenServiceStub.Setup(x => x.CreateToken(It.IsAny<AppUser>()))
            .Returns("testTokenString");

        var accountController = new AccountController(
            _userManagerStub.Object, 
            _signInManagerStub.Object, 
            _tokenServiceStub.Object
        );


        // Act
        var result = await accountController.Register(registerDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }
}
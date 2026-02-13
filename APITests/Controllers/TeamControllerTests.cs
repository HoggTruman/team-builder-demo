using API.Controllers;
using API.DTOs.Team;
using API.Models.User;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using System.Security.Claims;

namespace APITests.Controllers;

public class TeamControllerTests
{
    private readonly Mock<ITeamRepository> _repositoryStub;
    private readonly Mock<UserManager<AppUser>> _mockUserManager;
    
    public TeamControllerTests()
    {
        _repositoryStub = new Mock<ITeamRepository>();
        _mockUserManager = MockHelpers.CreateMockUserManager();
    }
    

    private const string TestGivenName = "TestUserName";

    private readonly ControllerContext testControllerContext = new ControllerContext()
    {
        HttpContext = new DefaultHttpContext()
        { 
            User = new ClaimsPrincipal(
                new ClaimsIdentity(
                    [ 
                        new Claim(ClaimTypes.GivenName, TestGivenName)
                    ]
                )
            )
        }
    };




    [Fact]
    public async void GetTeams_withValidUser_ReturnsOk()
    {
        // Arrange
        AppUser testAppUser = new();

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.GetTeams(testAppUser.Id))
            .Returns(new List<Team>());

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.GetTeams();
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        testAppUser.Should().NotBeNull();
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public async void GetTeams_withInvalidUser_ReturnsUnauthorized()
    {
        // This code is just for cases where a user is deleted from the db but still has a valid token.
        // In regular use, this endpoint is inaccessible to invalid users
        
        // Arrange
        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(null)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.GetTeams();
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }




    [Theory]
    [InlineData(1)]
    public async void GetTeamById_WithValidId_ReturnsOk(int testId)
    {
        // Arrange
        AppUser testAppUser = new();
        Team testTeam = new();

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.GetTeamById(testId, testAppUser.Id))
            .Returns(testTeam);

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act 
        var result = await teamController.GetTeamById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Theory]
    [InlineData(1)]
    public async void GetTeamById_WithInvalidId_ReturnsNotFound(int testId)
    {
        // Arrange
        AppUser testAppUser = new();

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.GetTeamById(testId, testAppUser.Id))
            .Returns((Team?)null);

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act 
        var result = await teamController.GetTeamById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }


    [Theory]
    [InlineData(1)]
    public async void GetTeamById_WithInvalidUser_ReturnsUnauthorized(int testId)
    {
        // Arrange
        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(null)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act 
        var result = await teamController.GetTeamById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }




    [Fact]
    public async void CreateTeam_WithValidModelState_ReturnsCreated()
    {
        // Arrange 
        AppUser testAppUser = new();
        Team testTeam = new();
        CreateUpdateTeamDTO createTeamDTO = new() { TeamName = "TestTeam" };

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.CreateTeam(createTeamDTO, testAppUser.Id))
            .Returns(testTeam);

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.CreateTeam(createTeamDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status201Created);
    }


    [Fact]
    public async void CreateTeam_WithInvalidModelState_ReturnsBadRequest()
    {
        // Arrange 
        AppUser testAppUser = new();
        CreateUpdateTeamDTO createTeamDTO = new() { TeamName = "TestTeam" };


        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;
        teamController.ModelState.AddModelError("test", "test");


        // Act
        var result = await teamController.CreateTeam(createTeamDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert 
        _repositoryStub.Verify(x => x.CreateTeam(createTeamDTO, testAppUser.Id), Times.Never());
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }


    [Fact]
    public async void CreateTeam_WithInvalidUser_ReturnsUnauthorized()
    {
        // Arrange 
        CreateUpdateTeamDTO createTeamDTO = new() { TeamName = "TestTeam" };


        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(null)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.CreateTeam(createTeamDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }




    [Fact]
    public async void UpdateTeam_WithValidIdValidModelState_ReturnsOk()
    {
        // Arrange 
        AppUser testAppUser = new();
        Team testTeam = new();
        CreateUpdateTeamDTO updateTeamDTO = new() { TeamName = "TestTeam" };

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.UpdateTeamById(testTeam.Id, updateTeamDTO, testAppUser.Id))
            .Returns(testTeam);

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.UpdateTeam(testTeam.Id, updateTeamDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public async void UpdateTeam_WithInvalidModelState_ReturnsBadRequest()
    {
        // Arrange 
        AppUser testAppUser = new();
        Team testTeam = new();
        CreateUpdateTeamDTO updateTeamDTO = new() { TeamName = "TestTeam" };

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;
        teamController.ModelState.AddModelError("test", "test");


        // Act
        var result = await teamController.UpdateTeam(testTeam.Id, updateTeamDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        _repositoryStub.Verify(
            x => x.UpdateTeamById(It.IsAny<int>(), It.IsAny<CreateUpdateTeamDTO>(), It.IsAny<string>()), 
            Times.Never()
        );
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }


    [Fact]
    public async void UpdateTeam_WithInvalidId_ReturnsNotFound()
    {
        // Arrange 
        AppUser testAppUser = new();
        Team testTeam = new();
        CreateUpdateTeamDTO updateTeamDTO = new() { TeamName = "TestTeam" };

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.UpdateTeamById(testTeam.Id, updateTeamDTO, testAppUser.Id))
            .Returns((Team?)null);

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.UpdateTeam(testTeam.Id, updateTeamDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }


    [Fact]
    public async void UpdateTeam_WithInvalidUser_ReturnsUnauthorized()
    {
        // Arrange 
        Team testTeam = new();
        CreateUpdateTeamDTO updateTeamDTO = new() { TeamName = "TestTeam" };

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(null)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.UpdateTeam(testTeam.Id, updateTeamDTO);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }




    [Fact]
    public async void DeleteTeam_WithValidId_ReturnsNoContent()
    {
        // Arrange
        AppUser testAppUser = new();
        Team testTeam = new();

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.DeleteTeamById(testTeam.Id, testAppUser.Id))
            .Returns(testTeam);

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.DeleteTeam(testTeam.Id);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status204NoContent);
    }


    [Fact]
    public async void DeleteTeam_WithInvalidId_ReturnsNotFound()
    {
        // Arrange
        AppUser testAppUser = new();
        int testId = 0;

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.DeleteTeamById(testId, testAppUser.Id))
            .Returns((Team?)null);

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.DeleteTeam(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }


    [Fact]
    public async void DeleteTeam_WithInvalidUser_ReturnsUnauthorized()
    {
        // Arrange
        int testId = 0;

        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(null)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.DeleteTeam(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert
        _repositoryStub.Verify(
            x => x.DeleteTeamById(It.IsAny<int>(), It.IsAny<string>()), 
            Times.Never()
        );
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }




    [Fact]
    public async void CreateUpdateTeams_WithValidInput_ReturnsOk()
    {
        // Arrange 
        AppUser testAppUser = new();
        List<CreateTeamsDTO> teamDTOs = new();


        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        _repositoryStub.Setup(x => x.CreateTeams(teamDTOs, testAppUser.Id))
            .Returns(new List<Team>());

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.CreateTeams(teamDTOs);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert 
        _repositoryStub.Verify(x => x.CreateTeams(teamDTOs, testAppUser.Id), Times.Once());
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public async void CreateTeams_WithInvalidModelState_ReturnsBadRequest()
    {
        // Arrange 
        AppUser testAppUser = new();
        List<CreateTeamsDTO> teamDTOs = new();


        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(testAppUser)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;
        teamController.ModelState.AddModelError("test", "test");


        // Act
        var result = await teamController.CreateTeams(teamDTOs);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert 
        _repositoryStub.Verify(x => x.CreateTeams(teamDTOs, testAppUser.Id), Times.Never());
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status400BadRequest);
    }


    [Fact]
    public async void CreateUpdateTeams_WithInvalidUser_ReturnsUnauthorized()
    {
        // Arrange 
        List<CreateTeamsDTO> teamDTOs = new();


        _mockUserManager.Setup(x => x.FindByNameAsync(TestGivenName))
            .Returns(Task.FromResult<AppUser?>(null)); 

        var teamController = new TeamController(_repositoryStub.Object, _mockUserManager.Object);
        teamController.ControllerContext = testControllerContext;


        // Act
        var result = await teamController.CreateTeams(teamDTOs);
        var statusCodeResult = (IStatusCodeActionResult)result;


        // Assert 
        _repositoryStub.Verify(x => x.CreateTeams(teamDTOs, It.IsAny<string>()), Times.Never());
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status401Unauthorized);
    }

}
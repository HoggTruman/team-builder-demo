using API.Controllers;
using API.Models.Static;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class MoveControllerTests
{
    private readonly Mock<IMoveRepository> _repositoryStub;

    public MoveControllerTests()
    {
        _repositoryStub = new Mock<IMoveRepository>();
    }

    List<Move> testMoves = TestData.Moves;


    [Fact]
    public void GetAll_WithNoMoves_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetAll())
            .Returns(new List<Move>());

        var controller = new MoveController(_repositoryStub.Object);

        // Act
        var result = controller.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public void GetAll_WithMoves_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetAll())
            .Returns(testMoves);

        var controller = new MoveController(_repositoryStub.Object);

        // Act
        var result = controller.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Theory]
    [InlineData(1)]
    public void GetMovesByPokemonId_WithMatches_ReturnsOk(int testPokemonId)
    {
        // Arrange 
        _repositoryStub.Setup(repo => repo.GetMovesByPokemonId(testPokemonId))
            .Returns(testMoves);

        var controller = new MoveController(_repositoryStub.Object);

        // Act
        var result = controller.GetMovesByPokemonId(testPokemonId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Theory]
    [InlineData(1)]
    public void GetMovesByPokemonId_WithoutMatches_ReturnsNotFound(int testPokemonId)
    {
        // Arrange 
        _repositoryStub.Setup(repo => repo.GetMovesByPokemonId(testPokemonId))
            .Returns((List<Move>?)null);

        var controller = new MoveController(_repositoryStub.Object);

        // Act
        var result = controller.GetMovesByPokemonId(testPokemonId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
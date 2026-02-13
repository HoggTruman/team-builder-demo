using API.Controllers;
using API.Models.Static;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class PokemonControllerTests
{
    private readonly Mock<IPokemonRepository> _repositoryStub;

    public PokemonControllerTests()
    {
        _repositoryStub = new Mock<IPokemonRepository>();
    }

    private readonly List<Pokemon> testPokemon = TestData.Pokemon;




    [Fact]
    public void GetAll_WithPokemonInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(testPokemon);
        var pokemonController = new PokemonController(_repositoryStub.Object);

        // Act
        var result = pokemonController.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public void GetAll_WithNoPokemonInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(new List<Pokemon>());
        var pokemonController = new PokemonController(_repositoryStub.Object);

        // Act
        var result = pokemonController.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Theory]
    [InlineData(1)]
    public void GetById_WithMatch_ReturnsOk(int testId)
    {
        // Arrange
        var testPoke = new Pokemon
        {
            Id = 1,
            Identifier = "test",
            SpeciesId = 1,
            BaseStats = new BaseStats(), // This is needed here to prevent the mapper from throwing a null reference exception
        };

        _repositoryStub.Setup(x => x.GetById(testId)).Returns(testPoke);
        var pokemonController = new PokemonController(_repositoryStub.Object);

        // Act
        var result = pokemonController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Theory]
    [InlineData(-1)]
    public void GetById_WithoutMatch_ReturnsNotFound(int testId)
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetById(testId))
            .Returns((Pokemon?)null);
        var pokemonController = new PokemonController(_repositoryStub.Object);

        // Act
        var result = pokemonController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
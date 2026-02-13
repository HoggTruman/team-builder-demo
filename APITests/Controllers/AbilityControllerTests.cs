using API.Controllers;
using API.Models.Static;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class AbilityControllerTests
{
    private readonly Mock<IAbilityRepository> _repositoryStub;

    public AbilityControllerTests()
    {
        _repositoryStub = new Mock<IAbilityRepository>();
    }

    List<Ability> testAbilities = TestData.Abilities;


    [Fact]
    public void GetAll_WithAbilitiesInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(testAbilities);
        var abilityController = new AbilityController(_repositoryStub.Object);

        // Act
        var result = abilityController.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public void GetAll_WithNoAbilitiesInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(new List<Ability>());
        var abilityController = new AbilityController(_repositoryStub.Object);

        // Act
        var result = abilityController.GetAll();
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
        _repositoryStub.Setup(x => x.GetById(testId)).Returns(new Ability());
        var abilityController = new AbilityController(_repositoryStub.Object);

        // Act
        var result = abilityController.GetById(testId);
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
            .Returns((Ability?)null);
        var abilityController = new AbilityController(_repositoryStub.Object);

        // Act
        var result = abilityController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

}
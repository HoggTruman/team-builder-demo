using API.Controllers;
using API.Models.Static;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class PkmnTypeControllerTests
{
    private readonly Mock<IPkmnTypeRepository> _repositoryStub;

    public PkmnTypeControllerTests()
    {
        _repositoryStub = new Mock<IPkmnTypeRepository>();
    }

    private readonly List<PkmnType> testPkmnTypes = TestData.PkmnTypes;




    [Fact]
    public void GetAll_WithPkmnTypesInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(testPkmnTypes);
        var pkmnTypeController = new PkmnTypeController(_repositoryStub.Object);

        // Act
        var result = pkmnTypeController.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public void GetAll_WithNoPkmnTypesInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(new List<PkmnType>());
        var pkmnTypeController = new PkmnTypeController(_repositoryStub.Object);

        // Act
        var result = pkmnTypeController.GetAll();
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
        _repositoryStub.Setup(x => x.GetById(testId)).Returns(testPkmnTypes[0]);
        var pkmnTypeController = new PkmnTypeController(_repositoryStub.Object);

        // Act
        var result = pkmnTypeController.GetById(testId);
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
            .Returns((PkmnType?)null);
        var pkmnTypeController = new PkmnTypeController(_repositoryStub.Object);

        // Act
        var result = pkmnTypeController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

}
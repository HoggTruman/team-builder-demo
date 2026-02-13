using API.Controllers;
using API.Models.Static;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class NatureControllerTests
{
    private readonly Mock<INatureRepository> _repositoryStub;

    public NatureControllerTests()
    {
        _repositoryStub = new Mock<INatureRepository>();
    }

    List<Nature> testNatures = TestData.Natures;


    [Fact]
    public void GetAll_WithNaturesInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(testNatures);
        var natureController = new NatureController(_repositoryStub.Object);

        // Act
        var result = natureController.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public void GetAll_WithNoNaturesInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(new List<Nature>());
        var natureController = new NatureController(_repositoryStub.Object);

        // Act
        var result = natureController.GetAll();
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
        _repositoryStub.Setup(x => x.GetById(testId)).Returns(new Nature());
        var natureController = new NatureController(_repositoryStub.Object);

        // Act
        var result = natureController.GetById(testId);
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
            .Returns((Nature?)null);
        var natureController = new NatureController(_repositoryStub.Object);

        // Act
        var result = natureController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

}
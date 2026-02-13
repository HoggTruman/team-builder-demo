using API.Controllers;
using API.Models.Static;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class GenderControllerTests
{
    private readonly Mock<IGenderRepository> _repositoryStub;

    public GenderControllerTests()
    {
        _repositoryStub = new Mock<IGenderRepository>();
    }

    List<Gender> testGenders = TestData.Genders;


    [Fact]
    public void GetAll_WithGendersInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(testGenders);
        var genderController = new GenderController(_repositoryStub.Object);

        // Act
        var result = genderController.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Fact]
    public void GetAll_WithNoGendersInDb_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(x => x.GetAll()).Returns(new List<Gender>());
        var genderController = new GenderController(_repositoryStub.Object);

        // Act
        var result = genderController.GetAll();
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
        _repositoryStub.Setup(x => x.GetById(testId)).Returns(new Gender());
        var genderController = new GenderController(_repositoryStub.Object);

        // Act
        var result = genderController.GetById(testId);
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
            .Returns((Gender?)null);
        var genderController = new GenderController(_repositoryStub.Object);

        // Act
        var result = genderController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert 
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }

}
using API.Controllers;
using API.Models.Static;
using API.Repository;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;

namespace APITests.Controllers;

public class ItemControllerTests
{
    private readonly Mock<IItemRepository> _repositoryStub;

    public ItemControllerTests()
    {
        _repositoryStub = new Mock<IItemRepository>();
    }

    List<Item> testItems = TestData.Items;


    [Fact]
    public void GetAll_WithItemsInRepo_ReturnsOk()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetAll()).Returns(testItems);
        var itemController = new ItemController(_repositoryStub.Object);

        // Act 
        var result = itemController.GetAll();
        var statusCodeResult = (IStatusCodeActionResult)result;
        

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetById_WithIdMatchingItem_ReturnsOk(int testId)
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetById(testId))
            .Returns(testItems.FirstOrDefault(x => x.Id == testId));
        var itemController = new ItemController(_repositoryStub.Object);        
        
        // Act
        var result = itemController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status200OK);
    }


    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1000)]
    public void GetById_WithIdNotMatchingItem_ReturnsNotFound(int testId)
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetById(testId))
            .Returns((Item?)null);
        var itemController = new ItemController(_repositoryStub.Object);        
        
        // Act
        var result = itemController.GetById(testId);
        var statusCodeResult = (IStatusCodeActionResult)result;

        // Assert
        result.Should().NotBeNull();
        statusCodeResult.StatusCode.Should().Be(StatusCodes.Status404NotFound);
    }
}
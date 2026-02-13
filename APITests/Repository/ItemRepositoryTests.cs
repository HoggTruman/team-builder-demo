using API.Data;
using API.Models.Static;
using API.Repository;
using FluentAssertions;

namespace APITests.Repository;

public class ItemRepositoryTests
{
    private readonly ApplicationDbContext _testDbContext;

    public ItemRepositoryTests()
    {
        _testDbContext = Utility.CreateTestDbContext();
    }


    [Fact]
    public void GetAll_WithItemsInDb_ReturnsListOfItems()
    {
        // Arrange
        Utility.AddTestData(_testDbContext);

        var itemRepository = new ItemRepository(_testDbContext);

        // Act
        var result = itemRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(TestData.Items.Count);
    }


    [Fact]
    public void GetAll_WithoutItemsInDb_ReturnsEmptyList()
    {
        // Arrange
        var expectedResult = new List<Item>();

        var itemRepository = new ItemRepository(_testDbContext);

        // Act
        var result = itemRepository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void GetById_WithMatchingId_ReturnsItem(int testId)
    {
        // Arrange
        Utility.AddTestData(_testDbContext);

        var itemRepository = new ItemRepository(_testDbContext);

        // Act
        var result = itemRepository.GetById(testId)!;

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(testId);
    }

    

    [Theory]
    [InlineData(-1)]
    [InlineData(1000)]
    public void GetById_WithoutMatchingId_ReturnsNull(int testId)
    {
        // Arrange
        Utility.AddTestData(_testDbContext);

        var itemRepository = new ItemRepository(_testDbContext);

        // Act
        var result = itemRepository.GetById(testId);

        // Assert
        result.Should().BeNull();
    }
}
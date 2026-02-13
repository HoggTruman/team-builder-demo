using API.Data;
using API.Models.Static;
using API.Repository;
using FluentAssertions;

namespace APITests.Repository;

public class NatureRepositoryTests
{
    private readonly ApplicationDbContext _testDbContext;

    public NatureRepositoryTests()
    {
        _testDbContext = Utility.CreateTestDbContext();
    }

    
    [Fact]
    public void GetAll_WithNaturesInDb_ReturnsListOfNatures()
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var natureRepository = new NatureRepository(_testDbContext);

        // Act
        var result = natureRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(TestData.Natures.Count);
    }


    [Fact]
    public void GetAll_WithNoNaturesInDb_ReturnsListOfNatures()
    {
        // Arrange 
        var expectedResult = new List<Nature>();
        var natureRepository = new NatureRepository(_testDbContext);

        // Act
        var result = natureRepository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [InlineData(1)]
    public void GetById_WithMatch_ReturnsNature(int testId)
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var natureRepository = new NatureRepository(_testDbContext);

        // Act
        var result = natureRepository.GetById(testId)!;

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(testId);
    }


    [Theory]
    [InlineData(-1)]
    public void GetById_WithoutMatch_ReturnsNull(int testId)
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var natureRepository = new NatureRepository(_testDbContext);

        // Act
        var result = natureRepository.GetById(testId)!;

        // Assert
        result.Should().BeNull();
    }
}
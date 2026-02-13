using API.Data;
using API.Models.Static;
using API.Repository;
using FluentAssertions;

namespace APITests.Repository;

public class GenderRepositoryTests
{
    private readonly ApplicationDbContext _testDbContext;

    public GenderRepositoryTests()
    {
        _testDbContext = Utility.CreateTestDbContext();
    }

    
    [Fact]
    public void GetAll_WithGendersInDb_ReturnsListOfGenders()
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var genderRepository = new GenderRepository(_testDbContext);

        // Act
        var result = genderRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(TestData.Genders.Count);
    }


    [Fact]
    public void GetAll_WithNoGendersInDb_ReturnsListOfGenders()
    {
        // Arrange 
        var expectedResult = new List<Gender>();
        var genderRepository = new GenderRepository(_testDbContext);

        // Act
        var result = genderRepository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [InlineData(1)]
    public void GetById_WithMatch_ReturnsGender(int testId)
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var genderRepository = new GenderRepository(_testDbContext);

        // Act
        var result = genderRepository.GetById(testId)!;

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
        var genderRepository = new GenderRepository(_testDbContext);

        // Act
        var result = genderRepository.GetById(testId)!;

        // Assert
        result.Should().BeNull();
    }
}
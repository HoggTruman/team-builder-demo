using API.Data;
using API.Models.Static;
using API.Repository;
using FluentAssertions;

namespace APITests.Repository;

public class PkmnTypeRepositoryTests
{
    private readonly ApplicationDbContext _testDbContext;

    public PkmnTypeRepositoryTests()
    {
        _testDbContext = Utility.CreateTestDbContext();
    }



    
    [Fact]
    public void GetAll_WithPkmnTypesInDb_ReturnsListOfPkmnTypes()
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var pkmnTypeRepository = new PkmnTypeRepository(_testDbContext);

        // Act
        var result = pkmnTypeRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(TestData.PkmnTypes.Count);
    }


    [Fact]
    public void GetAll_WithNoPkmnTypesInDb_ReturnsListOfPkmnTypes()
    {
        // Arrange 
        var expectedResult = new List<PkmnType>();
        var pkmnTypeRepository = new PkmnTypeRepository(_testDbContext);

        // Act
        var result = pkmnTypeRepository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [InlineData(1)]
    public void GetById_WithMatch_ReturnsPkmnType(int testId)
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var pkmnTypeRepository = new PkmnTypeRepository(_testDbContext);

        // Act
        var result = pkmnTypeRepository.GetById(testId)!;

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
        var pkmnTypeRepository = new PkmnTypeRepository(_testDbContext);

        // Act
        var result = pkmnTypeRepository.GetById(testId)!;

        // Assert
        result.Should().BeNull();
    }
}
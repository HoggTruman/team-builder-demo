using API.Data;
using API.Models.Static;
using API.Repository;
using FluentAssertions;

namespace APITests.Repository;

public class AbilityRepositoryTests
{
    private readonly ApplicationDbContext _testDbContext;

    public AbilityRepositoryTests()
    {
        _testDbContext = Utility.CreateTestDbContext();
    }

    
    [Fact]
    public void GetAll_WithAbilitiesInDb_ReturnsListOfAbilities()
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var abilityRepository = new AbilityRepository(_testDbContext);

        // Act
        var result = abilityRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(TestData.Abilities.Count);
    }


    [Fact]
    public void GetAll_WithNoAbilitiesInDb_ReturnsListOfAbilities()
    {
        // Arrange 
        var expectedResult = new List<Ability>();
        var abilityRepository = new AbilityRepository(_testDbContext);

        // Act
        var result = abilityRepository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [InlineData(1)]
    public void GetById_WithMatch_ReturnsAbility(int testId)
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var abilityRepository = new AbilityRepository(_testDbContext);

        // Act
        var result = abilityRepository.GetById(testId)!;

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
        var abilityRepository = new AbilityRepository(_testDbContext);

        // Act
        var result = abilityRepository.GetById(testId)!;

        // Assert
        result.Should().BeNull();
    }
}
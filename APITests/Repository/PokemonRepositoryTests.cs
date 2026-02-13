using API.Data;
using API.Models.Static;
using API.Repository;
using FluentAssertions;

namespace APITests.Repository;

public class PokemonRepositoryTests
{
    private readonly ApplicationDbContext _testDbContext;

    public PokemonRepositoryTests()
    {
        _testDbContext = Utility.CreateTestDbContext();
    }



    
    [Fact]
    public void GetAll_WithPokemonInDb_ReturnsListOfPokemon()
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var pokemonRepository = new PokemonRepository(_testDbContext);

        // Act
        var result = pokemonRepository.GetAll();

        // Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(TestData.Pokemon.Count);
    }


    [Fact]
    public void GetAll_WithNoPokemonInDb_ReturnsListOfPokemon()
    {
        // Arrange 
        var expectedResult = new List<Pokemon>();
        var pokemonRepository = new PokemonRepository(_testDbContext);

        // Act
        var result = pokemonRepository.GetAll();

        // Assert
        result.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [InlineData(1)]
    public void GetById_WithMatch_ReturnsPokemon(int testId)
    {
        // Arrange 
        Utility.AddTestData(_testDbContext);
        var pokemonRepository = new PokemonRepository(_testDbContext);

        // Act
        var result = pokemonRepository.GetById(testId)!;

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
        var pokemonRepository = new PokemonRepository(_testDbContext);

        // Act
        var result = pokemonRepository.GetById(testId);

        // Assert
        result.Should().BeNull();
    }
}
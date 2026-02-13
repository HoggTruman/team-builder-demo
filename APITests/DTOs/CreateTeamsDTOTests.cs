using System.ComponentModel.DataAnnotations;
using API.DTOs.Team;
using API.DTOs.UserPokemon;
using FluentAssertions;
using Moq;

namespace APITests.DTOs;

public class CreateTeamsDTOTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(100)]
    public void NonNegativeId_ReturnsFalse(int id)
    {
        CreateTeamsDTO testDTO = new()
        {
            Id = id,
            TeamName = "testTeamName",
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeFalse();

    }


    [Fact]
    public void ZeroPokemonNegativeId_ReturnsTrue()
    {
        CreateTeamsDTO testDTO = new()
        {
            Id = -1,
            TeamName = "testTeamName",
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeTrue();
    }


    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public void OneToSixPokemonWithUniqueTeamSlots_ReturnsTrue(int numPokemon)
    {
        // Arrange
        List<CreateUserPokemonDTO> pokemonList = new();

        for (int teamSlot = 1; teamSlot <= numPokemon; teamSlot++ )
        {
            var mockPokemonDTO = Mock.Of<CreateUserPokemonDTO>(x => x.TeamSlot == teamSlot);
            pokemonList.Add(mockPokemonDTO);
        }

        CreateTeamsDTO testDTO = new()
        {
            Id = -1,
            TeamName = "testTeamName",
            UserPokemon = pokemonList
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeTrue();
    }


    [Theory]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    public void NonUniqueTeamSlots_ReturnsFalse(int numPokemon)
    {
        // Arrange
        List<CreateUserPokemonDTO> pokemonList = new();

        for (int teamSlot = 1; teamSlot <= numPokemon; teamSlot++ )
        {
            var mockPokemonDTO = Mock.Of<CreateUserPokemonDTO>(x => x.TeamSlot == 1);
            pokemonList.Add(mockPokemonDTO);
        }

        CreateTeamsDTO testDTO = new()
        {
            Id = -1,
            TeamName = "testTeamName",
            UserPokemon = pokemonList
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeFalse();
    }


    [Theory]
    [InlineData(7)]
    [InlineData(8)]
    public void MoreThanSixPokemonWithUniqueTeamSlots_ReturnsFalse(int numPokemon)
    {
        // Arrange
        List<CreateUserPokemonDTO> pokemonList = new();

        for (int teamSlot = 1; teamSlot <= numPokemon; teamSlot++ )
        {
            var mockPokemonDTO = Mock.Of<CreateUserPokemonDTO>(x => x.TeamSlot == teamSlot);
            pokemonList.Add(mockPokemonDTO);
        }

        CreateTeamsDTO testDTO = new()
        {
            Id = -1,
            TeamName = "testTeamName",
            UserPokemon = pokemonList
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        


        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeFalse();
    } 
}
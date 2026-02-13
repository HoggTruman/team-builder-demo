using System.ComponentModel.DataAnnotations;
using API.DTOs.UserPokemon;
using FluentAssertions;

namespace APITests.DTOs;

public class CreateUserPokemonDTOTests
{
    [Fact]
    public void DuplicateNullMoveIds_ReturnsTrue()
    {
        // Arrange 
        CreateUserPokemonDTO testDTO = new()
        {
            Move1Id = null,
            Move2Id = null,
            Move3Id = null,
            Move4Id = null
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeTrue();
    }


    [Theory]
    [InlineData(1, 1, null, null)]
    [InlineData(1, 1, 1, null)]
    [InlineData(1, 1, 1, 1)]
    [InlineData(1, 1, 2, 2)]
    public void DuplicateIntegerMoveIds_ReturnsFalse(int? move1Id, int? move2Id, int? move3Id, int? move4Id)
    {
        // Arrange 
        CreateUserPokemonDTO testDTO = new()
        {
            Move1Id = move1Id,
            Move2Id = move2Id,
            Move3Id = move3Id,
            Move4Id = move4Id
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeFalse();
    }


    [Fact]
    public void EVTotalGreaterThan510_ReturnsFalse()
    {
        // Arrange 
        CreateUserPokemonDTO testDTO = new()
        {
            HPEV = 100,
            AttackEV = 100,
            DefenseEV = 100,
            SpecialAttackEV = 100,
            SpecialDefenseEV = 100,
            SpeedEV = 100
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeFalse();
    }


    [Fact]
    public void EVTotalEqualTo510_ReturnsTrue()
    {
        // Arrange 
        CreateUserPokemonDTO testDTO = new()
        {
            HPEV = 252,
            AttackEV = 252,
            DefenseEV = 6,
            SpecialAttackEV = 0,
            SpecialDefenseEV = 0,
            SpeedEV = 0
        };

        var context = new ValidationContext(testDTO, null, null);
        var results = new List<ValidationResult>();        

        // Act
        var isModelStateValid = Validator.TryValidateObject(testDTO, context, results, true);

        // Assert
        isModelStateValid.Should().BeTrue();
    }
}
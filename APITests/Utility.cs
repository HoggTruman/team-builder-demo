using API.Data;
using Microsoft.EntityFrameworkCore;

namespace APITests;

public static class Utility
{
    public static ApplicationDbContext CreateTestDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;

        var testDbContext = new ApplicationDbContext(options);

        testDbContext.Database.EnsureDeleted();
        testDbContext.Database.EnsureCreated();

        return testDbContext;
    }


    public static void AddTestData(ApplicationDbContext context)
    {
        context.Ability.AddRange(TestData.Abilities);
        context.BaseStats.AddRange(TestData.BaseStats);
        context.DamageClass.AddRange(TestData.DamageClasses);
        context.Gender.AddRange(TestData.Genders);
        context.Item.AddRange(TestData.Items);
        context.Move.AddRange(TestData.Moves);
        context.MoveEffect.AddRange(TestData.MoveEffects);
        context.Nature.AddRange(TestData.Natures);
        context.PkmnType.AddRange(TestData.PkmnTypes);
        context.Pokemon.AddRange(TestData.Pokemon);

        context.PokemonAbility.AddRange(TestData.PokemonAbilitys);
        context.PokemonGender.AddRange(TestData.PokemonGenders);
        context.PokemonMove.AddRange(TestData.PokemonMoves);
        context.PokemonPkmnType.AddRange(TestData.PokemonPkmnTypes);

        context.SaveChanges();
    }
}
using System.Globalization;
using CsvHelper;
using API.Models.Static;
using API.Mappers.CSVClassMaps;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;
using EFCore.BulkExtensions;


namespace API.Data;

public class DbInitializer : IDbInitializer
{
    private const string SeedDir = "SeedData";
    private readonly ApplicationDbContext _dbContext;

    public DbInitializer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }   


    /// <summary>
    /// Populates the database with records from the seed data csv files.
    /// </summary>
    public void SeedAll()
    {
        // Ordered to prevent foreign key constraint violations
        SeedTable<Gender, GenderCSVMap>(@"gender.csv", _dbContext.Gender);
        SeedTable<Nature, NatureCSVMap>(@"nature.csv", _dbContext.Nature);
        SeedTable<DamageClass, DamageClassCSVMap>(@"damage_class.csv", _dbContext.DamageClass);
        SeedTable<PkmnType, PkmnTypeCSVMap>(@"pkmn_type.csv", _dbContext.PkmnType);
        SeedTable<Ability, AbilityCSVMap>(@"ability.csv", _dbContext.Ability);
        SeedTable<MoveEffect, MoveEffectCSVMap>(@"move_effect.csv", _dbContext.MoveEffect);
        SeedTable<Item, ItemCSVMap>(@"item.csv", _dbContext.Item);
        SeedTable<Move, MoveCSVMap>(@"move.csv", _dbContext.Move);
        SeedTable<Pokemon, PokemonCSVMap>(@"pokemon.csv", _dbContext.Pokemon);
        SeedTable<BaseStats, BaseStatsCSVMap>(@"base_stats.csv", _dbContext.BaseStats);

        SeedTable<PokemonPkmnType, PokemonPkmnTypeCSVMap>(@"pokemon_pkmn_type.csv", _dbContext.PokemonPkmnType);
        SeedTable<PokemonMove, PokemonMoveCSVMap>(@"pokemon_move.csv", _dbContext.PokemonMove);
        SeedTable<PokemonAbility, PokemonAbilityCSVMap>(@"pokemon_ability.csv", _dbContext.PokemonAbility);
        SeedTable<PokemonGender, PokemonGenderCSVMap>(@"pokemon_gender.csv", _dbContext.PokemonGender);

        _dbContext.BulkSaveChanges();
    }


    /// <summary>
    /// Reads the data from the file with provided name and adds its data to the provided DbSet.
    /// </summary>
    /// <typeparam name="T">The type of object within the DbSet.</typeparam>
    /// <typeparam name="M">The CSV class map type associated with T.</typeparam>
    /// <param name="filename">The filename containing the records.</param>
    /// <param name="dbSet">The DbSet to add entries to.</param>
    private static void SeedTable<T, M>(string filename, DbSet<T> dbSet) 
        where M : ClassMap<T>
        where T : class
    {
        if (dbSet.Any())
        {
            return;
        }

        using var streamReader = new StreamReader(Path.Join(SeedDir, filename));
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        csvReader.Context.RegisterClassMap<M>();
        var records = csvReader.GetRecords<T>();
        dbSet.AddRange(records);
    }
}
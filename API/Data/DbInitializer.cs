using System.Globalization;
using CsvHelper;
using API.Models.Static;
using API.Mappers.CSVClassMaps;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using CsvHelper.Configuration;


namespace API.Data;

public class DbInitializer : IDbInitializer
{
    private const string SeedDir = @"Data\SeedData";
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
        ClearTables();

        AddRecords<Pokemon, PokemonCSVMap>(@"pokemon.csv", _dbContext.Pokemon);
        AddRecords<PkmnType, PkmnTypeCSVMap>(@"pkmn_type.csv", _dbContext.PkmnType);
        AddRecords<BaseStats, BaseStatsCSVMap>(@"base_stats.csv", _dbContext.BaseStats);
        AddRecords<Ability, AbilityCSVMap>(@"ability.csv", _dbContext.Ability);
        AddRecords<Gender, GenderCSVMap>(@"gender.csv", _dbContext.Gender);
        AddRecords<Move, MoveCSVMap>(@"move.csv", _dbContext.Move);
        AddRecords<MoveEffect, MoveEffectCSVMap>(@"move_effect.csv", _dbContext.MoveEffect); // MOVE EFFECTS ARE MISSING FOR SOME NEWER MOVES IN SEED DATA
        AddRecords<DamageClass, DamageClassCSVMap>(@"damage_class.csv", _dbContext.DamageClass);
        AddRecords<Item, ItemCSVMap>(@"item.csv", _dbContext.Item);
        AddRecords<Nature, NatureCSVMap>(@"nature.csv", _dbContext.Nature);

        AddRecords<PokemonPkmnType, PokemonPkmnTypeCSVMap>(@"pokemon_pkmn_type.csv", _dbContext.PokemonPkmnType);
        AddRecords<PokemonMove, PokemonMoveCSVMap>(@"pokemon_move.csv", _dbContext.PokemonMove);
        AddRecords<PokemonAbility, PokemonAbilityCSVMap>(@"pokemon_ability.csv", _dbContext.PokemonAbility);
        AddRecords<PokemonGender, PokemonGenderCSVMap>(@"pokemon_gender.csv", _dbContext.PokemonGender);

        _dbContext.SaveChanges();
    }


    private void ClearTables()
    {
        _dbContext.Pokemon.ExecuteDelete();
        _dbContext.PkmnType.ExecuteDelete();
        _dbContext.BaseStats.ExecuteDelete();
        _dbContext.Ability.ExecuteDelete();
        _dbContext.Move.ExecuteDelete();
        _dbContext.DamageClass.ExecuteDelete();
        _dbContext.MoveEffect.ExecuteDelete();
        _dbContext.Gender.ExecuteDelete();
        _dbContext.Item.ExecuteDelete();
        _dbContext.Nature.ExecuteDelete();

        _dbContext.PokemonPkmnType.ExecuteDelete();
        _dbContext.PokemonMove.ExecuteDelete();
        _dbContext.PokemonAbility.ExecuteDelete();
        _dbContext.PokemonGender.ExecuteDelete();
    }


    /// <summary>
    /// Reads the data from the file with provided name and adds its data to the provided DbSet.
    /// </summary>
    /// <typeparam name="T">The type of object within the DbSet.</typeparam>
    /// <typeparam name="M">The CSV class map type associated with T.</typeparam>
    /// <param name="filename">The filename containing the records.</param>
    /// <param name="dbSet">The DbSet to add entries to.</param>
    private static void AddRecords<T, M>(string filename, DbSet<T> dbSet) 
        where M : ClassMap<T>
        where T : class
    {
        using (var streamReader = new StreamReader(Path.Join(SeedDir, filename)))
        using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        {
            csvReader.Context.RegisterClassMap<M>();
            var records = csvReader.GetRecords<T>().ToArray();
            dbSet.AddRange(records);
        }
    }
}
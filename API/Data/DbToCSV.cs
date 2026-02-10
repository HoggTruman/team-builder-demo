using System.Globalization;
using API.Mappers.CSVClassMaps;
using API.Models.Static;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class DbToCSV
{
    private const string WriteDir = @"Data\WriteData";
    private readonly ApplicationDbContext _dbContext;
    

    public DbToCSV(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }    


    /// <summary>
    /// Writes the database tables to csv files.
    /// </summary>
    public void WriteAllToCSV()
    {
        WriteTableToCSV<Pokemon, PokemonCSVMap>(@"pokemon.csv", _dbContext.Pokemon);
        WriteTableToCSV<PkmnType, PkmnTypeCSVMap>(@"pkmn_type.csv", _dbContext.PkmnType);
        WriteTableToCSV<BaseStats, BaseStatsCSVMap>(@"base_stats.csv", _dbContext.BaseStats);
        WriteTableToCSV<Ability, AbilityCSVMap>(@"ability.csv", _dbContext.Ability);
        WriteTableToCSV<Move, MoveCSVMap>(@"move.csv", _dbContext.Move);
        WriteTableToCSV<DamageClass, DamageClassCSVMap>(@"damage_class.csv", _dbContext.DamageClass);
        WriteTableToCSV<MoveEffect, MoveEffectCSVMap>(@"move_effect.csv", _dbContext.MoveEffect);
        WriteTableToCSV<Gender, GenderCSVMap>(@"gender.csv", _dbContext.Gender);
        WriteTableToCSV<Item, ItemCSVMap>(@"item.csv", _dbContext.Item);
        WriteTableToCSV<Nature, NatureCSVMap>(@"nature.csv", _dbContext.Nature);

        // Join Tables
        WriteTableToCSV<PokemonPkmnType, PokemonPkmnTypeCSVMap>(@"pokemon_pkmn_type.csv", _dbContext.PokemonPkmnType);
        WriteTableToCSV<PokemonMove, PokemonMoveCSVMap>(@"pokemon_move.csv", _dbContext.PokemonMove);
        WriteTableToCSV<PokemonAbility, PokemonAbilityCSVMap>(@"pokemon_ability.csv", _dbContext.PokemonAbility);
        WriteTableToCSV<PokemonGender, PokemonGenderCSVMap>(@"pokemon_move.csv", _dbContext.PokemonGender);
    }


    /// <summary>
    /// Writes the data contained within the provided DbSet to a file with the name provided.
    /// </summary>
    /// <typeparam name="T">The type of object within the DbSet</typeparam>
    /// <typeparam name="M">The CSV class map type associated with T</typeparam>
    /// <param name="filename"></param>
    private static void WriteTableToCSV<T, M>(string filename, DbSet<T> dbset) 
        where M : ClassMap<T>
        where T : class
    {
        string path = Path.Join(WriteDir, filename);

        if (File.Exists(path))
        {
            Console.WriteLine($"File already exists at {path}");
            return;
        }

        using (var streamWriter = new StreamWriter(path))
        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
        {
            var records = dbset.ToList();
            csvWriter.Context.RegisterClassMap<M>();
            csvWriter.WriteRecords(records);
            Console.WriteLine($"File written to {path}");
        }
    }
}
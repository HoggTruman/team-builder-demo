using System.Globalization;
using CsvHelper;
using API.Models.Static;
using API.Mappers.CSVClassMaps;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace API.Data;

public class RawDbInitializer : IDbInitializer
{
    private const string SeedDir = @"Data\RawData\";
    private readonly ApplicationDbContext _dbContext;


    public RawDbInitializer(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    


    public void SeedAll()
    {
        ClearTables();            

        AddPokemon(_dbContext);
        AddPkmnType(_dbContext);
        AddBaseStats(_dbContext);
        AddAbility(_dbContext);
        AddGender(_dbContext);
        AddMove(_dbContext);
        AddMoveEffect(_dbContext); // MOVE EFFECTS ARE MISSING FOR SOME NEWER MOVES IN SEED DATA
        AddDamageClass(_dbContext);
        AddItem(_dbContext);
        AddNature(_dbContext);

        //Join Tables
        AddPokemonPkmnType(_dbContext);
        AddPokemonMove(_dbContext);
        AddPokemonAbility(_dbContext);
        AddPokemonGender(_dbContext);

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




    private void AddPokemon(ApplicationDbContext context)
    {
        if (!context.Pokemon.Any())
            {
                using (var reader = new StreamReader(SeedDir + @"pokemon.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<PokemonCSVMap>();
                    var records = csv.GetRecords<Pokemon>().ToArray();
                    context.Pokemon.AddRange(records);  
                }
            }
    }


    private void AddPkmnType(ApplicationDbContext context)
    {
        if (!context.PkmnType.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"types.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<PkmnTypeCSVMap>();
                var records = csv.GetRecords<PkmnType>().ToArray();
                context.PkmnType.AddRange(records);
            }
        }
    }


    private void AddBaseStats(ApplicationDbContext context)
    {
        if (!context.BaseStats.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"pokemon_stats.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = new List<BaseStats>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = records.Find(x => x.PokemonId == csv.GetField<int>("pokemon_id"));
                    if (record == null)
                    {
                        record = new BaseStats()
                        {
                            PokemonId = csv.GetField<int>("pokemon_id")
                        };

                        records.Add(record);
                    }

                    switch (csv.GetField<int>("stat_id"))
                    {
                        case 1:
                            record.HP = csv.GetField<int>("base_stat");
                            break;
                        case 2:
                            record.Attack = csv.GetField<int>("base_stat");
                            break;
                        case 3:
                            record.Defense = csv.GetField<int>("base_stat");
                            break;
                        case 4:
                            record.SpecialAttack = csv.GetField<int>("base_stat");
                            break;
                        case 5:
                            record.SpecialDefense = csv.GetField<int>("base_stat");
                            break;
                        case 6:
                            record.Speed = csv.GetField<int>("base_stat");
                            break;                        
                    }
                }
                context.BaseStats.AddRange(records);
            }  
        }
    }


    private void AddAbility(ApplicationDbContext context)
    {
        if (!context.Ability.Any())
        {
            var abilityRecords = new List<Ability>();

            var config = new CsvHelper.Configuration.CsvConfiguration(CultureInfo.InvariantCulture);    
            config.MissingFieldFound = null;

            // Add Main series Ability name and Id
            using (var reader = new StreamReader(SeedDir + @"abilities.csv"))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<AbilityCSVMap>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    if (csv.GetField<int>("is_main_series") == 1)
                    {
                        abilityRecords.Add(csv.GetRecord<Ability>());
                    }
                }
            }

            // Add FlavorText to each Ability
            using (var reader = new StreamReader(SeedDir + @"ability_flavor_text.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                const int ENGLISH_LANGUAGE_ID = 9;
                
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    if (csv.GetField<int>("language_id") == ENGLISH_LANGUAGE_ID)
                    {
                        var record = abilityRecords.Find(x => x.Id == csv.GetField<int>("ability_id"));

                        if (record != null) 
                        {
                            record.FlavorText = csv.GetField<string>("flavor_text");
                        }
                    }
                }
            }

            context.Ability.AddRange(abilityRecords);            
        }
    }


    private void AddMove(ApplicationDbContext context)
    {
        if (!context.Move.Any())
            {
                using (var reader = new StreamReader(SeedDir + @"moves.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<MoveCSVMap>();
                    var records = csv.GetRecords<Move>().ToArray();
                    context.Move.AddRange(records);
                }
            }
    }


    private void AddDamageClass(ApplicationDbContext context)
    {
        if (!context.DamageClass.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"move_damage_classes.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<DamageClassCSVMap>();
                var records = csv.GetRecords<DamageClass>().ToArray();
                context.DamageClass.AddRange(records);
            }
        }
    }


    private void AddMoveEffect(ApplicationDbContext context)
    {
        if (!context.MoveEffect.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"move_effect_prose.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<MoveEffectCSVMap>();
                var records = csv.GetRecords<MoveEffect>().ToArray();
                context.MoveEffect.AddRange(records);
            }     
        }
    }


    private void AddGender(ApplicationDbContext context)
    {
        if (!context.Gender.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"genders.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<GenderCSVMap>();
                var records = csv.GetRecords<Gender>().ToArray();
                context.Gender.AddRange(records); 
            }                     
        }

    }


    private void AddItem(ApplicationDbContext context)
    {
        if (!context.Item.Any())
        {
            int[] HOLDITEM_FLAG_IDS = [5, 6, 7]; // potentially 5 not needed

            var records = new List<Item>();

            // Create record for each holdable item with the "item_id"
            using (var reader = new StreamReader(SeedDir + @"item_flag_map.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    if (HOLDITEM_FLAG_IDS.Contains(csv.GetField<int>("item_flag_id")))
                    {
                        var record = records.FirstOrDefault(x => x.Id == csv.GetField<int>("item_id"));

                        if (record == null)
                        {
                            record = new Item()
                            {
                                Id = csv.GetField<int>("item_id")
                            };
                            
                            records.Add(record);
                        }
                    }
                }

            }

            // Add Identifier to each 
            using (var reader = new StreamReader(SeedDir + @"items.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = records.FirstOrDefault(x => x.Id == csv.GetField<int>("id"));
                    if (record != null)
                    {
                        record.Identifier = csv.GetField("identifier");
                    }
                }
            }

            // Add Effect to each 
            using (var reader = new StreamReader(SeedDir + @"item_prose.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = records.FirstOrDefault(x => x.Id == csv.GetField<int>("item_id"));
                    if (record != null)
                    {
                        record.Effect = csv.GetField("short_effect");
                    }
                }
            }
            context.Item.AddRange(records);
        }
    }


    private void AddNature(ApplicationDbContext context)
    {
        if (!context.Nature.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"natures.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                const double INCREASED_MULTIPLIER = 1.1;
                const double DECREASED_MULTIPLIER = 0.9;

                var records = new List<Nature>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    var record = new Nature()
                    {
                        Id = csv.GetField<int>("id"),
                        Identifier = csv.GetField("identifier"),
                        AttackMultiplier = 1,
                        DefenseMultiplier = 1,
                        SpecialAttackMultiplier = 1,
                        SpecialDefenseMultiplier = 1,
                        SpeedMultiplier = 1
                    };

                    if (csv.GetField("increased_stat_id") != csv.GetField("decreased_stat_id"))
                    {
                        switch (csv.GetField<int>("increased_stat_id"))
                        {
                            case 2:
                                record.AttackMultiplier = INCREASED_MULTIPLIER;
                                break;
                            case 3:
                                record.DefenseMultiplier = INCREASED_MULTIPLIER;
                                break;
                            case 4:
                                record.SpecialAttackMultiplier = INCREASED_MULTIPLIER;
                                break;
                            case 5:
                                record.SpecialDefenseMultiplier = INCREASED_MULTIPLIER;
                                break;
                            case 6:
                                record.SpeedMultiplier = INCREASED_MULTIPLIER;
                                break;                                                                        
                        }

                        switch (csv.GetField<int>("decreased_stat_id"))
                        {
                            case 2:
                                record.AttackMultiplier = DECREASED_MULTIPLIER;
                                break;
                            case 3:
                                record.DefenseMultiplier = DECREASED_MULTIPLIER;
                                break;
                            case 4:
                                record.SpecialAttackMultiplier = DECREASED_MULTIPLIER;
                                break;
                            case 5:
                                record.SpecialDefenseMultiplier = DECREASED_MULTIPLIER;
                                break;
                            case 6:
                                record.SpeedMultiplier = DECREASED_MULTIPLIER;
                                break;                                                                        
                        }
                    }

                    records.Add(record);
                }

                context.Nature.AddRange(records);
            }   
        }
     
    }







    private void AddPokemonPkmnType(ApplicationDbContext context)
    {
        if (!context.PokemonPkmnType.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"pokemon_types.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<PokemonPkmnTypeCSVMap>();
                var records = csv.GetRecords<PokemonPkmnType>().ToArray();
                context.PokemonPkmnType.AddRange(records);
            }
        }
    }


    private void AddPokemonMove(ApplicationDbContext context)
    {
        if (!context.PokemonMove.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"pokemon_moves.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<PokemonMoveCSVMap>();
                var records = csv.GetRecords<PokemonMove>().ToArray();
                context.PokemonMove.AddRange(records);
            }     
        }
    }


    private void AddPokemonAbility(ApplicationDbContext context)
    {
        if (!context.PokemonAbility.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"pokemon_abilities.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<PokemonAbilityCSVMap>();

                var records = new List<PokemonAbility>();

                csv.Read();
                csv.ReadHeader();

                while(csv.Read())
                {
                    var record = csv.GetRecord<PokemonAbility>();

                    if (records.Find(x => x.PokemonId == record.PokemonId && x.AbilityId == record.AbilityId) == null)
                    {
                        records.Add(record);
                    }
                }

                context.PokemonAbility.AddRange(records);
            }            
        }
    }


    private void AddPokemonGender(ApplicationDbContext context)
    {
        if (!context.PokemonGender.Any())
        {
            using (var reader = new StreamReader(SeedDir + @"pokemon_species.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                const int FEMALE_ID = 1;
                const int MALE_ID = 2;
                const int GENDERLESS_ID = 3;

                var records = new List<PokemonGender>();

                csv.Read();
                csv.ReadHeader();

                while (csv.Read())
                {
                    switch (csv.GetField<int>("gender_rate"))
                    {
                        case 0:
                            records.Add(
                                new PokemonGender
                                {
                                    PokemonId = csv.GetField<int>("id"),
                                    GenderId = MALE_ID
                                }
                            );
                            break;

                        case 8:
                            records.Add(
                                new PokemonGender
                                {
                                    PokemonId = csv.GetField<int>("id"),
                                    GenderId = FEMALE_ID
                                }
                            );
                            break;

                        case -1:
                            records.Add(
                                new PokemonGender
                                {
                                    PokemonId = csv.GetField<int>("id"),
                                    GenderId = GENDERLESS_ID
                                }
                            );                    
                            break;
                            
                        default:
                            records.Add(
                                new PokemonGender
                                {
                                    PokemonId = csv.GetField<int>("id"),
                                    GenderId = MALE_ID
                                }
                            );
                            records.Add(
                                new PokemonGender
                                {
                                    PokemonId = csv.GetField<int>("id"),
                                    GenderId = FEMALE_ID
                                }
                            );
                            break;                        
                    }
                }

                context.PokemonGender.AddRange(records);
            }
        }
    }
}
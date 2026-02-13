using API.Models.Static;

namespace APITests;

public static class TestData
{
    public static List<Ability> Abilities
    {
        get
        {
            return new List<Ability>
            {
                new()
                {
                    Id = 1,
                    Identifier = "TestAbility1",
                    FlavorText = "FlavorText1"
                },
                new()
                {
                    Id = 2,
                    Identifier = "TestAbility2",
                    FlavorText = "FlavorText2"
                },
                new()
                {
                    Id = 3,
                    Identifier = "TestAbility3",
                    FlavorText = "FlavorText3"
                }
            };
        }
    }    




    public static List<BaseStats> BaseStats
    {
        get
        {
            return new List<BaseStats>
            {
                new()
                {
                    PokemonId = 1,
                    HP = 1,
                    Attack = 1,
                    Defense = 1,
                    SpecialAttack = 1,
                    SpecialDefense = 1,
                    Speed = 1
                },
                new()
                {
                    PokemonId = 2,
                    HP = 2,
                    Attack = 2,
                    Defense = 2,
                    SpecialAttack = 2,
                    SpecialDefense = 2,
                    Speed = 2
                },
                new()
                {
                    PokemonId = 3,
                    HP = 3,
                    Attack = 3,
                    Defense = 3,
                    SpecialAttack = 3,
                    SpecialDefense = 3,
                    Speed = 3
                }
            };
        }
    }    




    public static List<DamageClass> DamageClasses
    {
        get
        {
            return new List<DamageClass>
            {
                new()
                {
                    Id = 1,
                    Identifier = "TestDamageClass1"
                },
                new()
                {
                    Id = 2,
                    Identifier = "TestDamageClass2"
                },
                new()
                {
                    Id = 3,
                    Identifier = "TestDamageClass3"
                }
            };
        }
    }




    public static List<Gender> Genders
    {
        get
        {
            return new List<Gender>
            {
                new()
                {
                    Id = 1,
                    Identifier = "TestGender1"
                },
                new()
                {
                    Id = 2,
                    Identifier = "TestGender2"
                },
                new()
                {
                    Id = 3,
                    Identifier = "TestGender3"
                }
            };
        }
    }




    public static List<Item> Items
    {
        get
        {
            return new List<Item>
            {
                new() 
                {
                    Id = 1, 
                    Identifier = "TestItem1",
                    Effect = "TestItem1Effect"
                },
                new() 
                {
                    Id = 2, 
                    Identifier = "TestItem2",
                    Effect = "TestItem2Effect"
                },
                new() 
                {
                    Id = 3, 
                    Identifier = "TestItem3",
                    Effect = "TestItem3Effect"
                },
            };
        }
    }




    public static List<Move> Moves
    {
        get
        {
            return new List<Move>
            {
                new() 
                {
                    Id = 1, 
                    Identifier = "TestMove1",
                    Power = 100,
                    PP = 5,
                    Accuracy = 100,
                    Priority = 0,
                    PkmnTypeId = 1,
                    DamageClassId = 1,
                    MoveEffectId = 1,
                    MoveEffectChance = 100,     
                },
                new() 
                {
                    Id = 2, 
                    Identifier = "TestMove2",
                    Power = 200,
                    PP = 5,
                    Accuracy = 200,
                    Priority = 1,
                    PkmnTypeId = 2,
                    DamageClassId = 2,
                    MoveEffectId = 2,
                    MoveEffectChance = 100,     
                },
                new() 
                {
                    Id = 3, 
                    Identifier = "TestMove3",
                    Power = 300,
                    PP = 5,
                    Accuracy = 300,
                    Priority = 1,
                    PkmnTypeId = 3,
                    DamageClassId = 3,
                    MoveEffectId = 3,
                    MoveEffectChance = 100,     
                },
                new()
                {
                    Id = 4,
                    Identifier = "TestMove4",
                    PkmnTypeId = 1,
                    DamageClassId = 1,
                    MoveEffectId = 1
                },
                new()
                {
                    Id = 5,
                    Identifier = "TestMove5",
                    PkmnTypeId = 1,
                    DamageClassId = 1,
                    MoveEffectId = 1
                },
                new()
                {
                    Id = 6,
                    Identifier = "TestMove6",
                    PkmnTypeId = 1,
                    DamageClassId = 1,
                    MoveEffectId = 1
                },
                new()
                {
                    Id = 7,
                    Identifier = "TestMove7",
                    PkmnTypeId = 1,
                    DamageClassId = 1,
                    MoveEffectId = 1
                }
            };
        }
    }




    public static List<MoveEffect> MoveEffects
    {
        get
        {
            return new List<MoveEffect>
            {
                new()
                {
                    Id = 1,
                    Description = "MoveEffect1 Description"
                },
                new()
                {
                    Id = 2,
                    Description = "MoveEffect2 Description"
                },
                new()
                {
                    Id = 3,
                    Description = "MoveEffect3 Description"
                }
            };
        }
    }




    public static List<Nature> Natures
    {
        get
        {
            return new List<Nature>
            {
                new()
                {
                    Id = 1,
                    Identifier = "TestNature1",
                    AttackMultiplier = 1,
                    DefenseMultiplier = 1,
                    SpecialAttackMultiplier = 1,
                    SpecialDefenseMultiplier = 1,
                    SpeedMultiplier = 1,
                },
                new()
                {
                    Id = 2,
                    Identifier = "TestNature2",
                    AttackMultiplier = 0.9,
                    DefenseMultiplier = 1,
                    SpecialAttackMultiplier = 1.1,
                    SpecialDefenseMultiplier = 1,
                    SpeedMultiplier = 1,
                },
                new()
                {
                    Id = 3,
                    Identifier = "TestNature3",
                    AttackMultiplier = 1.1,
                    DefenseMultiplier = 1,
                    SpecialAttackMultiplier = 0.9,
                    SpecialDefenseMultiplier = 1,
                    SpeedMultiplier = 1,
                }
            };
        }  
    }




    public static List<PkmnType> PkmnTypes
    {
        get
        {
            return new List<PkmnType>
            {
                new()
                {
                    Id = 1,
                    Identifier = "TestPkmnType1"
                },
                new()
                {
                    Id = 2,
                    Identifier = "TestPkmnType2"
                },
                new()
                {
                    Id = 3,
                    Identifier = "TestPkmnType3"
                }
            };
        }
    }




    public static List<Pokemon> Pokemon
    {
        get
        {
            return new List<Pokemon>
            {
                new()
                {
                    Id = 1,
                    Identifier = "TestPokemon1",
                    SpeciesId = 1
                },
                new()
                {
                    Id = 2,
                    Identifier = "TestPokemon2",
                    SpeciesId = 2
                },
                new()
                {
                    Id = 3,
                    Identifier = "TestPokemon3",
                    SpeciesId = 3
                }
            };
        }
    }




    public static List<PokemonAbility> PokemonAbilitys
    {
        get
        {
            return new List<PokemonAbility>
            {
                new()
                {
                    PokemonId = 1,
                    AbilityId = 1,
                    Slot = 1
                },
                new()
                {
                    PokemonId = 2,
                    AbilityId = 2,
                    Slot = 1
                },
                new()
                {
                    PokemonId = 3,
                    AbilityId = 3,
                    Slot = 1
                }
            };
        }        
    }




    public static List<PokemonGender> PokemonGenders
    {
        get
        {
            return new List<PokemonGender>
            {
                new()
                {
                    PokemonId = 1,
                    GenderId = 1
                },
                new()
                {
                    PokemonId = 1,
                    GenderId = 2
                },
                new()
                {
                    PokemonId = 2,
                    GenderId = 2
                },
                new()
                {
                    PokemonId = 3,
                    GenderId = 3
                }
            };
        }
    }




    public static List<PokemonMove> PokemonMoves
    {
        get
        {
            return new List<PokemonMove>
            {
                new()
                {
                    PokemonId = 1,
                    MoveId = 1
                },
                new()
                {
                    PokemonId = 2,
                    MoveId = 2
                },
                new()
                {
                    PokemonId = 3,
                    MoveId = 3
                },
                new()
                {
                    PokemonId = 1,
                    MoveId = 4
                },
                new()
                {
                    PokemonId = 2,
                    MoveId = 4
                },
                new()
                {
                    PokemonId = 1,
                    MoveId = 5
                },
                new()
                {
                    PokemonId = 3,
                    MoveId = 5
                },
                new()
                {
                    PokemonId = 1,
                    MoveId = 6
                },
                new()
                {
                    PokemonId = 2,
                    MoveId = 7
                }
            };
        }        
    }




    public static List<PokemonPkmnType> PokemonPkmnTypes
    {
        get
        {
            return new List<PokemonPkmnType>
            {
                new()
                {
                    PokemonId = 1,
                    PkmnTypeId = 1,
                    Slot = 1
                },
                new()
                {
                    PokemonId = 1,
                    PkmnTypeId = 2,
                    Slot = 2
                },
                new()
                {
                    PokemonId = 2,
                    PkmnTypeId = 2,
                    Slot = 1
                },
                new()
                {
                    PokemonId = 3,
                    PkmnTypeId = 3,
                    Slot = 1
                }
            };
        }    
    }
}
function createNewPokemon({
    id = -Date.now(),
    teamSlot = 1,
    pokemonId = null,
    nickname = null,
    level = 100,
    genderId = 4,
    shiny = false,
    teraPkmnTypeId = 1,
    itemId = null,
    abilityId = null,

    move1Id = null,
    move2Id = null,
    move3Id = null,
    move4Id = null,

    natureId = 1,
    
    hpEV = 0,
    attackEV = 0,
    defenseEV = 0,
    specialAttackEV = 0,
    specialDefenseEV = 0,
    speedEV = 0,

    hpIV = 31,
    attackIV = 31,
    defenseIV = 31,
    specialAttackIV = 31,
    specialDefenseIV = 31,
    speedIV = 31
}={}) {
    return (
        {
            id: id,
            teamSlot: teamSlot,
            pokemonId: pokemonId,
            nickname: nickname,
            level: level,
            genderId: genderId,
            shiny: shiny,
            teraPkmnTypeId: teraPkmnTypeId,
            itemId: itemId,
            abilityId: abilityId,

            move1Id: move1Id,
            move2Id: move2Id,
            move3Id: move3Id,
            move4Id: move4Id,

            natureId: natureId,
            hpEV: hpEV,
            attackEV: attackEV,
            defenseEV: defenseEV,
            specialAttackEV: specialAttackEV,
            specialDefenseEV: specialDefenseEV,
            speedEV: speedEV,

            hpIV: hpIV,
            attackIV: attackIV,
            defenseIV: defenseIV,
            specialAttackIV: specialAttackIV,
            specialDefenseIV: specialDefenseIV,
            speedIV: speedIV
        }
    );
}


export default createNewPokemon;


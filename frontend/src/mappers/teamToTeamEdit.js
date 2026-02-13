import createNewTeamEdit from "../models/teamEditFactory"
import createNewPokemonEdit from "../models/pokemonEditFactory"

export function teamToTeamEdit(team, data) {
    const pokemonEditList = team.pokemon.map(pokemon => createNewPokemonEdit({
        id: pokemon.id,
        teamSlot: pokemon.teamSlot,
        pokemonName: data.pokemon.find(x => x.id == pokemon.pokemonId)?.identifier || "",
        nickname: pokemon.nickname || "",
        level: pokemon.level,
        genderId: pokemon.genderId || "4",
        shiny: pokemon.shiny,
        teraPkmnTypeId: String(pokemon.teraPkmnTypeId) || "1",
        itemName: data.items.find(x => x.id == pokemon.itemId)?.identifier || "",
        abilityName: data.abilities.find(x => x.id == pokemon.abilityId)?.identifier || "",

        move1Name: data.moves.find(x => x.id == pokemon.move1Id)?.identifier || "",
        move2Name: data.moves.find(x => x.id == pokemon.move2Id)?.identifier || "",
        move3Name: data.moves.find(x => x.id == pokemon.move3Id)?.identifier || "",
        move4Name: data.moves.find(x => x.id == pokemon.move4Id)?.identifier || "",

        natureId: String(pokemon.natureId) || "1",

        hpEV: String(pokemon.hpEV),
        attackEV: String(pokemon.attackEV),
        defenseEV: String(pokemon.defenseEV),
        specialAttackEV: String(pokemon.specialAttackEV),
        specialDefenseEV: String(pokemon.specialDefenseEV),
        speedEV: String(pokemon.speedEV),

        hpIV: pokemon.hpIV,
        attackIV: pokemon.attackIV,
        defenseIV: pokemon.defenseIV,
        specialAttackIV: pokemon.specialAttackIV,
        specialDefenseIV: pokemon.specialDefenseIV,
        speedIV: pokemon.speedIV          
    }))

    return createNewTeamEdit({
        id: team.id,
        teamName: team.teamName,
        pokemon: pokemonEditList
    })
}
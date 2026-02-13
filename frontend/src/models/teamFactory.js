import createNewPokemon from "./pokemonFactory";


function createNewTeam({
    id = null, 
    teamName = "new team",
    pokemon = [createNewPokemon()]
}={}) {
    return (
        {
            id: id,
            teamName: teamName,
            pokemon: pokemon
        }
    );
}


export default createNewTeam;


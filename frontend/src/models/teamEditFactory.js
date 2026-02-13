import createNewPokemonEdit from "./pokemonEditFactory";


function createNewTeamEdit({
    id = null, 
    teamName = "new team",
    pokemon = [createNewPokemonEdit()]
}={}) {
    return (
        {
            id: id,
            teamName: teamName,
            pokemon: pokemon
        }
    );
}


export default createNewTeamEdit;


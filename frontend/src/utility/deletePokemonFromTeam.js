/**
 * 
 * @param {object} team 
 * @param {int} deleteSlot 
 * @returns A shallow copy of the modified team object
 * 
 * Note: this modifies the team passed in
 */
export function deletePokemonFromTeam(team, deleteSlot) {
    if (team.pokemon.length == 1) {
        return team;
    }

    team.pokemon = team.pokemon.filter(pokemon => pokemon.teamSlot != deleteSlot);
    for (const pokemon of team.pokemon) {
        if (pokemon.teamSlot > deleteSlot) {
            pokemon.teamSlot -= 1;
        }
    }

    return team
}
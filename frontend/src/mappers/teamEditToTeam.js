import createNewPokemon from "../models/pokemonFactory";
import createNewTeam from "../models/teamFactory";
import { clean } from "../utility/cleanString";


export function teamEditToTeam(teamEdit, data) {
    const pokemonList = teamEdit.pokemon.map(pokemonEdit => {
        const pokemonData = data.pokemon.find(x => x.identifier == clean(pokemonEdit.pokemonName));
        const validAbilityIds = pokemonData?.abilities || [];
        const validMoveIds = pokemonData?.moves || [];
        
        return createNewPokemon({
            id: pokemonEdit.id,
            teamSlot: pokemonEdit.teamSlot,
            pokemonId: pokemonData?.id,
            nickname: pokemonEdit.nickname === ""? null: pokemonEdit.nickname,
            level: convertLevel(pokemonEdit.level),
            genderId: Number(pokemonEdit.genderId),

            shiny: pokemonEdit.shiny,
            teraPkmnTypeId: Number(pokemonEdit.teraPkmnTypeId),
            itemId: getItemId(pokemonEdit.itemName, data.items),
            abilityId: getAbilityId(pokemonEdit.abilityName, validAbilityIds, data.abilities),

            move1Id: getMoveId(pokemonEdit.move1Name, validMoveIds, data.moves),
            move2Id: getMoveId(pokemonEdit.move2Name, validMoveIds, data.moves),
            move3Id: getMoveId(pokemonEdit.move3Name, validMoveIds, data.moves),
            move4Id: getMoveId(pokemonEdit.move4Name, validMoveIds, data.moves),

            natureId: Number(pokemonEdit.natureId),

            hpEV: Number(pokemonEdit.hpEV),
            attackEV: Number(pokemonEdit.attackEV),
            defenseEV: Number(pokemonEdit.defenseEV),
            specialAttackEV: Number(pokemonEdit.specialAttackEV),
            specialDefenseEV: Number(pokemonEdit.specialDefenseEV),
            speedEV: Number(pokemonEdit.speedEV),

            hpIV: convertIV(pokemonEdit.hpIV),
            attackIV: convertIV(pokemonEdit.attackIV),
            defenseIV: convertIV(pokemonEdit.defenseIV),
            specialAttackIV: convertIV(pokemonEdit.specialAttackIV),
            specialDefenseIV: convertIV(pokemonEdit.specialDefenseIV),
            speedIV: convertIV(pokemonEdit.speedIV)    
        });
    })

    return createNewTeam({
        id: teamEdit.id,
        teamName: teamEdit.teamName,
        pokemon: pokemonList
    })
}



// Helpers (Exported only for testing)

/** Returns level as an integer from 1 to 100 if it can be converted, otherwise returns 100.
 * 
 * @param {*} level
 * @returns {number}
 */
export function convertLevel(level) {
    const numLevel = Number(level);

    if (
        Number.isNaN(numLevel) === false &&
        Number.isInteger(numLevel) &&
        numLevel <= 100 &&
        numLevel >= 1
    ) {
        return numLevel;
    }

    return 100;
}




/** Returns the id of the item with identifier itemName
 * 
 * @param {string} itemName 
 * @param {*} items array of item objects
 * @returns {number}
 */
export function getItemId(itemName, items) {
    return items.find(x => x.identifier == clean(itemName))?.id || null;
}




/** Returns the id of the ability with identifier abilityName if contained in pokemonData
 * 
 * @param {string} abilityName 
 * @param {*} validIds array of integers
 * @param {*} abilities array of ability objects
 * @returns {number}
 */
export function getAbilityId(abilityName, validIds, abilities) {
    const abilityId = abilities.find(x => x.identifier == clean(abilityName))?.id;
    if (validIds.includes(abilityId)) {
        return abilityId;
    }

    return null;
}




/** Returns the id of the move with identifier moveName if contained in pokemonData
 * 
 * @param {string} moveName 
 * @param {*} validIds array of integers
 * @param {*} moves array of move objects
 * @returns {number}
 */
export function getMoveId(moveName, validIds, moves) {
    const moveId = moves.find(x => x.identifier == clean(moveName))?.id;
    if (validIds.includes(moveId)) {
        return moveId;
    }

    return null;
}




/** Returns iv as an integer from 0 to 31 if it can be converted, otherwise returns 31.
 * 
 * @param {*} iv string or number
 * @returns {number}
 */
export function convertIV(iv) {
    if (iv === "" || iv === null || iv === undefined) {
        return 31
    }
    
    const numIV = Number(iv);

    if (
        Number.isNaN(numIV) === false &&
        Number.isInteger(numIV) &&
        numIV <= 31 &&
        numIV >= 0
    ) {
        return numIV;
    }

    return 31;
}
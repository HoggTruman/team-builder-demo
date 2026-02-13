import { calcRemainingEVs } from "./calcRemainingEVs";

export function checkValidTeam(team) {
    const issues = [];

    if (team.teamName === "") {
        issues.push("team name can not be blank");
    }

    for (const pokemon of team.pokemon) {
        // Check for duplicate moves
        const moveIds = [
            pokemon.move1Id,
            pokemon.move2Id,
            pokemon.move3Id,
            pokemon.move4Id,
        ].filter(moveId => moveId !== null);

        if (hasDuplicates(moveIds)) {
            issues.push(`pokemon in slot #${pokemon.teamSlot} has duplicate moves`)
        }

        // Check for too many EVs
        const remainingEVs = calcRemainingEVs(
            pokemon.hpEV,
            pokemon.attackEV,
            pokemon.defenseEV,
            pokemon.specialAttackEV,
            pokemon.specialDefenseEV,
            pokemon.speedEV
        )

        if (remainingEVs < 0) {
            issues.push(`pokemon in slot #${pokemon.teamSlot} has more than 508 EVs`)
        }
    }

    if (issues.length > 0) {
        return issues;
    }
}


// Helpers 
function hasDuplicates(arr) {
    return arr.length !== new Set(arr).size;
}
const MAX_EVS = 508;

export function calcRemainingEVs(hp, atk, def, spatk, spdef, speed) {
    return MAX_EVS - hp - atk - def- spatk - spdef - speed;
}
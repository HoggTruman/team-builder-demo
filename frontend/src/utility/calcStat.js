export function calcStat(statName, base, ev, iv, natureMultiplier, level) {
    if (
        [...arguments].every(x => x !== null) === false ||
        iv === ""
    ) {
        return null;
    }


    if (statName == "HP") {
        return Math.floor((2 * base + iv + Math.floor(ev/4)) * level / 100) + level + 10;
    }
    else {
        return Math.floor((Math.floor((2 * base + iv + Math.floor(ev/4)) * level / 100) + 5) * natureMultiplier);
    }
}
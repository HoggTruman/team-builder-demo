import { calcStat } from "./calcStat";

describe("calcStat Tests", () => {
    test("HP stat calculated correctly", () => {
        const statName = "HP";
        const base = 108;
        const ev = 74;
        const iv = 24;
        const natureMultiplier = 1;
        const level = 78;

        const actual = calcStat(statName, base, ev, iv, natureMultiplier, level);
        const expected = 289;

        expect(actual).toBe(expected);
    })

    test("Attack stat calculated correctly", () => {
        const statName = "Attack";
        const base = 130;
        const ev = 190;
        const iv = 12;
        const natureMultiplier = 1.1;
        const level = 78;

        const actual = calcStat(statName, base, ev, iv, natureMultiplier, level);
        const expected = 278;

        expect(actual).toBe(expected);
    })

    test("Defense stat calculated correctly", () => {
        const statName = "Defense";
        const base = 95;
        const ev = 91;
        const iv = 30;
        const natureMultiplier = 1;
        const level = 78;

        const actual = calcStat(statName, base, ev, iv, natureMultiplier, level);
        const expected = 193;

        expect(actual).toBe(expected);
    })

    test("Special Attack stat calculated correctly", () => {
        const statName = "Sp. Atk.";
        const base = 80;
        const ev = 48;
        const iv = 16;
        const natureMultiplier = 0.9;
        const level = 78;

        const actual = calcStat(statName, base, ev, iv, natureMultiplier, level);
        const expected = 135;

        expect(actual).toBe(expected);
    })

    test("Special Defense stat calculated correctly", () => {
        const statName = "Sp. Def.";
        const base = 85;
        const ev = 84;
        const iv = 23;
        const natureMultiplier = 1;
        const level = 78;

        const actual = calcStat(statName, base, ev, iv, natureMultiplier, level);
        const expected = 171;

        expect(actual).toBe(expected);
    })

    test("Speed stat calculated correctly", () => {
        const statName = "Speed";
        const base = 102;
        const ev = 23;
        const iv = 5;
        const natureMultiplier = 1;
        const level = 78;

        const actual = calcStat(statName, base, ev, iv, natureMultiplier, level);
        const expected = 171;

        expect(actual).toBe(expected);
    })
})
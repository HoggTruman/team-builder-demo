import {expect, jest, test} from '@jest/globals';
import { convertIV, convertLevel, getAbilityId, getItemId, getMoveId } from "./teamEditToTeam";



describe("convertLevel Tests", () => {
    test.each(
        [0, -10, 101, 10.5, "0", "-10", "101", "10.5", "ten", "hello", "", null, undefined]
    ) ("Bad level (%p) returns 100", (level) => {
        expect(convertLevel(level)).toStrictEqual(100);
    })

    test.each(
        [
            [1, 1],
            ["1", 1],
            [30, 30],
            ["30", 30],
            [100, 100],
            ["100", 100]
        ]
    ) ("Valid level (%p) returns (%p)", (level, expected) => {
        expect(convertLevel(level)).toStrictEqual(expected)
    })
})




describe("getItemId Tests", () => {
    // note: item identifiers are always lower case separated by hyphens
    const testItems = [
        {
            id: 1,
            identifier: "test-item1"
        },
        {
            id: 2,
            identifier: "test-item2"
        },
        {
            id: 3,
            identifier: "test-item3"
        },
    ];


    test.each([0, 1, 2]) ("returns correct id when itemName matches identifier strictly #%#", (index) => {
        const testItem = testItems[index];
        expect(getItemId(testItem.identifier, testItems)).toStrictEqual(testItem.id);
    })


    test.each(["", "cheese", "test item 1"]) ("returns null when itemName does not match any identifier", (itemName) => {
        expect(getItemId(itemName, testItems)).toBeNull()
    })


    test("returns id when itemName matches except for case and surrounding whitespace", () => {
        expect(getItemId("   TeST-itEm1   ", testItems)).toStrictEqual(1);
    })
})




describe("getAbilityId Tests", () => {
    // note: ability identifiers are always lower case separated by hyphens
    const testAbilities = [
        {
            id: 1,
            identifier: "test-ability1"
        },
        {
            id: 2,
            identifier: "test-ability2"
        },
        {
            id: 3,
            identifier: "test-ability3"
        },
    ];


    test("returns correct id when abilityName matches identifier strictly and the id is contained in validIds", () => {
        const testAbility = testAbilities[2];
        const validIds = [testAbility.id];
        expect(getAbilityId(testAbility.identifier, validIds, testAbilities)).toStrictEqual(testAbility.id);
    })


    test("returns null when abilityName matches identifier strictly but id is not contained in validIds", () => {
        const testAbility = testAbilities[2];
        const validIds = [1, 2];
        expect(getAbilityId(testAbility.identifier, validIds, testAbilities)).toBeNull();
    })

    
    test("returns null when abilityName does not match identifier", () => {
        const validIds = [1, 2, 3];
        expect(getAbilityId("bad-identifier", validIds, testAbilities)).toBeNull();
    })


    test("returns correct id when abilityName matches identifier except for case and surrounding whitespace with valid id", () => {
        const validIds = [1];
        expect(getAbilityId("  TeST-aBIlitY1  ", validIds, testAbilities)).toStrictEqual(1);
    })
})




describe("getMoveId Tests", () => {
    // note: move identifiers are always lower case separated by hyphens
    const testMoves = [
        {
            id: 1,
            identifier: "test-move1"
        },
        {
            id: 2,
            identifier: "test-move2"
        },
        {
            id: 3,
            identifier: "test-move3"
        },
    ];


    test("returns correct id when moveName matches identifier strictly and the id is contained in validIds", () => {
        const testMove = testMoves[2];
        const validIds = [testMove.id];
        expect(getMoveId(testMove.identifier, validIds, testMoves)).toStrictEqual(testMove.id);
    })


    test("returns null when moveName matches identifier strictly but id is not contained in validIds", () => {
        const testMove = testMoves[2];
        const validIds = [1, 2];
        expect(getMoveId(testMove.identifier, validIds, testMoves)).toBeNull();
    })

    
    test("returns null when moveName does not match identifier", () => {
        const validIds = [1, 2, 3];
        expect(getMoveId("bad-identifier", validIds, testMoves)).toBeNull();
    })


    test("returns correct id when moveName matches identifier except for case and surrounding whitespace with valid id", () => {
        const validIds = [1];
        expect(getMoveId("  TeST-MoVe1  ", validIds, testMoves)).toStrictEqual(1);
    })
})




describe("convertIV Tests", () => {
    test.each(
        [-1, 32, 10.5, 100, "-1", "32", "10.5", "100", "ten", "hello", "", null, undefined]
    ) ("Bad iv (%p) returns 31", (iv) => {
        expect(convertIV(iv)).toStrictEqual(31);
    })

    test.each(
        [
            [0, 0],
            ["0", 0],
            [31, 31],
            ["30", 30],
            [16, 16],
            ["16", 16]
        ]
    ) ("Valid iv (%p) returns (%p)", (iv, expected) => {
        expect(convertIV(iv)).toStrictEqual(expected)
    })
})


import createNewTeam from "../models/teamFactory";
import { generateLocalTeamId } from "./generateLocalTeamId";

describe("generateLocalTeamId tests", () => {
    test("Empty teams returns -1", () => {
        const testTeams = [];

        const result = generateLocalTeamId(testTeams)

        expect(result).toBe(-1);
    })


    test("Teams containing no negative ids returns -1", () => {
        const testTeams = [createNewTeam({id: 1}), createNewTeam({id: 2}), createNewTeam({id: 3})]

        const result = generateLocalTeamId(testTeams)

        expect(result).toBe(-1);
    })


    test.each([
        [-3, -4], [-20, -21], [-36, -37], [-10000, -10001]
    ]) ("Teams containing only negative ids returns next negative number", (minId, expected) => {
        const testTeams = [createNewTeam({id: -1}), createNewTeam({id: -2}), createNewTeam({id: minId})]

        const result = generateLocalTeamId(testTeams)

        expect(result).toBe(expected);
    })

    
    test.each([
        [-3, -4], [-20, -21], [-36, -37], [-10000, -10001]
    ]) ("Teams containing mixed ids returns next negative number", (minId, expected) => {
        const testTeams = [createNewTeam({id: 1}), createNewTeam({id: 5}), createNewTeam({id: -2}), createNewTeam({id: minId})]

        const result = generateLocalTeamId(testTeams)

        expect(result).toBe(expected);
    })
})
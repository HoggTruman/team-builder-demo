import { deletePokemonFromTeam } from "./deletePokemonFromTeam";


describe("deletePokemonFromTeam Tests", () => {
    test("Attempting to delete only pokemon returns unaltered team", () => {
        // Arrange
        const team = {
            teamName: "testTeam",
            pokemon: [
                {
                    id: 1,
                    teamSlot: 1
                },
            ]
        };

        const deleteSlot = 1;


        // Act
        const result = deletePokemonFromTeam(team, deleteSlot);


        // Assert
        expect(result.pokemon).toHaveLength(1);
        expect(result.pokemon.find(x => x.teamSlot == 1)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 1).id).toBe(1);
    })


    test("Delete last pokemon from team with pokemon in order", () => {
        // Arrange
        const team = {
            teamName: "testTeam",
            pokemon: [
                {
                    id: 1,
                    teamSlot: 1
                },
                {
                    id: 2,
                    teamSlot: 2
                },
                {
                    id: 3,
                    teamSlot: 3
                },
                {
                    id: 4,
                    teamSlot: 4
                }
            ]
        };

        const deleteSlot = 4;

        // Act
        const result = deletePokemonFromTeam(team, deleteSlot);


        // Asserts
        expect(result.pokemon).toHaveLength(3);
        expect(result.pokemon.find(x => x.teamSlot == 1)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 1).id).toBe(1);
        expect(result.pokemon.find(x => x.teamSlot == 2)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 2).id).toBe(2);
        expect(result.pokemon.find(x => x.teamSlot == 3)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 3).id).toBe(3);
    })


    test("Delete first pokemon from team with pokemon in order", () => {
        // Arrange
        const team = {
            teamName: "testTeam",
            pokemon: [
                {
                    id: 1,
                    teamSlot: 1
                },
                {
                    id: 2,
                    teamSlot: 2
                },
                {
                    id: 3,
                    teamSlot: 3
                },
                {
                    id: 4,
                    teamSlot: 4
                }
            ]
        }

        const deleteSlot = 1;

        // Act
        const result = deletePokemonFromTeam(team, deleteSlot);


        // Asserts
        expect(result.pokemon).toHaveLength(3);
        expect(result.pokemon.find(x => x.teamSlot == 1)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 1).id).toBe(2);
        expect(result.pokemon.find(x => x.teamSlot == 2)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 2).id).toBe(3);
        expect(result.pokemon.find(x => x.teamSlot == 3)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 3).id).toBe(4);
    })


    test("Delete middle pokemon from team with pokemon in order", () => {
        // Arrange
        const team = {
            teamName: "testTeam",
            pokemon: [
                {
                    id: 1,
                    teamSlot: 1
                },
                {
                    id: 2,
                    teamSlot: 2
                },
                {
                    id: 3,
                    teamSlot: 3
                },
                {
                    id: 4,
                    teamSlot: 4
                }
            ]
        }

        const deleteSlot = 2;

        // Act
        const result = deletePokemonFromTeam(team, deleteSlot);


        // Asserts
        expect(result.pokemon).toHaveLength(3);
        expect(result.pokemon.find(x => x.teamSlot == 1)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 1).id).toBe(1);
        expect(result.pokemon.find(x => x.teamSlot == 2)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 2).id).toBe(3);
        expect(result.pokemon.find(x => x.teamSlot == 3)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 3).id).toBe(4);
    })




    test("Delete last pokemon from team with pokemon out of order", () => {
        // Arrange
        const team = {
            teamName: "testTeam",
            pokemon: [
                {
                    id: 3,
                    teamSlot: 3
                },
                {
                    id: 1,
                    teamSlot: 1
                },
                {
                    id: 4,
                    teamSlot: 4
                },
                {
                    id: 2,
                    teamSlot: 2
                }
            ]
        };

        const deleteSlot = 4;

        // Act
        const result = deletePokemonFromTeam(team, deleteSlot);


        // Asserts
        expect(result.pokemon).toHaveLength(3);
        expect(result.pokemon.find(x => x.teamSlot == 1)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 1).id).toBe(1);
        expect(result.pokemon.find(x => x.teamSlot == 2)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 2).id).toBe(2);
        expect(result.pokemon.find(x => x.teamSlot == 3)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 3).id).toBe(3);
    })


    test("Delete first pokemon from team with pokemon out order", () => {
        // Arrange
        const team = {
            teamName: "testTeam",
            pokemon: [
                {
                    id: 3,
                    teamSlot: 3
                },
                {
                    id: 1,
                    teamSlot: 1
                },
                {
                    id: 4,
                    teamSlot: 4
                },
                {
                    id: 2,
                    teamSlot: 2
                }
            ]
        }

        const deleteSlot = 1;

        // Act
        const result = deletePokemonFromTeam(team, deleteSlot);


        // Asserts
        expect(result.pokemon).toHaveLength(3);
        expect(result.pokemon.find(x => x.teamSlot == 1)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 1).id).toBe(2);
        expect(result.pokemon.find(x => x.teamSlot == 2)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 2).id).toBe(3);
        expect(result.pokemon.find(x => x.teamSlot == 3)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 3).id).toBe(4);
    })


    test("Delete middle pokemon from team with pokemon in order", () => {
        // Arrange
        const team = {
            teamName: "testTeam",
            pokemon: [
                {
                    id: 3,
                    teamSlot: 3
                },
                {
                    id: 1,
                    teamSlot: 1
                },
                {
                    id: 4,
                    teamSlot: 4
                },
                {
                    id: 2,
                    teamSlot: 2
                }
            ]
        }

        const deleteSlot = 2;

        // Act
        const result = deletePokemonFromTeam(team, deleteSlot);


        // Asserts
        expect(result.pokemon).toHaveLength(3);
        expect(result.pokemon.find(x => x.teamSlot == 1)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 1).id).toBe(1);
        expect(result.pokemon.find(x => x.teamSlot == 2)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 2).id).toBe(3);
        expect(result.pokemon.find(x => x.teamSlot == 3)).not.toBeUndefined();
        expect(result.pokemon.find(x => x.teamSlot == 3).id).toBe(4);
    })
})
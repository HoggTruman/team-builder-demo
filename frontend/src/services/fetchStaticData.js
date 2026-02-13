import { getAllAbilities } from "./api/abilityAPI";
import { getAllGenders } from "./api/genderAPI";
import { getAllItems } from "./api/itemAPI";
import { getAllMoves } from "./api/moveAPI";
import { getAllNatures } from "./api/natureAPI";
import { getAllPokemon } from "./api/pokemonAPI";
import { getAllTypes } from "./api/typeAPI";



export async function fetchStaticData() {
    const abilities = await getAllAbilities();
    const genders = await getAllGenders();
    const items = await getAllItems();
    const moves = await getAllMoves();
    const natures = await getAllNatures();
    const pokemon = await getAllPokemon();
    const types = await getAllTypes();

    if (
        abilities !== undefined &&
        genders !== undefined &&
        items !== undefined &&
        moves !== undefined &&
        natures !== undefined &&
        pokemon !== undefined &&
        types !== undefined
    ) {
        return {
            abilities: abilities,
            genders: genders,
            items: items,
            moves: moves,
            natures: natures,
            pokemon: pokemon,
            types: types
        };
    }

    return null;
}
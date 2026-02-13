import axios from "axios";

const API = "http://localhost:5110/api/move";


export async function getAllMoves() {
    try {
        const response = await axios.get(API, {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}

export async function getMovesByPokemonId(pokemonId) {
    try {
        const response = await axios.get(API.concat("/", pokemonId), {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}
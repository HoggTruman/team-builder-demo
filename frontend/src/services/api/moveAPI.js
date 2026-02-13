import axios from "axios";
import path from "path";
import { API_BASE } from "./apiInfo";

const API = path.join(API_BASE, "move");


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
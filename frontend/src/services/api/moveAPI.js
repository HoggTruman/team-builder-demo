import axios from "axios";
import { API_BASE } from "./baseAPI";

axios.defaults.baseURL = API_BASE;
const API = "/move";

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
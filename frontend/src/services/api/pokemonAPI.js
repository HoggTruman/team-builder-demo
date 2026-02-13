import axios from "axios";

const API = "http://localhost:5110/api/pokemon";


export async function getAllPokemon() {
    try {
        const response = await axios.get(API, {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}

export async function getPokemonById(id) {
    try {
        const response = await axios.get(API.concat("/", id), {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}
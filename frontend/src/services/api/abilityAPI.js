import axios from "axios";
import { API_BASE } from "./baseAPI";

axios.defaults.baseURL = API_BASE;
const API = "/ability";

export async function getAllAbilities() {
    try {
        const response = await axios.get(API, {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}

export async function getAbilityById(id) {
    try {
        const response = await axios.get(API + "/" + id, {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}
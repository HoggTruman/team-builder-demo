import axios from "axios";
import { API_BASE } from "./baseAPI";

axios.defaults.baseURL = API_BASE;
const API = "/type";

export async function getAllTypes() {
    try {
        const response = await axios.get(API, {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}

export async function getTypeById(id) {
    try {
        const response = await axios.get(API.concat("/", id), {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}
import axios from "axios";

const API = "http://localhost:5110/api/nature";


export async function getAllNatures() {
    try {
        const response = await axios.get(API, {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}

export async function getNatureById(id) {
    try {
        const response = await axios.get(API.concat("/", id), {
            validateStatus: status => status === 200,
        });
        return response.data;
    } catch (error) {
        console.error(error);
    }
}
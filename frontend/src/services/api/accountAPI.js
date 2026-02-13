import axios from "axios";
import { API_BASE } from "./baseAPI";

axios.defaults.baseURL = API_BASE;
const API = "/account"

export async function loginAPI(userName, password) {
    try {
        const response = await axios.post(
            API + "/login",
            {
                userName: userName,
                password: password
            }, 
            {
                validateStatus: status => status === 200,
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
        return error;
    }
}


export async function registerAPI(userName, password, confirmPassword) {
    try {
        const response = await axios.post(
            API + "/register",
            {
                userName: userName,
                password: password,
                confirmPassword: confirmPassword
            },            
            {
                validateStatus: status => status === 200,
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
        return error;
    }
}
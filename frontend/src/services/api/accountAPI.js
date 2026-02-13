import axios from "axios";

const API = "http://localhost:5110/api/account"

export async function loginAPI(userName, password) {
    try {
        const response = await axios.post(
            API.concat("/", "login"),
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
            API.concat("/", "register"),
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
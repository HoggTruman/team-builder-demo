import { loginAPI, registerAPI } from "../services/api/accountAPI";
import { createContext, useContext, useEffect, useState } from "react";


const UserContext = createContext();

export function UserProvider(props) {
    const [token, setToken] = useState(null);
    const [userName, setUserName] = useState(null);
    const [isReady, setIsReady] = useState(false);

    useEffect(() => {
        const storageToken = localStorage.getItem("token");
        const storageUserName = localStorage.getItem("userName");

        if (storageToken && storageUserName) {
            setToken(storageToken);
            setUserName(storageUserName);
        }

        setIsReady(true);
    }, [])

    async function login(userName, password) {
        const loginData = await loginAPI(userName, password);
        if (loginData?.userName && loginData?.token) {
            localStorage.setItem("token", loginData.token);
            setToken(loginData.token);

            localStorage.setItem("userName", loginData.userName);
            setUserName(loginData.userName);
        }
        return loginData;
    }

    async function register(userName, password, confirmPassword) {
        const registerData = await registerAPI(userName, password, confirmPassword);
        if (registerData?.userName && registerData?.token) {
            localStorage.setItem("token", registerData.token);
            setToken(registerData.token);

            localStorage.setItem("userName", registerData.userName);
            setUserName(registerData.userName);
        }
        return registerData;
    }

    function logout() {
        localStorage.removeItem("token");
        setToken(null);

        localStorage.removeItem("userName");
        setUserName(null);
    }

    const isLoggedIn = () => token !== null;

    return(
        <UserContext.Provider value={{login, register, logout, isLoggedIn, token, userName}}>
            {isReady? props.children: null}
        </UserContext.Provider>
    );
}


export const userContext = () => useContext(UserContext);
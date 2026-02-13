import React from "react";
import { userContext } from "../context/userContext";

import "./AccountPage.css"
import { TEAM_LIST_PAGE } from "./constants/pageNames";
import { getAllTeamsAPI } from "../services/api/teamAPI";



function AccountPage(props) {
    const { login, register, isLoggedIn } = userContext();


    

    async function handleSubmitLogin(e) {
        e.preventDefault();
        if (isLoggedIn()) {
            return;
        }
            

        const data = new FormData(e.target);  
        const formData = Object.fromEntries(data.entries());

        // Input validation
        if (formData.userName.length < 6) {
            return alert("Username must be at least 6 characters");
        }

        if (formData.password.length < 6) {
            return alert("Password must be at least 6 characters");
        }

        // Make login request
        const loginResult = await login(formData.userName, formData.password);

        // Evaluate response
        if (loginResult?.message === "Request failed with status code 401") {
            return alert("Incorrect username/password");
        }
        else if (loginResult?.message) {
            return alert("Try again later");
        }
        
        // Fetch server teams and navigate to list page
        const newServerTeams = await getAllTeamsAPI(loginResult?.token);

        if (newServerTeams) {
            props.setServerTeams(teams => newServerTeams);
        }
        
        props.setPage(TEAM_LIST_PAGE);
        
    } 


    async function handleSubmitRegister(e) {
        e.preventDefault();
        if (isLoggedIn()) {
            return;
        }



        const data = new FormData(e.target);  
        const formData = Object.fromEntries(data.entries());

        // Input validation
        if (formData.userName.length < 6) {
            return alert("Username must be at least 6 characters");
        }

        if (formData.password.length < 6) {
            return alert("Password must be at least 6 characters");
        }

        if (formData.password !== formData.confirmPassword) {
            return alert("Passwords do not match");
        }

        // Make Request
        const registerResult = await register(formData.userName, formData.password, formData.confirmPassword);

        // Evaluate response
        if (registerResult?.message) {
            // This could be due to a taken username, or server error, but there is no way to differentiate the two from the response currently
            return alert("Username is taken, please choose a different username");
        }
        // else if (registerResult?.message) {
        //     return alert("Try again later");
        // }
        else {
            props.setPage(TEAM_LIST_PAGE);
        }
    }




    // Render
    return (
        <div id="AccountPage">
            <div id="sectionsContainer">
                <div id="loginSection">
                    <h2>Log In</h2>
                    <form 
                        id="loginForm"
                        onSubmit={e => handleSubmitLogin(e)}
                    >
                        <label htmlFor="loginUserName" id="userNameLabel">Username: </label>
                        <input 
                            id="loginUserName"
                            name="userName"
                            type="text"
                        />

                        <br/>

                        <label htmlFor="loginPassword">Password: </label>
                        <input 
                            id="loginPassword"
                            name="password"
                            type="password"
                        />

                        <br/>

                        <input
                            id="loginSubmit"
                            type="submit"
                            value="Log In"
                        />
                    </form>
                </div>

                <div id="registerSection">
                <h2>Register</h2>
                    <form 
                        id="registerForm"
                        onSubmit={e => handleSubmitRegister(e)}
                    >
                        <label htmlFor="registerUserName">Username: </label>
                        <input 
                            id="registerUserName"
                            name="userName"
                            type="text"
                        />

                        <br/> 

                        <label htmlFor="registerPassword">Password: </label>
                        <input 
                            id="registerPassword"
                            name="password"
                            type="password"
                        />

                        <br/>

                        <label htmlFor="registerConfirmPassword">Confirm Password: </label>
                        <input 
                            id="registerConfirmPassword"
                            name="confirmPassword"
                            type="password"
                        />

                        <br/>

                        <input
                            id="registerSubmit"
                            type="submit"
                            value="Register"
                        />
                    </form>
                </div>
            </div>
        </div>
    )
}

export default AccountPage;
import axios from "axios";

const TEAM_API = "http://localhost:5110/api/team";
const TEAMS_API = "http://localhost:5110/api/teams";




export async function getAllTeamsAPI(token) {
    try {
        const response = await axios.get(TEAMS_API,
            {
                validateStatus: status => status === 200,
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
    }
}


export async function createTeamsAPI(teams, token) {
    try {
        const response = await axios.post(TEAMS_API, teams,
            {
                validateStatus: status => status === 200,
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
    }
}




export async function createTeamAPI(team, token) {
    try {
        const response = await axios.post(TEAM_API, team,
            {
                validateStatus: status => status === 201,
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
    }
}


export async function getTeamByIdAPI(id, token) {
    try {
        const response = await axios.post(TEAM_API.concat('/', id),
            {
                validateStatus: status => status === 200,
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
    }
}


export async function updateTeamByIdAPI(id, team, token) {
    try {
        const response = await axios.put(TEAM_API.concat('/', id), team,
            {
                validateStatus: status => status === 200,
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
    }
}


export async function deleteTeamByIdAPI(id, token) {
    try {
        const response = await axios.delete(TEAM_API.concat('/', id),
            {
                validateStatus: status => status === 204,
                headers: {
                    Authorization: `Bearer ${token}`
                }
            }
        );

        return response.data;
    } 
    catch (error) {
        console.error(error)
    }
}



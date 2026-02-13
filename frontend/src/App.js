import React, { useEffect, useState } from "react";

import PageSelector from "./components/PageSelector";
import TopBar from "./components/TopBar/TopBar";
import { TEAM_LIST_PAGE } from "./pages/constants/pageNames";


import { fetchStaticData } from "./services/fetchStaticData";
import { userContext } from "./context/userContext";
import { getLocalStorageTeams, setLocalStorageTeams } from "./utility/teamStorage";
import { getAllTeamsAPI } from "./services/api/teamAPI";

import "./App.css";




// Fetch static data
const staticData = await fetchStaticData();



// App
function App() {
    const { isLoggedIn, token } = userContext();

    const [page, setPage] = useState(TEAM_LIST_PAGE);
    const [serverTeams, setServerTeams] = useState([]);
    const [localTeams, setLocalTeams] = useState(getLocalStorageTeams() || []);
    const [activeTeamId, setActiveTeamId] = useState(0);

    // Fetch static data
    if (staticData == null) {
        return <p> Page could not load. try again</p>
    }
    const data = staticData;

    // Fetch User Teams if logged in
    useEffect(() => {
        async function fetchServerTeams() {
            if (isLoggedIn()) {
                const newServerTeams = await getAllTeamsAPI(token);

                if (newServerTeams) {
                    setServerTeams(teams => newServerTeams);
                }
            }
        }

        fetchServerTeams();
    }, [])

    // Update local storage
    useEffect(() => {
        setLocalStorageTeams(localTeams);
    }, [localTeams]);


    // Render
    return (
        <>
            <TopBar 
                page={page}
                setPage={setPage}
                setServerTeams={setServerTeams}
            />
            <PageSelector 
                page={page}
                setPage={setPage}
                serverTeams={serverTeams}
                setServerTeams={setServerTeams}
                localTeams={localTeams}
                setLocalTeams={setLocalTeams}
                activeTeamId={activeTeamId}
                setActiveTeamId={setActiveTeamId}
                data={data}
            />
        </>
    )
}
  
export default App;
import React from "react";
import TeamList from "../components/TeamListPage/TeamList";
import createNewTeam from "../models/teamFactory";
import { ACCOUNT_PAGE, TEAM_EDIT_PAGE } from "./constants/pageNames";
import { userContext } from "../context/userContext";
import { createTeamsAPI, getAllTeamsAPI } from "../services/api/teamAPI";
import { generateLocalTeamId } from "../utility/generateLocalTeamId";


import "./TeamListPage.css";


function TeamListPage(props) {
    const { token, isLoggedIn } = userContext();

    async function handleClickGetTeamsButton() {
        if (isLoggedIn() === false) {
            alert("Log in to access the server");
            props.setPage(ACCOUNT_PAGE);
            return;
        }

        const newServerTeams = await getAllTeamsAPI(token);

        if (newServerTeams === undefined) {
            return alert("Failed to retrieve teams from server. Please try again");
        }

        props.setServerTeams(teams => newServerTeams)
    }


    async function handleClickSaveLocalTeamsButton() {
        if (isLoggedIn() === false) {
            alert("Log in to access the server");
            props.setPage(ACCOUNT_PAGE);
            return;
        }

        if (props.localTeams.length === 0) {
            return;
        }

        const newServerTeams = await createTeamsAPI(props.localTeams, token);

        if (newServerTeams === undefined) {
            return alert("Save failed. Please try again");
        }

        alert("Save Successful");
        props.setServerTeams(teams => teams.concat(newServerTeams));
        props.setLocalTeams(teams => [])
    }




    function handleClickNewTeamButton() {
        const newTeam = createNewTeam({id: generateLocalTeamId(props.localTeams)});  // Use negative id for new teams/pokemon

        props.setLocalTeams(teams => teams.concat(newTeam));
        
        props.setActiveTeamId(newTeam.id);
        props.setPage(TEAM_EDIT_PAGE);
    }



    // Render 
    return (
        <div id="teamListPage">
            <h1>Select a Team or Create a New One</h1>

            <div id="teamListPageButtons">
                <button
                    id="newTeamButton"
                    onClick={handleClickNewTeamButton}
                >
                    + Create New Team
                </button>

                <button
                    id="getTeamsButton"
                    onClick={handleClickGetTeamsButton}
                >
                    ↓ Get Teams From Server
                </button>

                <button
                    id="saveTeamsButton"
                    onClick={handleClickSaveLocalTeamsButton}
                >
                    ↑ Save Local Teams To Server 
                </button>
            </div>


            <div id="serverTeamsSection">
                <h2 className="teamsHeader">
                    {`Server Teams (${props.serverTeams.length})`}
                </h2>
                <TeamList
                    setPage={props.setPage}
                    teams={props.serverTeams}
                    setTeams={props.setServerTeams}
                    setActiveTeamId={props.setActiveTeamId}
                    data={props.data}
                />
                <div>
                    {
                        props.serverTeams.length === 0? 
                            isLoggedIn()?
                                "Get your teams from the server, or save a local team!":
                                "Log in to retrieve your teams from the server!": 
                            ""
                    }
                </div>
            </div>

            
            <div id="localTeamsSection">
                <h2 className="teamsHeader">
                    {`Local Teams (${props.localTeams.length})`}
                </h2>
                <TeamList
                    setPage={props.setPage}
                    teams={props.localTeams}
                    setTeams={props.setLocalTeams}
                    setActiveTeamId={props.setActiveTeamId}
                    data={props.data}
                />
                <div>
                    {
                        props.localTeams.length === 0? 
                            isLoggedIn()?
                                "All your teams are saved to the server!":
                                "Create a new team to get started!": 
                            ""
                    }
                </div>
            </div>
        </div>
    )
}



export default TeamListPage;
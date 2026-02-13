import React from "react";

import { TEAM_EDIT_PAGE } from "../../pages/constants/pageNames";
import { pokemonIcons } from "../../assets/assets";
import { deleteTeamByIdAPI, updateTeamByIdAPI } from "../../services/api/teamAPI";
import { userContext } from "../../context/userContext";

import "./TeamInList.css";





function TeamInList(props) {
    const { token } = userContext();


    function handleClickTeamButton() {
        props.setPage(TEAM_EDIT_PAGE);
        props.setActiveTeamId(props.team.id);
    }


    async function handleClickDeleteTeamButton() {
        if (!confirm(`Are you sure you want to delete "${props.team.teamName}"`)) {
            return;
        }

        if (props.team.id > 0) {
            const result = await deleteTeamByIdAPI(props.team.id, token);
            if (result === undefined) {
                return alert("Failed to delete team. Please try again")
            }
        }

        props.setTeams(teams => {
            return teams.filter(team => team.id != props.team.id);
        });
    }


    async function handleClickRenameTeamButton() {
        let newTeamName = prompt("New Team Name:", props.team.teamName);

        if (newTeamName == null || newTeamName === "" || newTeamName === props.team.teamName) {
            return;
        }

        let newTeam = {...props.team};
        newTeam.teamName = newTeamName;

        if (newTeam.id > 0) {
            const result = await updateTeamByIdAPI(newTeam.id, newTeam, token);
            if (result === undefined) {
                return alert("Failed to rename team. Please try again")
            }
        }

        props.setTeams(teams => {
            props.team.teamName = newTeamName;
            return [...teams];
        })
    }


    function getIcon(pokemonId) {
        const pokemon = props.data.pokemon.find(x => x.id == pokemonId);
        const identifier = pokemon?.identifier || "_unknown";
        return pokemonIcons[identifier]
    }



    // Render
    let icons = props.team.pokemon.map(pokemon => (
        <div
            key={pokemon.id} 
            className="iconFrame"
        >
            <img 
                src={getIcon(pokemon.pokemonId)}
                className="icon"
            />
        </div>
    ));

    return (
        <div 
            className="teamInList"
            key={props.id}
        >
            <button 
                className="teamButton" 
                onClick={handleClickTeamButton}
            >
                <p>{props.team.teamName}</p>
                <div className="teamIcons">
                    <span className="iconHelper"></span>
                    {icons}
                </div>
            </button>

            <button
                className="renameTeamButton"
                onClick={handleClickRenameTeamButton}
            >
                Rename
            </button>

            <button
                className="deleteTeamButton"
                onClick={handleClickDeleteTeamButton}
            >
                Delete
            </button>
        </div>
    )
}


export default TeamInList;
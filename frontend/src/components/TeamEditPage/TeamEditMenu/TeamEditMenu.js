import React from "react";
import SelectPokemonButton from "./SelectPokemonButton";
import { TEAM_LIST_PAGE } from "../../../pages/constants/pageNames";
import createNewPokemonEdit from "../../../models/pokemonEditFactory";
import { deletePokemonFromTeam } from "../../../utility/deletePokemonFromTeam";
import { updateTeamByIdAPI } from "../../../services/api/teamAPI";
import { teamEditToTeam } from "../../../mappers/teamEditToTeam";
import { userContext } from "../../../context/userContext";
import { checkValidTeam } from "../../../utility/checkValidTeam";

import "./TeamEditMenu.css";




function TeamEditMenu(props) {
    const { token } = userContext();
    
    function handleClickAddPokemonButton()
    {
        if (props.teamEdit.pokemon.length >= 6)
        {
            return;
        }

        const newTeamSlot = props.teamEdit.pokemon.length + 1;
            
        props.setTeamEdit(team => {
            team.pokemon.push(createNewPokemonEdit({ teamSlot: newTeamSlot }));

            return {...team};
        });

        props.setActiveTeamSlot(newTeamSlot);
    }


    function handleClickDeletePokemonButton() {
        if (props.teamEdit.pokemon.length == 1) {
            return;
        }
        
        props.setTeamEdit(team => {
            team = deletePokemonFromTeam(team, props.activeTeamSlot);

            return {...team};
        })


        props.setActiveTeamSlot(slot => Math.max(1, slot - 1));
    }


    async function handleClickSaveButton() {
        const modifiedTeam = teamEditToTeam(props.teamEdit, props.data);
        const teamErrors = checkValidTeam(modifiedTeam);

        if (teamErrors) {
            // Make user correct errors before saving
            const errorMessage = "NOT SAVED - Please correct the following issues and try again:\n\n";
            const errors = teamErrors.map(error => `- ${error}`).join('\n');
            return alert(errorMessage + errors);
        }


        if (modifiedTeam.id > 0) {
            // Make API call for server teams
            const result = await updateTeamByIdAPI(modifiedTeam.id, modifiedTeam, token);
            if (result === undefined) {
                return alert("Failed to save team. Please try again")
            }
        }

        props.setTeams(teams => {
            const newTeams = teams.map(team => team.id === modifiedTeam.id? modifiedTeam: team);
            return newTeams;
        })
    }


    function handleClickBackButton() {
        if (props.teamHasUnsavedChanges() === false || confirm("You have unsaved changes, are you sure you want to go back?")) {
            props.setPage(TEAM_LIST_PAGE);
        }
    }



    // Render
    let pokemonButtons = props.teamEdit.pokemon.map(pokemon => (
        <SelectPokemonButton
            key={pokemon.id || `${pokemon.teamSlot}${Date.now()}`}
            pokemon={pokemon} 
            activeTeamSlot={props.activeTeamSlot}
            setActiveTeamSlot={props.setActiveTeamSlot}
            data={props.data}
        />
    ))

    let addPokemonButton = (
        <button
            id="addPokemonButton"
            className="menuButton"
            onClick={handleClickAddPokemonButton}
        >
            <img
                src=""
                alt="+"
            />
            <p>Add Pokemon</p>
        </button>
    );

    return (
        <div id="teamEditMenu">
            <button
                className="menuButton back"
                onClick={handleClickBackButton}
            >
                {"< Teams"}
            </button>

            <div id="selectPokemonButtons">
                {pokemonButtons}
                {pokemonButtons.length < 6? addPokemonButton: null}
            </div>

            <button 
                id="deletePokemonButton"
                className="menuButton"
                disabled={props.teamEdit.pokemon.length == 1}
                onClick={handleClickDeletePokemonButton}
            >
                <img
                    src=""
                    alt="-"
                />
                <p>Delete Pokemon</p>
            </button>

            <button
                id="saveTeamButton"
                className="menuButton"
                disabled={props.teamHasUnsavedChanges() === false}
                onClick={handleClickSaveButton}
            >
                Save Changes
            </button>
        </div>
    )
}


export default TeamEditMenu;
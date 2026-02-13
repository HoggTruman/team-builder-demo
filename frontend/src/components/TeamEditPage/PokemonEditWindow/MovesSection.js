import React from "react";

import { MOVE1_FIELD, MOVE2_FIELD, MOVE3_FIELD, MOVE4_FIELD } from "./constants/fieldNames";

import "./MovesSection.css";


function MovesSection(props) {
    function handleClickMoveInput(fieldName) {
        props.setActiveField(fieldName)
    }

    function handleChangeMoveInput(e, teamEditKey) {
        props.setTeamEdit(team => {
            props.activePokemon[teamEditKey] = e.target.value;
            return {...team};
        })
    }


    // Render
    return (
        <div id="movesSection">
            <div className="moveContainer">
                <label htmlFor="move1Input">Move 1 </label>
                <input 
                    id="move1Input"
                    type="text"
                    name={MOVE1_FIELD}
                    onClick={() => handleClickMoveInput(MOVE1_FIELD)}
                    onChange={e => handleChangeMoveInput(e, "move1Name")}
                    value={props.activePokemon.move1Name}
                />
            </div>

            <div className="moveContainer">
                <label htmlFor="move2Input">Move 2 </label>
                <input 
                    id="move2Input"
                    type="text"
                    name={MOVE2_FIELD}
                    onClick={() => handleClickMoveInput(MOVE2_FIELD)}
                    onChange={e => handleChangeMoveInput(e, "move2Name")}
                    value={props.activePokemon.move2Name}
                />
            </div>

            <div className="moveContainer">
                <label htmlFor="move3Input">Move 3 </label>
                <input 
                    id="move3Input"
                    type="text"
                    name={MOVE3_FIELD}
                    onClick={() => handleClickMoveInput(MOVE3_FIELD)}
                    onChange={e => handleChangeMoveInput(e, "move3Name")}
                    value={props.activePokemon.move3Name}
                />
            </div>

            <div className="moveContainer">
                <label htmlFor="move4Input">Move 4 </label>
                <input 
                    id="move4Input"
                    type="text"
                    name={MOVE4_FIELD}
                    onClick={() => handleClickMoveInput(MOVE4_FIELD)}
                    onChange={e => handleChangeMoveInput(e, "move4Name")}
                    value={props.activePokemon.move4Name}
                />
            </div>
        </div>
    );
}

export default MovesSection;
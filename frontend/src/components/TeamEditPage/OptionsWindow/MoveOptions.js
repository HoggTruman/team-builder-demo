import React from "react";
import { FixedSizeList } from "react-window";
import MoveOptionsRow from "./MoveOptionsRow";
import { MOVE1_FIELD, MOVE2_FIELD, MOVE3_FIELD, MOVE4_FIELD } from "../PokemonEditWindow/constants/fieldNames";

import "./OptionsTable.css";
import "./MoveOptions.css"



function MoveOptions(props) {
    function handleClickOptionRow(identifier) {
        props.setTeamEdit(team => {
            switch (props.activeField) {
                case MOVE1_FIELD:
                    props.activePokemon.move1Name = identifier;
                    break;
                case MOVE2_FIELD:
                    props.activePokemon.move2Name = identifier;
                    break;
                case MOVE3_FIELD:
                    props.activePokemon.move3Name = identifier;
                    break;
                case MOVE4_FIELD:
                    props.activePokemon.move4Name = identifier;
                    break;
            }
            
            return {...team};
        })
    }


    // Render
    let moveOptionsRows;

    if (props.moveList.length === 0) {
        moveOptionsRows = <div className="noMatches">No Moves Found</div>
    }
    else {
        moveOptionsRows = (
            <FixedSizeList
                height={390}
                width={1100}
                itemSize={50}
                itemCount={props.moveList.length}
                itemData={{moves: props.moveList, handleClick: handleClickOptionRow}}
                style={{overflowY: "scroll"}}
            >
                {MoveOptionsRow}
            </FixedSizeList>
        );
    }

    return (
        <div id="moveOptionsTable" className="optionsTable">
            <div className="row move header">
                <div className="col name">Name</div>
                <div className="col type">Type</div>
                <div className="col category">Cat</div>
                <div className="col stat">Pow</div>
                <div className="col stat">Acc</div>
                <div className="col stat">PP</div>
                <div className="col effect">Effect</div>
            </div>
            { moveOptionsRows }
        </div>
    );
}

export default MoveOptions;
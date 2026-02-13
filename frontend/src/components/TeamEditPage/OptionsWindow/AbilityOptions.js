import React from "react";
import { FixedSizeList } from "react-window";
import AbilityOptionsRow from "./AbilityOptionsRow";

import "./OptionsTable.css";
import "./AbilityOptions.css";


function AbilityOptions(props) {
    function handleClickOptionRow(identifier) {
        props.setTeamEdit(team => {
            props.activePokemon.abilityName = identifier;
            return {...team};
        })
    }


    // Render
    let abilityOptionsRows;

    if (props.abilityList.length === 0) {
        abilityOptionsRows = <div className="noMatches">No Abilities Found</div>
    }
    else {
        abilityOptionsRows = (
            <FixedSizeList
                height={390}
                width={1100}
                itemSize={50}
                itemCount={props.abilityList.length}
                itemData={{abilities: props.abilityList, handleClick: handleClickOptionRow}}
                style={{overflowY: "scroll"}}
            >
                {AbilityOptionsRow}
            </FixedSizeList>
        );
    }

    return (
        <div id="abilityOptionsTable" className="optionsTable">
            <div className="row ability header">
                <div className="col name">Ability</div>
                <div className="col effect">Effect</div>
            </div>
            { abilityOptionsRows }
        </div>
    );
}

export default AbilityOptions;
import React from "react";

function AbilityOptionsRow(props) {
    const { index, style } = props;
    const ability = props.data.abilities[index];

    return (
        <button 
            className="row ability"
            style={style}
            onClick={() => props.data.handleClick(ability.identifier)}
        >
            <div className="col name">
                {ability.identifier}
            </div>
            <div className="col effect">
                {ability.flavorText}
            </div>
        </button>
    )
}

export default AbilityOptionsRow;
import React from "react";
import { damageCategoryImages, typeImages } from "../../../assets/assets";

function MoveOptionsRow(props) {
    const { index, style } = props;
    const move = props.data.moves[index];

    return (
        <button 
            className="row move"
            style={style}
            onClick={() => props.data.handleClick(move.identifier)}
        >
            <div className="col name">{move.identifier}</div>
            <div className="col type">
                <img
                    src={typeImages[move.type]}
                    alt={move.type}
                    loading="lazy"
                />
            </div>
            <div className="col category">
                <img
                    src={damageCategoryImages[move.damageClass]}
                    alt={move.damageClass}
                    loading="lazy"
                />
            </div>
            <div className="col stat">{move.power || "-"}</div>
            <div className="col stat">{move.accuracy || "-"}</div>
            <div className="col stat">{move.pp}</div>
            <div className="col effect">
                {
                    move.moveEffect
                        .replace('$effect_chance', move.moveEffectChance)
                }
            </div>
        </button>
    )
}

export default MoveOptionsRow;
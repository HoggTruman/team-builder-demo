import React from "react";
import { calcStat } from "../../../utility/calcStat";

function StatRow(props) {
    function onChangeEVRange(e) {
        props.setTeamEdit(team => {
            props.activePokemon[props.evKey] = e.target.value;
            return {...team};
        });
    }

    function onChangeIVInput(e) {
        let iv = e.target.value;

        if (e.target.value !== "") {
            iv = Math.max(e.target.min, Math.min(e.target.value, e.target.max));
        }

        props.setTeamEdit(team => {
            props.activePokemon[props.ivKey] = iv;
            return {...team};
        });
    }
    
    // Render
    return (
        <tr className="statRow">
            <td>
                <p>{props.statName}</p>
            </td>
            <td>
                <p>{props.baseStat || "?"}</p>
            </td>
            <td>
                <p className="evValue">
                    {
                    `${props.activePokemon[props.evKey]}`.concat(' ',
                        props.natureMultiplier > 1? "+": props.natureMultiplier < 1? "-": "")
                    }
                </p>
                <input 
                    type="range"
                    className="evSlider"
                    min={0} 
                    max={252} 
                    step={4} 
                    value={props.activePokemon[props.evKey]}
                    onChange={e => onChangeEVRange(e)}
                />
            </td>
            <td>
                <input 
                    type="number" 
                    min={0} 
                    max={31} 
                    value={props.activePokemon[props.ivKey]}
                    onChange={e => onChangeIVInput(e)}
                />
            </td>
            <td>
                <p>
                    {
                        calcStat(
                            props.statName, 
                            props.baseStat, 
                            Number(props.activePokemon[props.evKey]),
                            props.activePokemon[props.ivKey],
                            props.natureMultiplier,
                            props.activePokemon.level
                        ) || "?"
                    }
                </p>
            </td>
        </tr>
    );
}

export default StatRow;
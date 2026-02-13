import React from "react";
import { pokemonIcons, typeImages } from "../../../assets/assets";


function PokemonOptionsRow(props) {
    const { index, style } = props;
    const pokemon = props.data.pokemon[index];
    const data = props.data.data;
    
    return (
        <button 
            className="row pokemon"
            style={style}
            onClick={() => props.data.handleClick(pokemon.identifier)}
        >
            <div className="col icon">
                <img
                    src={pokemonIcons[pokemon.identifier]}
                    alt="icon"
                    loading="lazy"
                    className="pokemonIcon"
                />
            </div>
            <div className="col name">{pokemon.identifier}</div>
            <div className="col types">
                {
                    pokemon.types.map(type => (
                        <img
                            key={type}
                            src={typeImages[type]}
                            loading="lazy"
                            alt={type}
                        />
                    ))
                }
            </div>
            <div className="col abilities">
                {
                    pokemon.abilities.map(abilityId => (
                        <span key={abilityId}>
                            {data.abilities.find(x => x.id == abilityId)?.identifier}
                        </span>
                    ))
                }
            </div>
            <div className="col stat">{pokemon.baseStats.hp}</div>
            <div className="col stat">{pokemon.baseStats.attack}</div>
            <div className="col stat">{pokemon.baseStats.defense}</div>
            <div className="col stat">{pokemon.baseStats.specialAttack}</div>
            <div className="col stat">{pokemon.baseStats.specialDefense}</div>
            <div className="col stat">{pokemon.baseStats.speed}</div>
            <div className="col bst">{calcBST(pokemon.baseStats)}</div>
        </button>
    )
}



function calcBST(baseStats) {
    return Object.values(baseStats).reduce((a, b) => a + b, 0);
}



export default PokemonOptionsRow;
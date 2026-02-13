import React from "react";

import { POKEMON_FIELD } from "./constants/fieldNames";
import { pokemonNormalImages, pokemonShinyImages, typeImages } from "../../../assets/assets";
import { clean } from "../../../utility/cleanString";

import "./PokemonSection.css";



function PokemonSection(props) {
    function handleClickPokemonInput() {
        props.setActiveField(POKEMON_FIELD)
    }

    function handleChangePokemonInput(e) {
        props.setTeamEdit(team => {
            props.activePokemon.pokemonName = e.target.value;
            return {...team};
        })
    }


    function handleChangeNicknameInput(e) {
        props.setTeamEdit(team => {
            props.activePokemon.nickname = e.target.value;
            return {...team};
        });
    }


    // Get Images
    function getPokemonImage(name, shiny) {
        const cleanName = clean(name);
        const pokemonId = props.data.pokemon.find(x => x.identifier == cleanName)?.id;
    
        if (pokemonId == null) {
            return pokemonNormalImages["0"];
        }
    
        if (shiny) {
            return pokemonShinyImages[pokemonId];
        }
        else {
            return pokemonNormalImages[pokemonId];
        }
    }

    function getPokemonTypeImages(name) {
        const cleanName = clean(name);
        const pokemon = props.data.pokemon.find(x => x.identifier == cleanName);

        return pokemon?.types || ["unknown"];
    }


    // Render
    return (
        <div id="pokemonSection">
            <img 
                src={getPokemonImage(props.activePokemon.pokemonName, props.activePokemon.shiny)}
                id={"pokemonimg"}
            />
            <div
                className="types"
            >
                {
                    getPokemonTypeImages(props.activePokemon.pokemonName).map(type => (
                        <img
                            key={type}
                            src={typeImages[type]}
                            alt={type}
                        />
                    ))
                }
            </div>

            <label htmlFor="pokemonInput">Pokemon</label>
            <input 
                id="pokemonInput" 
                type="text" 
                name={POKEMON_FIELD}
                onClick={handleClickPokemonInput}
                onChange={e => handleChangePokemonInput(e)}
                value={props.activePokemon.pokemonName}
            />

            <label htmlFor="nicknameInput">Nickname</label>
            <input 
                id="nicknameInput" 
                type="text" 
                name="nickname" 
                placeholder={props.activePokemon.pokemonName || "nickname"}
                onChange={e => handleChangeNicknameInput(e)}
                value={props.activePokemon.nickname}
            />
        </div>
    );
}




export default PokemonSection
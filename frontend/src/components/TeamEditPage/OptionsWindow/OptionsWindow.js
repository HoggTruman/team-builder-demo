import React from "react";
import PokemonOptions from "./PokemonOptions";
import ItemOptions from "./ItemOptions";
import AbilityOptions from "./AbilityOptions";
import MoveOptions from "./MoveOptions";
import { ABILITY_FIELD, ITEM_FIELD, MOVE1_FIELD, MOVE2_FIELD, MOVE3_FIELD, MOVE4_FIELD, POKEMON_FIELD } from "../PokemonEditWindow/constants/fieldNames";
import { clean } from "../../../utility/cleanString";



function OptionsWindow(props) {
    let optionsTable;

    if (props.activeField == POKEMON_FIELD) {
        optionsTable = (
            <PokemonOptions 
                pokemonList={filterListByInput(props.data.pokemon, props.activePokemon.pokemonName)}
                setTeamEdit={props.setTeamEdit}
                activePokemon={props.activePokemon}
                data={props.data}
            />
        );
    } 
    else if (props.activeField == ITEM_FIELD) {
        optionsTable = (
            <ItemOptions 
                itemList={filterListByInput(props.data.items, props.activePokemon.itemName)}
                setTeamEdit={props.setTeamEdit}
                activePokemon={props.activePokemon}
            />
        );
    }
    else if (props.activeField == ABILITY_FIELD) {
        const abilities = props.data.pokemon.find(x => x.identifier == props.activePokemon.pokemonName)
            ?.abilities.map(abilityId => props.data.abilities.find(x => x.id == abilityId))
            || [];

        optionsTable = (
            <AbilityOptions 
                abilityList={filterListByInput(abilities, props.activePokemon.abilityName)}
                setTeamEdit={props.setTeamEdit}
                activePokemon={props.activePokemon}
            />
        );
    }
    else if (
        props.activeField == MOVE1_FIELD ||
        props.activeField == MOVE2_FIELD ||
        props.activeField == MOVE3_FIELD ||
        props.activeField == MOVE4_FIELD         
    ) {
        let moves = props.data.pokemon.find(x => x.identifier == props.activePokemon.pokemonName)
        ?.moves.map(moveId => props.data.moves.find(x => x.id == moveId))
        || [];

        let currentMoves = [
            props.activePokemon.move1Name, 
            props.activePokemon.move2Name, 
            props.activePokemon.move3Name, 
            props.activePokemon.move4Name
        ]


        optionsTable = (
            <MoveOptions 
                moveList={filterMovesList(moves, props.activeField, currentMoves)}
                activeField={props.activeField}
                setTeamEdit={props.setTeamEdit}
                activePokemon={props.activePokemon}
            />
        );
    }
    else {
        optionsTable = null;
    }

    return (
        <div id="optionsWindow">
            {optionsTable}
        </div>
    );
}

function filterListByInput(list, input) {
    const cleanInput = clean(input);

    return list.filter(x => x.identifier.includes(cleanInput));
}


// Filters options by current input + removes already selected moves from options
function filterMovesList(list, activeField, currentMoves) {
    let cleanInput;
    const cleanCurrentMoves = currentMoves.map(move => clean(move));

    if (activeField == MOVE1_FIELD) {
        cleanInput = cleanCurrentMoves[0];
        cleanCurrentMoves.splice(0, 1);
    }
    else if (activeField == MOVE2_FIELD) {
        cleanInput = cleanCurrentMoves[1];
        cleanCurrentMoves.splice(1, 1);
    }
    else if (activeField == MOVE3_FIELD) {
        cleanInput = cleanCurrentMoves[2];
        cleanCurrentMoves.splice(2, 1);
    }
    else if (activeField == MOVE4_FIELD) {
        cleanInput = cleanCurrentMoves[3];
        cleanCurrentMoves.splice(3, 1);
    }

    return list
        .filter(x => x.identifier.includes(cleanInput) &&
            cleanCurrentMoves.includes(x.identifier) === false)
        .sort((a, b) => a.identifier > b.identifier? 1 : 0);
}



export default OptionsWindow;
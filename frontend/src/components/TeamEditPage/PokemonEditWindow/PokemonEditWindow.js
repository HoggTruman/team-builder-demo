import React from "react";

import PokemonSection from "./PokemonSection";
import StatsSection from "./StatsSection";


import "./PokemonEditWindow.css";
import DetailsSection from "./DetailsSection";
import MovesSection from "./MovesSection";



function PokemonEditWindow(props) {
    return (
        <div id="pokemonEditWindow">
            <PokemonSection 
                activePokemon={props.activePokemon}
                setActiveField={props.setActiveField}
                teamEdit={props.teamEdit}
                setTeamEdit={props.setTeamEdit}
                data={props.data}
            />
            <div id="detailsMovesSection">
                <DetailsSection
                    activePokemon={props.activePokemon}
                    setActiveField={props.setActiveField}
                    teamEdit={props.teamEdit}
                    setTeamEdit={props.setTeamEdit}
                    data={props.data}
                />

                <MovesSection
                    activePokemon={props.activePokemon}
                    setActiveField={props.setActiveField}
                    teamEdit={props.teamEdit}
                    setTeamEdit={props.setTeamEdit}
                    data={props.data}
                />
            </div>

            <StatsSection 
                activePokemon={props.activePokemon}
                setActiveField={props.setActiveField}
                teamEdit={props.teamEdit}
                setTeamEdit={props.setTeamEdit}
                data={props.data}
            />
        </div>
    )
}

export default PokemonEditWindow;
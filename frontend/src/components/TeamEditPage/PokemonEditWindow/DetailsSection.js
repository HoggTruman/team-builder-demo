import React, { useEffect } from "react";
import { ABILITY_FIELD, ITEM_FIELD } from "./constants/fieldNames";
import { clean } from "../../../utility/cleanString";

import "./DetailsSection.css";




function DetailsSection(props) {
    function handleClickItemInput() {
        props.setActiveField(ITEM_FIELD);
    }

    function handleClickAbilityInput() {
        props.setActiveField(ABILITY_FIELD);
    }

    function handleChangeLevel(e) {
        let level = e.target.value;

        if (e.target.value !== "") {
            level = Math.max(e.target.min, Math.min(e.target.value, e.target.max));
        }

        props.setTeamEdit(team => {
            props.activePokemon.level = level;
            return {...team};
        });
    }

    function handleChangeGender(e) {
        props.setTeamEdit(team => {
            props.activePokemon.genderId = e.target.value;
            return {...team};
        });
    }

    function handleChangeShiny(e) {
        props.setTeamEdit(team => {
            props.activePokemon.shiny = e.target.value === "true";
            return {...team};
        });
    }

    function handleChangeTeraType(e) {
        props.setTeamEdit(team => {
            props.activePokemon.teraPkmnTypeId = e.target.value;
            return {...team};
        });
    }

    function handleChangeItemInput (e) {
        props.setTeamEdit(team => {
            props.activePokemon.itemName = e.target.value;
            return {...team};
        });
    }

    function handleChangeAbilityInput (e) {
        props.setTeamEdit(team => {
            props.activePokemon.abilityName = e.target.value;
            return {...team};
        });
    }



    // Render
    const activePokemonData = props.data.pokemon.find(
        x => x.identifier == clean(props.activePokemon.pokemonName)  // breaks for pokemon with form identifier
    );
    
    let genderOptions = activePokemonData?.genders.map(genderId => {
        const gender = props.data.genders.find(x => x.id == genderId);

        return (
            <option 
                key={gender.identifier}
                value={gender.id}
            >
                {gender.identifier}
            </option>
        )
    });

    function genderSelectValue() {
        const newGenderId = activePokemonData?.genders.includes(Number(props.activePokemon.genderId))?
            props.activePokemon.genderId:
            "4" 

        return newGenderId;
    }

    let teraTypeOptions = props.data.types
        .filter(x => x.id < 10000)
        .sort((a, b) => a.identifier > b.identifier? 1 : 0)
        .map(type => (
        <option 
            key={type.identifier}
            value={type.id}
        >
            {type.identifier}
        </option>
    ))

    useEffect(() => {
        // Trigger gender change when pokemon name is changed
        const genderSelectElement = document.getElementById("genderSelect");
        const fakeEvent = {target: { value: genderSelectElement.value}};

        handleChangeGender(fakeEvent);

        }, [props.activePokemon.pokemonName])



    return (
        <div id="detailsSection">
            <div className="row">
                <div className="field">
                    <label htmlFor="levelInput" className="side">Level</label>
                    <input 
                        id="levelInput" 
                        type="number" 
                        name="level"
                        min={1}
                        max={100}
                        value={props.activePokemon.level}
                        onChange={e => handleChangeLevel(e)}
                    />
                </div>

                <div className="field">
                    <label htmlFor="genderSelect" className="side">Gender</label>
                    <select 
                        id="genderSelect" 
                        name="gender" 
                        value= {genderSelectValue()}
                        onChange={e => handleChangeGender(e)}
                    > 
                        <option 
                            key="4"
                            value="4"
                        >
                            auto
                        </option>
                        {genderOptions}
                    </select>
                </div>
            </div>


            <div className="row">
                <div className="field">
                    <label htmlFor="shinySelect" className="side">Shiny</label>
                    <select 
                        id="shinySelect" 
                        name="shiny"
                        value={props.activePokemon.shiny}
                        onChange={e => handleChangeShiny(e)}
                    >
                        <option value="false">No</option>
                        <option value="true">Yes</option>
                    </select>
                </div>

                <div className="field">
                    <label htmlFor="teraTypeSelect" className="side">Tera Type</label>
                    <select 
                        id="teraTypeSelect" 
                        name="teraType"
                        value={props.activePokemon.teraPkmnTypeId}
                        onChange={e => handleChangeTeraType(e)}
                    >
                        {teraTypeOptions}
                    </select>
                </div>
            </div>


            <div className="row">
                <div className="field">
                    <label htmlFor="itemInput" className="above">Item</label>
                    <input 
                        id="itemInput" 
                        type="text" 
                        name="item"
                        onClick={handleClickItemInput}
                        onChange={e => handleChangeItemInput(e)}
                        value={props.activePokemon.itemName}
                    />
                </div>

                <div className="field">
                    <label htmlFor="abilityInput" className="above">Ability</label>
                    <input 
                        id="abilityInput" 
                        type="text" 
                        name="ability"
                        onClick={handleClickAbilityInput}
                        onChange={handleChangeAbilityInput}
                        value={props.activePokemon.abilityName}
                    />
                </div>
            </div>
        </div>
    );
}

export default DetailsSection
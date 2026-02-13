import React from "react";
import ItemOptionsRow from "./ItemOptionsRow";
import { FixedSizeList } from "react-window";

import "./OptionsTable.css";
import "./ItemOptions.css";

function ItemOptions(props) {
    function handleClickOptionRow(identifier) {
        props.setTeamEdit(team => {
            props.activePokemon.itemName = identifier;
            return {...team};
        })
    }



    // Render
    let itemOptionsRows;

    if (props.itemList.length === 0) {
        itemOptionsRows = <div className="noMatches">No Items Found</div>
    }
    else {
        itemOptionsRows = (
            <FixedSizeList
                height={390}
                width={1100}
                itemSize={50}
                itemCount={props.itemList.length}
                itemData={{items: props.itemList, handleClick: handleClickOptionRow}}
                style={{overflowY: "scroll"}}
            >
                {ItemOptionsRow}
            </FixedSizeList>
        );
    }


    // Render
    return (
        <div id="itemOptionsTable" className="optionsTable">
            <div className="row item header">
                <div className="col icon"></div>
                <div className="col name">Item</div>
                <div className="col effect">Effect</div>
            </div>
            { itemOptionsRows }
        </div>
    );
}

export default ItemOptions;
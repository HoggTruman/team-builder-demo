import React from "react";
import { itemIcons } from "../../../assets/assets";

function ItemOptionsRow(props) {
    const { index, style } = props;
    const item = props.data.items[index];

    return (
        <button 
            className="row item"
            style={style}
            onClick={() => props.data.handleClick(item.identifier)}
        >
            <div className="col icon">
                <img
                    src={itemIcons[item.identifier]}
                    alt="icon"
                    className="itemIcon"
                    loading="lazy"
                />
            </div>
            <div className="col name">
                {item.identifier}
            </div>
            <div className="col effect">
                {item.effect}
            </div>
        </button>
    )
}

export default ItemOptionsRow;
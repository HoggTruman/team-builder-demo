import "./TeamNameBar.css";

function TeamNameBar(props) {
    function handleChangeTeamNameInput(e) {
        props.setTeamEdit(team => {
            team.teamName = e.target.value;
            return {...team}
        })
    }

    return (
        <div id="teamNameBar">
            <label htmlFor="teamNameInput">Team Name: </label>
            <input
                id="teamNameInput"
                type="text"
                maxLength={40}
                value={props.teamEdit.teamName}
                onChange={e => handleChangeTeamNameInput(e)}
            />
        </div>
    )
}


export default TeamNameBar;
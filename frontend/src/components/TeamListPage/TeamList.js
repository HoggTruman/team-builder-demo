import React from "react";
import TeamInList from "./TeamInList";


function TeamList(props) {


    // Render
    let teamRender = props.teams.map(team => (
        <TeamInList 
            key={team.id}
            team={team}
            setTeams={props.setTeams}
            setPage={props.setPage}
            setActiveTeamId={props.setActiveTeamId}
            data={props.data}
        />
    ));

    return (
        <div id="teamList">
            {teamRender}
        </div>
    );
}



export default TeamList;
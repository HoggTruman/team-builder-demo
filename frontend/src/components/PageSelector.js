import React from "react";
import TeamListPage from "../pages/TeamListPage";
import TeamEditPage from "../pages/TeamEditPage";
import { ACCOUNT_PAGE, TEAM_EDIT_PAGE, TEAM_LIST_PAGE } from "../pages/constants/pageNames";
import AccountPage from "../pages/AccountPage";
import { userContext } from "../context/userContext";

function PageSelector(props) {
    const { isLoggedIn } = userContext();

    // Render
    if (props.page == TEAM_LIST_PAGE) {
        return (
            <TeamListPage 
                setPage={props.setPage}
                serverTeams={props.serverTeams}
                setServerTeams={props.setServerTeams}
                localTeams={props.localTeams}
                setLocalTeams={props.setLocalTeams}
                setActiveTeamId={props.setActiveTeamId}
                data={props.data}
            />
        );
    }
    else if (props.page == TEAM_EDIT_PAGE) {
        let activeTeam;
        if (props.activeTeamId > 0) {
            activeTeam = props.serverTeams.find(x => x.id == props.activeTeamId);
        }
        else if (props.activeTeamId < 0) {
            activeTeam = props.localTeams.find(x => x.id == props.activeTeamId)
        }


        return (
            <TeamEditPage
                setPage={props.setPage}
                team={activeTeam}
                setTeams={props.activeTeamId > 0? props.setServerTeams: props.setLocalTeams}
                activeTeamId={props.activeTeamId}
                data={props.data}
            />
        );
    }
    else if (props.page == ACCOUNT_PAGE && isLoggedIn() === false) {
        return (
            <AccountPage
                setPage={props.setPage}
                setServerTeams={props.setServerTeams}
            />
        )
    }
}

export default PageSelector;
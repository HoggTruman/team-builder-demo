// Local teams use negative Ids to differentiate themselves from server teams.
// This function returns the next negative integer from the current team ids

export function generateLocalTeamId(teams) {
    const localTeams = teams.filter(team => team.id < 0);
    if (localTeams.length === 0) {
        return -1;
    }
    else {
        return Math.min(...localTeams.map(team => team.id)) - 1;
    }
}
const KEY = "teams";


export function setLocalStorageTeams(teams) {
    const stringTeams = JSON.stringify(teams);
    localStorage.setItem(KEY, stringTeams);
}

export function getLocalStorageTeams() {
    const stringTeams = localStorage.getItem(KEY);
    return JSON.parse(stringTeams);
}

export function clearLocalStorageTeams() {
    localStorage.removeItem(KEY);
}
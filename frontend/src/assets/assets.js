// Image Caches
export const damageCategoryImages = {};
export const itemIcons = {};
export const pokemonIcons = {};
export const pokemonNormalImages = {};
export const pokemonShinyImages = {};
export const typeImages = {};




// Helpers
function getIdentifier(key) {
    return key.match(/^\.\/(.*)\.png$/)[1];
}

function importAll(r, cache) {
    r.keys().forEach((key) => {
        cache[getIdentifier(key)] = r(key)
    });
}




// Import
importAll(require.context('./damagecategory', false, /\.png$/), damageCategoryImages);
importAll(require.context('./items', false, /\.png$/), itemIcons);
importAll(require.context('./pokemon/icons/', false, /\.png$/), pokemonIcons);
importAll(require.context('./pokemon/normal/', false, /\.png$/), pokemonNormalImages);
importAll(require.context('./pokemon/shiny/', false, /\.png$/), pokemonShinyImages);
importAll(require.context('./types', false, /\.png$/), typeImages);
function solve(data) {
    'use strict';

    let heroes = [];

    for (let heroInfo of data) {
        let [heroName, heroLevel, itemsStr] = heroInfo.split(' / ');

        heroLevel = parseInt(heroLevel);

        let items = itemsStr.split(', ');

        let hero = {
            name: heroName,
            level: heroLevel,
            items: items
        };

        heroes.push(hero);
    }

    heroes.sort((a, b) => a.level - b.level);

    for (let hero of heroes) {
        console.log(`Hero: ${hero.name}`);
        console.log(`level => ${hero.level}`);
        console.log(`items => ${hero.items.join(', ')}`);
    }
}

solve([
    'Isacc / 25 / Apple, GravityGun',
    'Derek / 12 / BarrelVest, DestructionSword',
    'Hes / 1 / Desolator, Sentinel, Antara'
    ]);

solve ([
    'Batman / 2 / Banana, Gun',
    'Superman / 18 / Sword',
    'Poppy / 28 / Sentinel, Antara'
    ]);
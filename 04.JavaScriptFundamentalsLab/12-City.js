function solve(personObj) {
    'use strict';

    let prsnProperties = {};

    for (let key in personObj) {
        console.log(key + " -> " + personObj[key]);
    }
}

solve({
    name: "Sofia",
    area: 492,
    population: 1238438,
    country: "Bulgaria",
    postCode: "1000"
}
);
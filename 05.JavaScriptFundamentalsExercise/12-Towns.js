function solve(inputArray) {
    'use strict';

    let townDetails = [];

    for (const rawData of inputArray) {
        const splitData = rawData.split(' | ')

        townDetails.push({
            town: splitData[0],
            latitude: parseFloat(splitData[1]).toFixed(2),
            longitude: parseFloat(splitData[2]).toFixed(2)
        })
    }

    townDetails.forEach((town) => console.log(town));
}

solve(['Sofia | 42.696552 | 23.32601',
    'Beijing | 39.913818 | 116.363625']);
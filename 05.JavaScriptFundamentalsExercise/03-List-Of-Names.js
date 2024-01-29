function solve(arrayInput) {
    'use strict';

    arrayInput.sort((a, b) => a.localeCompare(b));

    for (let i = 0; i < arrayInput.length; i += 1) {
        console.log(`${i + 1}.${arrayInput[i]}`);
    }

}

solve(["John", "Bob", "Christina", "Ema"]);

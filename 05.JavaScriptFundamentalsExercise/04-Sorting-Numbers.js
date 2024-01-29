function solve(inputArray) {
    'use strict';

    inputArray.sort((a, b) => a - b);

    let convertedArray = [];

    while (inputArray.length > 0) {
        const firstElement = inputArray.shift();
        const lastElement = inputArray.pop();

        convertedArray.push(firstElement);

        if (lastElement !== undefined) {
            convertedArray.push(lastElement);
        }
    }

    return convertedArray;
}

console.log(solve([1, 65, 3, 52, 48, 63, 31, -3, 18, 56]));
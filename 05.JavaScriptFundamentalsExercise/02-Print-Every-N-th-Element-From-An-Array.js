function solve(arrayInput, stepNum) {
    'use strict';

    let outputArray = [];

    for (let i = 0; i < arrayInput.length; i += stepNum) {
        outputArray.push(arrayInput[i]);
    }
    return outputArray;
}

console.log(solve(['5','20','31','4','20'],2));
console.log(solve(['dsa','asd','test','tset'], 2));
console.log(solve(['1', '2','3','4','5'], 6));

function solve(inputArray) {
    'use strict';

    let firstElement = inputArray[0];
    let lastElement = inputArray[inputArray.length - 1];
    let result = firstElement + lastElement;

    console.log(result);

}

solve([1, 2, 3, 4, 5]);
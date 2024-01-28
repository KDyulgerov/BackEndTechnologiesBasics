function solve(inputArray) {
    'use strict';

    let evenSum = 0;
    let oddSum = 0;

    for (let i = 0; i < inputArray.length; i += 1) {
        inputArray[i] = Number(inputArray[i]);

        if (inputArray[i] % 2 === 0) {
            evenSum += inputArray[i];
        } else {
            oddSum += inputArray[i];
        }
    }
    console.log(evenSum - oddSum);
}

solve([1, 2, 3, 4, 5, 6]);
solve([3, 5, 7, 9]);
solve([2, 4, 6, 8, 10]);
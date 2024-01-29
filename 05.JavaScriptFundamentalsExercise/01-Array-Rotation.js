function solve(arrayInput, rotationTimes) {
    'use strict';

    for (let i = 1; i <= rotationTimes; i += 1) {
        let firstNum = arrayInput.shift();
        arrayInput.push(firstNum);
    }

    console.log(arrayInput.join(' '));
}

solve([51, 47, 32, 61, 21], 2);
solve([32, 21, 61, 1], 4);
solve([2, 4, 15, 31], 5);
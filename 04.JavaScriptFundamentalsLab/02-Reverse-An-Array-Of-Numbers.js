function solve(n, inputArray) {
    'use strict';

    let slicedArray = inputArray.slice(0, n);
    let reversedArray = slicedArray.reverse();

    console.log(reversedArray.join(' '));
}

solve(3, [10, 20, 30, 40, 50]);
solve(4, [-1, 20, 99, 5]);
solve(2, [66, 43, 75, 89, 47]);
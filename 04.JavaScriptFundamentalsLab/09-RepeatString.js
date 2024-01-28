function solve(input, repeatCount) {
    'use strict';

    let repeatedInput = input.repeat(repeatCount);

    return repeatedInput;
}

console.log(solve('abc', 3));
console.log(solve('String', 2));
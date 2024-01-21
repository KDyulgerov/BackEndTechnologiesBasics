function solve(firstNum, secondNum, operation) {
    'use strict'

    let result = 0;

    if (operation === '+') {
        result = firstNum + secondNum;
    } else if (operation === '-') {
        result = firstNum - secondNum;
    } else if (operation === '*') {
        result = firstNum * secondNum;
    } else if (operation === '%') {
        result = firstNum % secondNum;
    } else if (operation === '/') {
        result = firstNum / secondNum;
    } else if (operation === '**') {
        result = firstNum ** secondNum;
    }

    console.log(result);
}

solve(1, 2, "+");
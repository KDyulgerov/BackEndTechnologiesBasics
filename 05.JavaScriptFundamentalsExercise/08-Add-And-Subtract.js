function solve(firstNum, secondNum, thirdNum) {
    'use strict';

    function sum(firstNumber, secondNumber) {
        return firstNumber + secondNumber;
    }

    function substract(firstNumber, secondNumber, thirdNumber) {
        return sum(firstNumber, secondNumber) - thirdNumber;
    }

    console.log(substract(firstNum, secondNum, thirdNum));
}

solve(23, 6, 10);
solve(1, 17, 30);
solve(42, 58, 100);
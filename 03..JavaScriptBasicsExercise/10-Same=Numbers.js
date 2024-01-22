function solve(number) {
    'use strict';

    let totalSum = 0;
    const firstDigit = number % 10; 
    let allEqual = true;

    while(number > 0) {
        const currentDigit = number % 10;

        if (firstDigit !== currentDigit) {
            allEqual = false;
        }
        totalSum += number % 10;
        number = Math.floor(number/ 10);
    }
    console.log(allEqual);
    console.log(totalSum);
}

solve(12345);

solve(22222);
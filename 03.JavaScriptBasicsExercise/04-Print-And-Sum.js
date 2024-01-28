function solve(startNum, endNum) {
    'use strict';

    let numbers = '';
    let totalSum = 0;

    for (let i = startNum; i <= endNum; i += 1) {
        numbers += `${i} `;

        totalSum += i;
    }

    console.log(numbers.trimEnd());
    console.log(`Sum: ${totalSum}`);
}

solve(5, 10);

solve(0, 26);

solve(50, 60);

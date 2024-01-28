function solve(number) { 
    'use strict';

    for(let i = 1; i <= 10; i += 1) {
        let result = number * i;

        console.log(`${number} X ${i} = ${result}`);
    }
}

solve(5);
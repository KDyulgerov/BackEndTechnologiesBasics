function solve(stringInput, startIndex, countNum) {
    'use strict';

    let result = stringInput.substring(startIndex, startIndex + countNum);

    console.log(result);
}

solve('ASentence', 1, 8);
solve('SkipWord', 4, 7);
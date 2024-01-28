function solve(stringInput, searchedWord) {
    'use strict';

    let words = stringInput.split(' ');
    let counterOcc = 0;

    for (const word of words) {
        if (word === searchedWord) {
            counterOcc += 1;
        }
    }

    console.log(counterOcc);
}

solve('This is a word and it also is a sentence','is');
solve('softuni is great place for learning new programming languages','softuni');
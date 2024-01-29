function solve(word, text) {
    'use strict';

   const hasWord = text
        .toLowerCase()
        .split(' ')
        .includes(word);

    if (hasWord) {
        console.log(word);
    } else {
        console.log(`${word} not found!`)
    }
}

solve('javascript','JavaScript is the best programming language');
solve('python','JavaScript is the best programming language');
function solve(textInput, wordToReplace) {
    'use strict';

    let censored = textInput.replace(wordToReplace, repeat(wordToReplace));

    function repeat(wordToRepeat) {
        let repeatedWord = '*'.repeat(wordToRepeat.length);

        return repeatedWord;
    }

    while (censored.includes(wordToReplace)) {
        censored = censored.replace(wordToReplace, repeat(wordToReplace));
    }

    console.log(censored);
}

solve('A small sentence with some words', 'small');
solve('Find the hidden word', 'hidden');
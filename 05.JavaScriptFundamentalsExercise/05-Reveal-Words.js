function solve(words, template) {
    'use strict';

    const separatedWords = words.split(', ');

    for (const separatedWord of separatedWords) {
        template = template.replace('*'.repeat(separatedWord.length), separatedWord);
    }

    console.log(template);
}

solve('great','softuni is ***** place for learning new programming languages');
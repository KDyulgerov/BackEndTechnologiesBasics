function solve(inputArray) {
    'use strict';

    let persons = [];

    for (const credentials of inputArray) {
        let person = {
            name: credentials,
            personalNumber: credentials.length
        }
        persons.push(person);
    }

    persons.forEach((person) => console.log(`Name: ${person.name} -- Personal Number: ${person.personalNumber}`));
   
}

solve([
    'Silas Butler',
    'Adnaan Buckley',
    'Juan Peterson',
    'Brendan Villarreal'
    ]);

solve([
    'Samuel Jackson',
    'Will Smith',
    'Bruce Willis',
    'Tom Holland'
    ]
    );
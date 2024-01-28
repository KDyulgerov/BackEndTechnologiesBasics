function solve(firstName, lastName, age) {
    'use strict';
    let person = {
        firstName: firstName,
        lastName: lastName,
        age: age
    }

    return person;
}

console.log(solve('Peter', 'Pan', 20));
console.log(solve('George', 'Smith', 18));
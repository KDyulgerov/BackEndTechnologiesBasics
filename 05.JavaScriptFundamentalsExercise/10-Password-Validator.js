function solve(password) {
    'use strict';

    let isValid = true;

    function isValidPassword(password) {
        const regex = /^[A-Za-z0-9]+$/;
        return regex.test(password);
    }

    function containsAtLeastTwoDigits(password) {
        const regex = /(?=(.*\d){2})/;
        return regex.test(password);
    }

    if(password.length < 6 || password.length > 10) {
        console.log('Password must be between 6 and 10 characters');
        isValid = false;
    }
    if (isValidPassword(password) === false) {
        console.log('Password must consist only of letters and digits');
        isValid = false;
    }
    if (containsAtLeastTwoDigits(password) === false) {
        console.log('Password must have at least 2 digits')
        isValid = false;
    }
    if (isValid) {
        console.log('Password is valid');
    }
}

solve('logIn');
solve('MyPass123');
solve('Pa$s$s');
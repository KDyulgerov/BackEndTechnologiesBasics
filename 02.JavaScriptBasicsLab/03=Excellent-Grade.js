function solve(grade) {
    'use strict';

    let status;

    if (grade >= 5.50) {

        status = 'Excellent';

    } else {

        status = 'Not excellent';

    }

    console.log(status);
}

solve(5.50);
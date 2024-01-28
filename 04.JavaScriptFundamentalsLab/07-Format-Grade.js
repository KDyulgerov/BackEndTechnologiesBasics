function solve(grade) {
    'use strict';

    let gradeStatus = '';

    if (grade >= 3.00 && grade < 3.50) {
        gradeStatus = 'Poor';
    } else if (grade >= 3.50 && grade < 4.50) {
        gradeStatus = 'Good';
    } else if (grade >= 4.50 && grade < 5.50) {
        gradeStatus = 'Very good';
    } else if (grade >= 5.50) {
        gradeStatus = 'Excellent';
    }

    if (grade < 3.00) {
        console.log('Fail (2)');
    } else {
        console.log(`${gradeStatus} (${grade.toFixed(2)})`);
    }
}

solve(3.33);
solve(4.50);
solve(2.99);
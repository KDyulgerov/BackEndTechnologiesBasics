function solve(speed, area) {
    'use strict';

    let maxSpeed = 0;

    if (area === 'motorway') {
        maxSpeed = 130;
    } else if (area === 'interstate') {
        maxSpeed = 90;
    } else if (area === 'city') {
        maxSpeed = 50;
    } else if (area === 'residential') {
        maxSpeed = 20;
    }

    const diff = speed - maxSpeed;

    if (diff <= 0) {
        console.log(`Driving ${speed} km/h in a ${maxSpeed} zone`);
    } else {
        let status = '';
        
        if (diff > 0 && diff <= 20) {
            status = 'speeding'
        } else if (diff > 20 && diff <= 40) {
            status = 'excessive speeding'
        } else {
            status = 'reckless driving'
        }
        
        console.log(`The speed is ${diff} km/h faster than the allowed speed of ${maxSpeed} - ${status}`)
    }
}

solve(40, 'city');

solve(21, 'residential');

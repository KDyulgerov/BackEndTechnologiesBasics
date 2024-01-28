function solve(peopleCount, type, day) {
    'use strict'

    let priceForPerson = 0;

    if (day === 'Friday') {
        if (type === 'Students') {
            priceForPerson = 8.45;
        } else if (type === 'Business') {
            priceForPerson = 10.90;
        } else if (type === 'Regular') {
            priceForPerson = 15;
        }

    } else if (day === 'Saturday') {
        if (type === 'Students') {
            priceForPerson = 9.80;
        } else if (type === 'Business') {
            priceForPerson = 15.60;
        } else if (type === 'Regular') {
            priceForPerson = 20;
        }
    } else if (day === 'Sunday') {
        if (type === 'Students') {
            priceForPerson = 10.46;
        } else if (type === 'Business') {
            priceForPerson = 16;
        } else if (type === 'Regular') {
            priceForPerson = 22.50;
        }
    }

    if (type === 'Business' && peopleCount >= 100) {
        peopleCount -= 10;
    }
    let totalPrice = peopleCount * priceForPerson;

    if (type === 'Students' && peopleCount >= 30) {
        totalPrice *= 0.85;
    }
    if (type === 'Regular' && (peopleCount >= 10 && peopleCount <= 20)) {
        totalPrice *= 0.95;
    }

    console.log(`Total price: ${totalPrice.toFixed(2)}`);
}

solve(30, "Students", "Sunday");

solve(40, "Regular", "Saturday");
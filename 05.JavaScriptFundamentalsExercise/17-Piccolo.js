function solve(input) {
    'use strict';

    const parkingLot = new Set();

    for (const entry of input) {
        const [direction, carNumber] = entry.split(', ');

        if (direction === 'IN') {
            parkingLot.add(carNumber);
        } else if (direction === 'OUT') {
            parkingLot.delete(carNumber);
        }
    }

    if (parkingLot.size === 0) {
        console.log('Parking Lot is Empty');
    } else {
        const sortedCarNumbers = Array.from(parkingLot).sort();
        console.log(sortedCarNumbers.join('\n'));
    }
}

solve([
    'IN, CA2844AA',
    'IN, CA1234TA',
    'OUT, CA2844AA',
    'IN, CA9999TT',
    'IN, CA2866HI',
    'OUT, CA1234TA',
    'IN, CA2844AA',
    'OUT, CA2866HI',
    'IN, CA9876HH',
    'IN, CA2822UU'
]);

solve([
    'IN, CA2844AA',
    'IN, CA1234TA',
    'OUT, CA2844AA',
    'OUT, CA1234TA'
]);
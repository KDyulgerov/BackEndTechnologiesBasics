function solve(currentStock, orderedStock) {
    'use strict';

    const storeStock = {};

    for (let i = 0; i < currentStock.length; i += 2) {
        const stockName = currentStock[i];
        const stockAmount = parseInt(currentStock[i + 1], 10);

        storeStock[stockName] = stockAmount;
    }

    for (let i = 0; i < orderedStock.length; i += 2) {
        const stockName = orderedStock[i];
        const stockAmount = parseInt(orderedStock[i + 1], 10);

        if (storeStock[stockName]) {
            storeStock[stockName] += stockAmount;
        } else {
            storeStock[stockName] = stockAmount;
        }
    }

    Object.keys(storeStock).forEach((currentItem) => console.log(`${currentItem} -> ${storeStock[currentItem]}`))
}

solve([
    'Chips', '5', 'CocaCola', '9', 'Bananas', '14', 'Pasta', '4', 'Beer', '2'
    ], 
    [
    'Flour', '44', 'Oil', '12', 'Pasta', '7', 'Tomatoes', '70', 'Bananas', '30'
    ]);

solve([
    'Salt', '2', 'Fanta', '4', 'Apple', '14', 'Water', '4', 'Juice', '5'
    ],
    [
    'Sugar', '44', 'Oil', '12', 'Apple', '7', 'Tomatoes', '7', 'Bananas', '30'
    ]);
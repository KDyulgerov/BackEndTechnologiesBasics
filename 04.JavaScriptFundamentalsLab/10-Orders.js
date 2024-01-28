function solve(product, count) {
    'use strict';

    let priceProduct = 0;

    if (product === 'coffee') {
        priceProduct = 1.50;
    } else if (product === 'water') {
        priceProduct = 1.00;
    } else if (product === 'coke') {
        priceProduct = 1.40;
    } else if (product === 'snacks') {
        priceProduct = 2.00;
    }

    console.log((priceProduct * count).toFixed(2));
}

solve("water", 5);
solve("coffee", 2);
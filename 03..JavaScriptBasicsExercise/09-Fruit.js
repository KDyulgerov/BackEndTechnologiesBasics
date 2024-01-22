function solve(typeFruit, grams, pricePerKg) {
    'use strict';

    const pricePerGram = pricePerKg / 1000;
    const totalPrice = grams * pricePerGram;

    console.log(`I need $${totalPrice.toFixed(2)} to buy ${(grams / 1000).toFixed(2)} kilograms ${typeFruit}.`);
}

solve('orange', 2500, 1.80);
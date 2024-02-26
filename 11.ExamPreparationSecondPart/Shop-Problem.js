function shop(products) {
    'use strict';

    const numberOfProducts = parseInt(products[0]);

    const productsList = products.slice(1, numberOfProducts + 1);
    const allComands = products.slice(numberOfProducts + 1)

    for (let index = 0; index < allComands.length; index += 1) {
        const rawCommand = allComands[index].split(' ');
        const commandName = rawCommand[0];

        if (commandName === 'Sell') {
            const removedProduct = productsList.shift();
            console.log(`${removedProduct} product sold!`)
        } else if (commandName === 'Add') {
            const productToAdd = allComands[index].slice(4);
            productsList.push(productToAdd);
        } else if (commandName === 'Swap') {
            const firstIndex = parseInt(rawCommand[1], 10);
            const secondIndex = parseInt(rawCommand[2], 10);

            if(firstIndex < 0 || firstIndex >= productsList.length) {
                continue;
            }
            if(secondIndex < 0 || secondIndex >= productsList.length) {
                continue;
            }

            const startIndexProduct = productsList[firstIndex];
            productsList[firstIndex] = productsList[secondIndex];
            productsList[secondIndex] = startIndexProduct;

            console.log('Swapped!');

        } else if (commandName === 'End') {
            break;
        }
    }

    if (productsList.length) {
        console.log(`Products left: ${productsList.join(', ')}`);
    } else {
        console.log('The shop is empty')
    }
}

shop(['3', 'Apple', 'Banana', 'Orange', 'Sell', 'End', 'Swap 0 1']);

/* shop(['5', 'Milk', 'Eggs', 'Bread', 'Cheese', 'Butter', 'Add Yogurt', 'Swap 1 4', 'End']);
shop(['3', 'Shampoo', 'Soap', 'Toothpaste', 'Sell', 'Sell', 'Sell', 'End']); */

function solve(inputArray) {
    'use strict';

    let uniqueContacts = {};

    inputArray.forEach(element => {
        let keyValuePair = element.split(' ');
        let name = keyValuePair[0];
        let phoneNum = keyValuePair[1];
        uniqueContacts[name] = phoneNum;
    });

    for (let key in uniqueContacts) {
        console.log(`${key} -> ${uniqueContacts[key]}`)
    }
}

solve(['Tim 0834212554',
    'Peter 0877547887',
    'Bill 0896543112',
    'Tim 0876566344']
);

solve(['George 0552554',
    'Peter 087587',
    'George 0453112',
    'Bill 0845344']
);


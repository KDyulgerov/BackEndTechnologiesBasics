function solve(rawNumber, firstOpr, secondOpr, thirdOpr, fourthOpr, fifthOpr) {
    'use strict';

    function performOpr(currentNumber, currentOpr) {
        if (currentOpr === `chop`) {
            return currentNumber /= 2;
        } else if (currentOpr === `dice`) {
            return currentNumber = Math.sqrt(currentNumber);
        } else if (currentOpr === `spice`) {
            return currentNumber += 1;
        } else if (currentOpr === `bake`) {
            return currentNumber *= 3;
        } else if (currentOpr === `fillet`) {
            return currentNumber *= 0.80;
        } else {
            return currentNumber;
        }
    }

    let number = parseInt(rawNumber, 10);

    number = performOpr(number, firstOpr);
    console.log(number);

    number = performOpr(number, secondOpr);
    console.log(number);

    number = performOpr(number, thirdOpr);
    console.log(number);

    number = performOpr(number, fourthOpr);
    console.log(number);

    number = performOpr(number, fifthOpr);
    console.log(number);
}

solve('32', 'chop', 'chop', 'chop', 'chop', 'chop');

solve('9', 'dice', 'spice', 'chop', 'bake', 'fillet');


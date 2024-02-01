import { isOddOrEven } from './01-Even-Or-Odd.js';
import { expect } from 'chai';

describe('isOddOrEven', () => {
    it('Should return even once empty string is passed', () => {
        const stringInput = '';
        const expectedResult = 'even';

        const result = isOddOrEven(stringInput);

        expect(result).to.be.equals(expectedResult);
    })
    it('Should return even once even string is passed', () => {
        const stringInput = 'abcd';
        const expectedResult = 'even';

        const result = isOddOrEven(stringInput);

        expect(result).to.be.equals(expectedResult);
    })
    it('Should return odd once odd string is passed', () => {
        const stringInput = 'abc';
        const expectedResult = 'odd';

        const result = isOddOrEven(stringInput);

        expect(result).to.be.equals(expectedResult);
    })
    it('Should return odd once odd string with spaces is passed', () => {
        const stringInput = 'a b c';
        const expectedResult = 'odd';

        const result = isOddOrEven(stringInput);

        expect(result).to.be.equals(expectedResult);
    })
    it('Should return even once even string with spaces is passed', () => {
        const stringInput = 'a b c d e ';
        const expectedResult = 'even';

        const result = isOddOrEven(stringInput);

        expect(result).to.be.equals(expectedResult);
    })
    it('Should return undefined once non-string is passed', () => {
        const numberInput = 123;
        const notANumber = NaN;
        const undefinedInput = undefined;

        const resultNum = isOddOrEven(numberInput);
        const resultNotNum = isOddOrEven(notANumber);
        const resultUndf = isOddOrEven(undefinedInput);

        expect(resultNum).to.be.undefined;
        expect(resultNotNum).to.be.undefined;
        expect(resultUndf).to.be.undefined;
    })
})
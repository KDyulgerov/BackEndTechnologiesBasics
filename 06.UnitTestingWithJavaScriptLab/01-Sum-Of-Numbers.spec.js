import { sum } from './01-Sum-Of-Numbers.js';
import { expect } from 'chai';

describe('sum', () => {
    it('Should retun 0 if and empty array is given', () => {
        const arrayInput = [];

        const result = sum(arrayInput);

        expect(result).to.be.equals(0);
    })
    it('Should return a single element when single element array is given', () => {
        const singleElemArray = [2];

        const result = sum(singleElemArray);

        expect(result).to.be.equals(2);
    })
    it('Should return the correct result, once positive elements array is given', () => {
        const positiveElemArray = [1, 2, 3];

        const result = sum(positiveElemArray);

        expect(result).to.be.equals(6);
    })
    it('Should return the correct result, once mixed elements array is given', () => {
        const mixedElemArray = [1, 2, -2, 3,];

        const result = sum(mixedElemArray);

        expect(result).to.be.equals(4);
    })
    it('Should return zero once only zero is given in the array input', () => {
        const zeroElemArray = [0];

        const result = sum(zeroElemArray);

        expect(result).to.be.equals(0);
    })
})
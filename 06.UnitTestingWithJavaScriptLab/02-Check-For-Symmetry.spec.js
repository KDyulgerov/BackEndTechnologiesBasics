import { isSymmetric } from './02-Check-For-Symmetry.js';
import { expect } from 'chai';

describe('isSymmetric', () => {
    it('Should return false once non-array input is given', () => {
        const stringInput = 'test';
        const numberInput = 123;
        const floatInput = 2.14213;
        const nanInput = NaN;

        const resultString = isSymmetric(stringInput);
        const resultNumber = isSymmetric(numberInput);
        const resultFloat = isSymmetric(floatInput);
        const resultNaN = isSymmetric(nanInput);

        expect(resultString).to.be.false;
        expect(resultNumber).to.be.false;
        expect(resultFloat).to.be.false;
        expect(resultNaN).to.be.false;
    })
    it('Should return false once non-symetric number array is given', () => {
        const nonSymetricNumberArray = [1, 2, 3, 1];

        const result = isSymmetric(nonSymetricNumberArray);

        expect(result).to.be.false;
    })
    it('Should return false once non-symetric string array is given', () => {
        const nonSymetricStringArray = ['1', '2', '1', '1'];

        const result = isSymmetric(nonSymetricStringArray);

        expect(result).to.be.false;
    })
    it('Should return false once non-symetric mixed array is given', () => {
        const nonSymetricMixedArray = ['1', '2', 2, '1'];

        const result = isSymmetric(nonSymetricMixedArray);

        expect(result).to.be.false;
    })

    it('Should return true once symetric string array is given', () => {
        const symetricStringArray = ['a', 'b', 'c', 'b', 'a'];

        const result = isSymmetric(symetricStringArray);

        expect(result).to.be.true;
    })

    it('Should return true once symetric number array is given', () => {
        const symetricNumberArray = [1, 2, 2, 1];

        const result = isSymmetric(symetricNumberArray);

        expect(result).to.be.true;
    })
    it('Should return true once symetric mixed array is given', () => {
        const symetricMixedArray = [1, 2, 'a', 'a', 2, 1];

        const result = isSymmetric(symetricMixedArray);

        expect(result).to.be.true;
    })
})
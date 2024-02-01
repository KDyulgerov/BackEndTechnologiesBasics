import { mathEnforcer } from './03-MathEnforcer.js';
import { expect } from 'chai';

describe('mathEnforcer', () => {
    describe('addFive', () => {
        it('Should return correct result once correct param is passed', () => {
            const input = 5;

            const result = mathEnforcer.addFive(input);

            expect(result).to.be.equals(10);
        })
        it('Should return correct result once correct param is passed', () => {
            const input = 5.5;

            const result = mathEnforcer.addFive(input);

            expect(result).to.be.equals(10.5);
        })
        it('Should return correct result once negative param is passed', () => {
            const input = -5;

            const result = mathEnforcer.addFive(input);

            expect(result).to.be.equals(0);
        })
        it('Should return correct result once zero is passed as input', () => {
            const input = 0;

            const result = mathEnforcer.addFive(input);

            expect(result).to.be.equals(5);
        })
        it('Should return correct result once zero is passed as input', () => {
            const input = -0;

            const result = mathEnforcer.addFive(input);

            expect(result).to.be.equals(5);
        })
        it('Should return NaN once NaN is passed as input', () => {
            const input = NaN;

            const result = mathEnforcer.addFive(input);

            expect(result).to.be.NaN;
        })
        it('Should return undefined once non-number is passed as input', () => {
            const inputString = 'abv';
            const inputNumbers = '123';
            const inputSpecialChars = '$%@';

            const resultString = mathEnforcer.addFive(inputString);
            const resultNumbers = mathEnforcer.addFive(inputNumbers);
            const resultSpecialChars = mathEnforcer.addFive(inputSpecialChars);

            expect(resultString).to.be.undefined;
            expect(resultNumbers).to.be.undefined;
            expect(resultSpecialChars).to.be.undefined;
        })
    })
    describe('subtractTen', () => {
        it('Should return correct result once correct param is passed', () => {
            const input = 15;

            const result = mathEnforcer.subtractTen(input);

            expect(result).to.be.equals(5);
        })
        it('Should return correct result once correct param is passed', () => {
            const input = 15.5;

            const result = mathEnforcer.subtractTen(input);

            expect(result).to.be.equals(5.5);
        })
        it('Should return correct result once negative param is passed', () => {
            const input = -10;

            const result = mathEnforcer.subtractTen(input);

            expect(result).to.be.equals(-20);
        })
        it('Should return correct result once zero is passed as input', () => {
            const input = 0;

            const result = mathEnforcer.subtractTen(input);

            expect(result).to.be.equals(-10);
        })
        it('Should return correct result once zero is passed as input', () => {
            const input = 10;

            const result = mathEnforcer.subtractTen(input);

            expect(result).to.be.equals(0);
        })
        it('Should return NaN once NaN is passed as input', () => {
            const input = NaN;

            const result = mathEnforcer.subtractTen(input);

            expect(result).to.be.NaN;
        })
        it('Should return undefined once non-number is passed as input', () => {
            const inputString = 'abv';
            const inputNumbers = '123';
            const inputSpecialChars = '$%@';

            const resultString = mathEnforcer.subtractTen(inputString);
            const resultNumbers = mathEnforcer.subtractTen(inputNumbers);
            const resultSpecialChars = mathEnforcer.subtractTen(inputSpecialChars);

            expect(resultString).to.be.undefined;
            expect(resultNumbers).to.be.undefined;
            expect(resultSpecialChars).to.be.undefined;
        })
    })
    describe('sum', () => {
        it('Should return correct result once both params are correct', () => {
            const firstParam = 1;
            const secondParam = 5;

            const result = mathEnforcer.sum(firstParam, secondParam);

            expect(result).to.be.equals(6);
        })
        it('Should return correct result once both params are correct', () => {
            const firstParam = -1;
            const secondParam = 5;

            const result = mathEnforcer.sum(firstParam, secondParam);

            expect(result).to.be.equals(4);
        })
        it('Should return correct result once both params are correct', () => {
            const firstParam = 1;
            const secondParam = -5;

            const result = mathEnforcer.sum(firstParam, secondParam);

            expect(result).to.be.equals(-4);
        })
        it('Should return zero once both params are zero', () => {
            const firstParam = 0;
            const secondParam = 0;

            const result = mathEnforcer.sum(firstParam, secondParam);

            expect(result).to.be.equals(0);
        })
        it('Should return correct result once both params are floating numbers', () => {
            const firstParam = 0.1;
            const secondParam = 0.1;

            const result = mathEnforcer.sum(firstParam, secondParam);

            expect(result).to.be.equals(0.2);
        })
        it('Should return undefined once first param is incorrect', () => {
            const firstParam = 'abv';
            const secondParam = 5;

            const result = mathEnforcer.sum(firstParam, secondParam);

            expect(result).to.be.undefined;
        })
        it('Should return undefined once second param is incorrect', () => {
            const firstParam = 5;
            const secondParam = 'abv';

            const result = mathEnforcer.sum(firstParam, secondParam);

            expect(result).to.be.undefined;
        })
    })
})
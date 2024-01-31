import { createCalculator } from './04-Add-Subtract.js';
import { expect } from 'chai';

describe('createCalculator', () => {
    it('Should be zero once no operations are executed', () => {
        const calculator = createCalculator();

        const result = calculator.get();

        expect(result).to.be.equals(0);
    })
    it('Should be zero once no invalid params are executed', () => {
        const calculator = createCalculator();

        calculator.add(NaN);
        calculator.add({name: "Name"});
        calculator.add('!@$');
        calculator.subtract({name: "Name"});
        calculator.subtract(NaN);
        calculator.subtract('!@$');
        const result = calculator.get();

        expect(result).to.be.NaN;
    })
    it('Should return negative number once only substract operations are executed', () => {
        const calculator = createCalculator();

        calculator.subtract(1);
        calculator.subtract(3);
        calculator.subtract(500);

        const result = calculator.get();

        expect(result).to.be.equals(-504);
    })
    it('Should return positive number once only add operations are executed', () => {
        const calculator = createCalculator();

        calculator.add(1);
        calculator.add(3);
        calculator.add(500);

        const result = calculator.get();

        expect(result).to.be.equals(504);
    })
    it('Should return correct result when mixed operations are executed', () => {
        const calculator = createCalculator();

        calculator.subtract(1);
        calculator.subtract(3);
        calculator.add(5);

        const result = calculator.get();

        expect(result).to.be.equals(1);
    })
    it('Should return correct result when mixed floating numbers are executed', () => {
        const calculator = createCalculator();

        calculator.subtract(1);
        calculator.add(3.2);
        calculator.add(5);

        const result = calculator.get();

        expect(result).to.be.equals(7.2);
    })
    it('Should be NaN once incorect input are executed', () => {
        const calculator = createCalculator();

        calculator.add('abc');
        calculator.subtract('bca');
        const result = calculator.get();

        expect(result).to.be.NaN;
    })

     it('Should be corect result once numbers are passed as a string', () => {
        const calculator = createCalculator();

        calculator.add('12');
        calculator.subtract('-10');
        calculator.add('100');
        const result = calculator.get();

        expect(result).to.be.equals(122);
    })
    it('Should be corect result once mixed operations are executed', () => {
        const calculator = createCalculator();

        calculator.add(1000);
        calculator.subtract(0);
        calculator.subtract(-10);
        calculator.add(0);
        calculator.add(-10);
        calculator.subtract(1000);
        const result = calculator.get();

        expect(result).to.be.equals(0);
    })
    it('Should return correct result when mixed operation are executed', () => {
        const calculator = createCalculator();

        calculator.subtract(100);
        calculator.add(1000);
        calculator.add(-5);
        calculator.subtract(-1);

        const result = calculator.get();

        expect(result).to.be.equals(896);
    })
})
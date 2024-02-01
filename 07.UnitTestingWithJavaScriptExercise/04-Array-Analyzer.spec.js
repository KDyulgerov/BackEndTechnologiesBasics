import { analyzeArray } from './04-Array-Analyzer.js';
import { expect } from 'chai';

describe('analyzeArray', () => {
    it('Should return correct values once correct params are passed', () => {
        const inputArray = [1, 2, 3, 4, 5];
        const expectedResult = {
            min: 1,
            max: 5,
            length: 5
        };

        const result = analyzeArray(inputArray);

        expect(result).to.deep.equal(expectedResult);
    })
    it('Should return correct values once correct negative params are passed', () => {
        const inputArray = [-1, -2, -5, -4, -3, -3, -1, -5];
        const expectedResult = {
            min: -5,
            max: -1,
            length: 8
        };

        const result = analyzeArray(inputArray);

        expect(result).to.deep.equal(expectedResult);
    })
    it('Should return correct values once single param is passed', () => {
        const inputArray = [1];
        const expectedResult = {
            min: 1,
            max: 1,
            length: 1
        };

        const result = analyzeArray(inputArray);

        expect(result).to.deep.equal(expectedResult);
    })
    it('Should return correct values once equal numbers are is passed', () => {
        const inputArray = [1, 1, 1, 1, 1];
        const expectedResult = {
            min: 1,
            max: 1,
            length: 5
        };

        const result = analyzeArray(inputArray);

        expect(result).to.deep.equal(expectedResult);
    })
    it('Should return undefined once empty array is passed', () => {
        const inputArray = [];

        const result = analyzeArray(inputArray);

        expect(result).to.be.undefined;
    })
    it('Should return undefined once string array is passed', () => {
        const inputArray = ['arr', 'abc', 'a', 'str'];

        const result = analyzeArray(inputArray);

        expect(result).to.be.undefined;
    })
    it('Should return undefined once invalid params is passed', () => {
        const inputString = 'abc';
        const inputNum = 123;
        const inputNaN = NaN;

        const resultString = analyzeArray(inputString);
        const resultNumber = analyzeArray(inputNum);
        const resultNaN = analyzeArray(inputNaN);

        expect(resultString).to.be.undefined;
        expect(resultNumber).to.be.undefined;
        expect(resultNaN).to.be.undefined;
    })
})
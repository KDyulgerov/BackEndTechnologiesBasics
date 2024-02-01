import { lookupChar } from './02-Char-Lookup.js';
import { expect } from 'chai';

describe('lookupChar', () => {
    it('Should return correct char once correct input is passed', () => {
        const stringInput = 'abcd';
        const indexInput = 0;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('a');
    })
    it('Should return correct char once correct input is passed', () => {
        const stringInput = 'abcd';
        const indexInput = stringInput.length - 1;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('d');
    })
    it('Should return correct white space once correct input with white space is passed', () => {
        const stringInput = 'abcd afaaf fafa asf asfsa ';
        const indexInput = 4;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals(' ');
    })
    it('Should return undefined once non-string is passed', () => {
        const stringInput = 123;
        const indexInput = 1;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined once non-number is passed as index', () => {
        const stringInput = 'test';
        const indexInput = 'abc';

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.undefined;
    })
    it('Should return Incorrect index once invalid index is passed', () => {
        const stringInput = 'abv acd';
        const indexInput = stringInput.length + 2;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('Incorrect index');
    })
    it('Should return Incorrect index once invalid index is passed', () => {
        const stringInput = 'abv acd';
        const indexInput = -1000;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('Incorrect index');
    })
    it('Should return Incorrect index once index with exact input length is passed', () => {
        const stringInput = 'abv acd';
        const indexInput = stringInput.length;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('Incorrect index');
    })
    it('Should return Incorrect index once empty string is passed', () => {
        const stringInput = '';
        const indexInput = 0;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('Incorrect index');
    })
    it('Should return correct result once single char is passed', () => {
        const stringInput = 'C';
        const indexInput = 0;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('C');
    })
    it('Should return correct result once single char is passed', () => {
        const stringInput = '0';
        const indexInput = 0;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.equals('0');
    })
    it('Should return Incorrect index once floating index is passed', () => {
        const stringInput = 'abv abv';
        const indexInput = 0.1;

        const result = lookupChar(stringInput, indexInput);

        expect(result).to.be.undefined;
    })
})
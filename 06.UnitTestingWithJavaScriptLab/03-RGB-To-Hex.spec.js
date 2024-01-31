import { rgbToHexColor } from './03-RGB-To-Hex.js';
import { expect } from 'chai';

describe('rgbToHexColor', () => {
    it('Should return undefined when non-integer is given as red', () => {
        const redInput = 'test';
        const greenInput = 255;
        const blueInput = 0;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when non-integer is given as green', () => {
        const redInput = 255;
        const greenInput = 'test';
        const blueInput = 0;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when non-integer is given as blue', () => {
        const redInput = 0;
        const greenInput = 255;
        const blueInput = 'test';

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when red is below the range', () => {
        const redInput = -10;
        const greenInput = 255;
        const blueInput = 0;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when green is below the range', () => {
        const redInput = 255;
        const greenInput = -255;
        const blueInput = 0;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when blue is below the range', () => {
        const redInput = 0;
        const greenInput = 255;
        const blueInput = -150;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when red is above the range', () => {
        const redInput = 1000;
        const greenInput = 255;
        const blueInput = 0;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when green is below the range', () => {
        const redInput = 2555;
        const greenInput = 255;
        const blueInput = 0;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when blue is below the range', () => {
        const redInput = 0;
        const greenInput = 255;
        const blueInput = 15220;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return correct color when upper boundary are given', () => {
        const redInput = 255;
        const greenInput = 255;
        const blueInput = 255;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.equals('#FFFFFF');
    })
    it('Should return correct color when lower boundary are given', () => {
        const redInput = 0;
        const greenInput = 0;
        const blueInput = 0;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.equals('#000000');
    })
    it('Should return correct color when random correct input is given', () => {
        const redInput = 21;
        const greenInput = 12;
        const blueInput = 21;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.equals('#150C15');
    })
    it('Should return undefined when a floating input is given', () => {
        const redInput = 21.2;
        const greenInput = 12.3;
        const blueInput = 21.43;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
    it('Should return undefined when a negative numbers input is given', () => {
        const redInput = -21;
        const greenInput = -12;
        const blueInput = -21;

        const result = rgbToHexColor(redInput, greenInput, blueInput);

        expect(result).to.be.undefined;
    })
})
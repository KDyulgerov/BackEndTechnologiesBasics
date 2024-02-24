function cinema(input) {
    'use strict';
    const moviesCount = parseInt(input[0], 10);

    const moviesList = input.slice(1, moviesCount + 1);
    const allCommands = input.slice(moviesCount + 1)

    for (let index = 0; index < allCommands.length; index += 1) {
        const rawCommandParams = allCommands[index].split(' ');
        const commandName = rawCommandParams[0];

        if(commandName === 'Sell') {
            const soldMovie = moviesList.shift();
            console.log(`${soldMovie} ticket sold!`);
        } else if(commandName === 'Add') {
            const movieToAdd = allCommands[index].slice(4);
            moviesList.push(movieToAdd);
        } else if(commandName === 'Swap') {
            const firstIndex = parseInt(rawCommandParams[1], 10);
            const secondIndex = parseInt(rawCommandParams[2], 10);

            if(firstIndex < 0 || firstIndex >= moviesList.length) {
                continue;
            }
            if(secondIndex < 0 || secondIndex >= moviesList.length) {
                continue;
            }

            const movieFirstIndex = moviesList[firstIndex];
            moviesList[firstIndex] = moviesList[secondIndex];
            moviesList[secondIndex] = movieFirstIndex;

            console.log('Swapped!');
        } else if(commandName === 'End') {
            break;
        }
    }
    if(moviesList.length) {
        console.log(`Tickets left: ${moviesList.join(', ')}`);
    } else {
        console.log('The box office is empty');
    }
}

cinema(['3','Avatar', 'Titanic', 'Joker', 'Sell', 'End', 'Swap 0 1']);
//cinema(['5', 'The Matrix', 'The Godfather', 'The Shawshank Redemption', 'The Dark Knight', 'Inception', 'Add The Lord of the Rings', 'Swap 1 4', 'End']);
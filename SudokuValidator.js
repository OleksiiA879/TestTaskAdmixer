class SudokuValidator {
    static isValidSolution(board) {
        if (!this.isValidBoardStructure(board)) {
            console.error("Invalid board structure. Board must be a 9x9 array.");
            return false;
        }


        const seen = Array.from({ length: 27 }, () => new Array(10).fill(false));

        for (let r = 0; r < 9; r++) {
            for (let c = 0; c < 9; c++) {
                const value = board[r][c];

                if (value < 1 || value > 9) {
                    return false;
                }

                const boxIndex = Math.floor(r / 3) * 3 + Math.floor(c / 3);


                if (
                    seen[r][value] ||
                    seen[9 + c][value] || 
                    seen[18 + boxIndex][value] 
                ) {
                    return false;
                }


                seen[r][value] = true;
                seen[9 + c][value] = true;
                seen[18 + boxIndex][value] = true;
            }
        }

        return true;
    }

    static isValidBoardStructure(board) {
        return (
            Array.isArray(board) &&
            board.length === 9 &&
            board.every(
                (row) =>
                    Array.isArray(row) &&
                    row.length === 9 &&
                    row.every((cell) => Number.isInteger(cell) && cell >= 0 && cell <= 9)
            )
        );
    }
}

const main = () => {
    const exampleBoard = [
        [5, 3, 4, 6, 7, 8, 9, 1, 2],
        [6, 7, 2, 1, 9, 5, 3, 4, 8],
        [1, 9, 8, 3, 4, 2, 5, 6, 7],
        [8, 5, 9, 7, 6, 1, 4, 2, 3],
        [4, 2, 6, 8, 5, 3, 7, 9, 1],
        [7, 1, 3, 9, 2, 4, 8, 5, 6],
        [9, 6, 1, 5, 3, 7, 2, 8, 4],
        [2, 8, 7, 4, 1, 9, 6, 3, 5],
        [3, 0, 0, 2, 8, 6, 1, 7, 9],
    ];

    const isValid = SudokuValidator.isValidSolution(exampleBoard);

    console.log(isValid ? "true" : "false");
};

main();

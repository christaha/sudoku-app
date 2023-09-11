class Game {
    board: string[][];

    constructor() {
        this.board = this.blank_board_array();
    }

    blank_board_array = () => {
        const row = Array(9).fill(" ")
        return Array(9).fill(row)
    }

    displayBoard = (element: HTMLDivElement) => {
        let result = document.createElement('tbody')
        result.className = 'sudoku-board'
        element.appendChild(result)
        for (let i = 0; i < 9; i++) {
            let row = document.createElement('tr');
            row.className = `${i}_row`;
            result.appendChild(row)
            for (let j = 0; j < 9; j++) {
                let cell = document.createElement('td');
                cell.id = `${i}_${j}_cell`

                let cellInput = document.createElement('input');
                cellInput.type =  "text"
                cellInput.maxLength = 1
                cellInput.className = "cell-input"
                cellInput.id = `${i}_${j}_input`
                
                cell.appendChild(cellInput)
                row.appendChild(cell)
            }
        }
    }

    startGame(element: HTMLDivElement, title: HTMLDivElement, startButton: HTMLButtonElement) {
        console.log("Starting Game")
        this.displayBoard(element)
        element.className = 'game-on'
        title.className = 'hidden'
        startButton.className = 'hidden'
      }
      
}

export default Game;
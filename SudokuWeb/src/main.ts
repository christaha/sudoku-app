import './style.css'
import Game from './game.ts';

const game = new Game()


const gameWrapper = document.querySelector<HTMLDivElement>('#game-wrapper')
const title = document.querySelector<HTMLDivElement>('#title')
const startButton = document.querySelector<HTMLButtonElement>('#start-button')


if (!gameWrapper || !startButton || !title) {
  throw new Error("HTML Error!")
}

startButton.addEventListener('click', () => game.startGame(gameWrapper, title, startButton))

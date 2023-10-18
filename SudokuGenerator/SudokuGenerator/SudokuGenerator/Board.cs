using System;
namespace SudokuGenerator
{
	public class Board
    {
        public char[,] Values;

        public Board(char[,] values)
		{
			this.Values = values;
		}

        public Board() {
            this.Values = new char[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.Values[i,j] = '.';
                }
            }
        }

        public void PrettyPrint()
        {
            Console.WriteLine("-----------");
            for (int i = 0; i < 9; i++) {
                Console.Write("|");
                for (int j = 0; j < 9; j++) {
                    Console.Write(this.Values[i,j].ToString());
                }
                Console.WriteLine("|");
            }
            Console.WriteLine("-----------");

        }

        public Board CloneBoard()
        {
            char[,] copy = new char[9, 9];
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    copy[i, j] = this.Values[i, j];
                }
            }

            return new Board(copy);
        }
    }
}


using System;
using Newtonsoft.Json;

namespace SudokuGenerator
{
	public class Board
    {
        [JsonProperty("values")]
        public char[,] values;

        public Board(char[,] _values)
		{
			this.values = _values;
		}

        public Board() {
            this.values = new char[9, 9];

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    this.values[i,j] = '.';
                }
            }
        }

        public void PrettyPrint()
        {
            Console.WriteLine("-----------");
            for (int i = 0; i < 9; i++) {
                Console.Write("|");
                for (int j = 0; j < 9; j++) {
                    Console.Write(this.values[i,j].ToString());
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
                    copy[i, j] = this.values[i, j];
                }
            }

            return new Board(copy);
        }
    }
}


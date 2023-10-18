using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SudokuGenerator;

public class Solver
{
    public Board Board;
    public List<Board> Solutions;

    public Solver(Board board)
    {
        this.Board = board;
        this.Solutions = new List<Board>();
    }

    bool checkValid(int row, int col, char val)
    {
        for (int c = 0; c < 9; c++)
        {
            if (this.Board.Values[row,c] == val)
            {
                return false;
            }
        }

        for (int r = 0; r < 9; r++)
        {
            if (this.Board.Values[r,col] == val)
            {
                return false;
            }
        }

        int startRow = row - row % 3;
        int startCol = col - col % 3;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (this.Board.Values[i + startRow,j + startCol] == val)
                {
                    return false;
                }

            }
        }
        return true;
    }



    public bool findOne(int idx, bool stopOnFirst)
    {
        if (idx == 81)
        {
            Board copy = this.Board.CloneBoard();
            this.Solutions.Add(copy);
            return true;
        }

        int row = idx / 9;
        int col = idx % 9;

        if (this.Board.Values[row,col] != '.')
        {
            return findOne(idx + 1, stopOnFirst);
        }

        for (int i = 1; i < 10; i++)
        {
            Char val = i.ToString()[0];
            if (checkValid(row, col, val))
            {
                this.Board.Values[row,col] = val;
                bool found = findOne(idx + 1, stopOnFirst);

                if (found && stopOnFirst)
                {
                    return true;
                }

                this.Board.Values[row, col] = '.';

            }
        }

        return false;
    }

    public void findSolutions()
    {
        findOne(0, false);
    }

    public void findSolution()
    {
        findOne(0, true);
    }
}


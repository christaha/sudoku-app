using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SudokuGenerator;

public class Solver
{
    bool checkValid(int row, int col, char val, Board board)
    {
        for (int c = 0; c < 9; c++)
        {
            if (board.values[row,c] == val)
            {
                return false;
            }
        }

        for (int r = 0; r < 9; r++)
        {
            if (board.values[r,col] == val)
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
                if (board.values[i + startRow,j + startCol] == val)
                {
                    return false;
                }

            }
        }
        return true;
    }



    public bool findOne(int idx, bool stopOnFirst, Board board, List<Board> solutions)
    {
        if (idx == 81)
        {
            Board copy = board.CloneBoard();
            solutions.Add(copy);
            return true;
        }

        int row = idx / 9;
        int col = idx % 9;

        if (board.values[row, col] != '.')
        {
            return findOne(idx + 1, stopOnFirst, board, solutions);
        }

        for (int i = 1; i < 10; i++)
        {
            Char val = i.ToString()[0];
            if (checkValid(row, col, val, board))
            {
                board.values[row, col] = val;
                bool found = findOne(idx + 1, stopOnFirst, board, solutions);

                if (found && stopOnFirst)
                {
                    return true;
                }

                board.values[row, col] = '.';

            }
        }
        return false;
    }

        public List<Board> findSolutions(Board board)
    {
        Board copy = board.CloneBoard();
        var solutions = new List<Board>();
        findOne(0, false, copy, solutions);
        return solutions;
    }

    public Board findSolution(Board board)
    {
        Board copy = board.CloneBoard();
        List<Board> solutions = new List<Board>();
        findOne(0, true, copy, solutions);
        if (solutions.Count <= 0)
        {
            throw new IndexOutOfRangeException("Failed to find solution");
        }
        return solutions[0];
    }
}


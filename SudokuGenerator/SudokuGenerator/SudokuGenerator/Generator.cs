using System;

namespace SudokuGenerator;

public class Generator
{
    public string Level;
    public Board board;
    public Solver solver;

    public Generator(string level, Board board, Solver solver)
    {
        this.Level = level;
        this.board = board;
        this.solver = solver;
    }

    public Generator(string level)
    {
        this.Level = level;
        this.board = new Board();
        this.solver = new Solver(this.board);
    }

    public int[] createRandomIntArray(int start, int end)
    {
        Random rnd = new Random();
        List<int> rowStart = new List<int>();

        for (int i = start; i < end; i++)
        {
            rowStart.Add(i);
        }

        int[] shuffledRow = rowStart.OrderBy(x => rnd.Next()).ToArray<int>();
        return shuffledRow;
    }

    public char[] createRandomCharArray(int start, int end)
    {
        Random rnd = new Random();
        List<char> rowStart = new List<char>();

        for (int i = start; i < end; i++)
        {
            rowStart.Add(i.ToString()[0]);
        }

        char[] shuffledRow = rowStart.OrderBy(x => rnd.Next()).ToArray<char>();
        return shuffledRow;
    }

    public void generateBase()
    {
        for (int i = 0; i < 3; i++)
        {
            char[] randomRow = createRandomCharArray(1, 10);
            int square = i;
            for (int j = 0; j < 9; j++)
            {
                float rowOffset = j / 3;
                int colOffset = j % 3;
                int offset = i * 3;
                int row = (int)Math.Floor(rowOffset) + offset;
                int col = colOffset + offset;

                this.board.Values[row, col] = randomRow[j];
            }
        }

        this.board.PrettyPrint();

        this.solver.findSolution();
    }

    public void generateOne()
    {
        var queue = new Queue<int>(createRandomIntArray(0, 81));

        var toRemove = 50;

        this.generateBase();
        while (queue.Count > 0 && toRemove >= 0)
        {
            var next = queue.Dequeue();
            var removed = removeOne(next);
            Console.WriteLine(String.Format("Removing square at {0}", next));
            if (removed)
            {
                toRemove -= 1;
            }
        }

    }

    public bool removeOne(int idx)
    {
        int row = idx / 9;
        int col = idx % 9;

        char temp = this.board.Values[row, col];
        this.board.Values[row, col] = '.';

        var solver = new Solver(this.board.CloneBoard());
        solver.findSolutions();
        if (solver.Solutions.Count() > 1)
        {
            Console.WriteLine("More than one solution!");
            this.board.Values[row, col] = temp;
            return false;
        }
        return true;
        
    }
}


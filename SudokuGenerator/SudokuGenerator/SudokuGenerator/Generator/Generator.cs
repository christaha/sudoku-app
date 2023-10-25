using System;
using System.Text;
using Newtonsoft.Json;

namespace SudokuGenerator;

public class Generator
{
    public int level;
    public Solver solver;
    private HttpClient httpClient;

    public Generator(int _level)
    {
        level = _level;
        solver = new Solver();
        httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:5128") };
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


    async public Task Save(Board board)
    {
        using StringContent jsonContent = new(
        JsonConvert.SerializeObject(new
        {
            level = this.level,
            board = board
        }),
        Encoding.UTF8,
        "application/json");

       
        board.PrettyPrint();

        using HttpResponseMessage response = await httpClient.PostAsync(
                "/api/Puzzles",
                jsonContent);

        Console.WriteLine($"{await jsonContent.ReadAsStringAsync()}\n");

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");
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

    public Board generateBase()
    {
        Board board = new Board();

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

                board.values[row, col] = randomRow[j];
            }
        }

        return this.solver.findSolution(board);
    }

    public bool removeOne(int idx, Board board)
    {
        int row = idx / 9;
        int col = idx % 9;

        char temp = board.values[row, col];
        board.values[row, col] = '.';

        var solver = new Solver();
        List<Board> solutions = solver.findSolutions(board);
        if (solutions.Count() > 1)
        {
            Console.WriteLine("More than one solution!");
            board.values[row, col] = temp;
            return false;
        }
        return true;
    }

    public async Task<Board> generateOne()
    {
        Board board = generateBase();

        var queue = new Queue<int>(createRandomIntArray(0, 81));

        var toRemove = 50;

        this.generateBase();
        while (queue.Count > 0 && toRemove >= 0)
        {
            var next = queue.Dequeue();
            var removed = removeOne(next, board);
            Console.WriteLine(String.Format("Removing square at {0}", next));
            if (removed)
            {
                toRemove -= 1;
            }
        }
        await this.Save(board);
        return board;
    }
}


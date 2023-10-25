
namespace SudokuGenerator;
using System;


internal class Program
    {
        async static Task Main(string[] args)
        {

        Generator generator = new Generator(0);

        Console.WriteLine("Starting Generation");
        await generator.generateOne();

        Console.WriteLine("Ending Generation");
    }
}

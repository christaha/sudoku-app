
namespace SudokuGenerator; // Note: actual namespace depends on the project name.
using System;


internal class Program
    {
        static void Main(string[] args)
        {

        Generator generator = new Generator("easy");

        Console.WriteLine("Starting Generation");
        generator.generateOne();

        generator.board.PrettyPrint();

        Console.WriteLine("Ending Generation");
    }
}

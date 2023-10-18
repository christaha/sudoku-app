using System;
namespace SudokuApi.Models
{
    public class Board
    {
        public List<List<String>> Values { get; set; }

        public Board()
        {
            var values = new List<List<string>>();
            for (int i = 0; i < 9; i++)
            {
                values.Add(new List<string>());
                for (int j = 0; j < 9; j++)
                {
                    values[i].Add("");
                }
            }
            this.Values = values;
        }
    }
}


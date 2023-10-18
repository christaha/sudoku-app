namespace SudokuApi.Models;

public class SudokuDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string PuzzlesCollectionName { get; set; } = null!;
}
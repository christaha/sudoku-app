using Swashbuckle.AspNetCore.Filters;
using SudokuApi.Models.Dto;
using SudokuApi.Models;

public class PuzzleDtoExample : IMultipleExamplesProvider<PuzzleDto>
{
    public IEnumerable<SwaggerExample<PuzzleDto>> GetExamples() {
        yield return SwaggerExample.Create(
        "Example 1",
            new PuzzleDto()
            {
                Board = new Board(),
                Level = Level.Easy
            }
        );
    } 
}
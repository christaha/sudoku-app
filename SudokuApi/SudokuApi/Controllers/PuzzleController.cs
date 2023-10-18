using SudokuApi.Models;
using SudokuApi.Services;
using Microsoft.AspNetCore.Mvc;
using SudokuApi.Models.Dto;
using Swashbuckle.AspNetCore.Filters;
using System.Net;


namespace SudokuApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PuzzlesController : ControllerBase
{
    private readonly PuzzleService _puzzleService;

    public PuzzlesController(PuzzleService puzzleService) =>
        _puzzleService = puzzleService;

    [HttpGet]
    public async Task<List<Puzzle>> Get() =>
        await _puzzleService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Puzzle>> Get(string id)
    {
        var puzzle = await _puzzleService.GetAsync(id);

        if (puzzle is null)
        {
            return NotFound();
        }

        return puzzle;
    }

    [HttpPost]
    [SwaggerRequestExample(typeof(PuzzleDto), typeof(PuzzleDtoExample))]
    public async Task<IActionResult> Post(PuzzleDto newPuzzle)
    {
        if (!_puzzleService.validateNewBoard(newPuzzle.Board)) {
            return BadRequest();
        }

        Puzzle puzzle = await _puzzleService.CreateAsync(newPuzzle);

        return CreatedAtAction(nameof(Get), new { id = puzzle.Id }, puzzle);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Puzzle updatedPuzzle)
    {
        var puzzle = await _puzzleService.GetAsync(id);

        if (puzzle is null)
        {
            return NotFound();
        }

        updatedPuzzle.Id = puzzle.Id;

        await _puzzleService.UpdateAsync(id, updatedPuzzle);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var puzzle = await _puzzleService.GetAsync(id);
        
        if (puzzle is null)
        {
            return NotFound();
        }

        await _puzzleService.RemoveAsync(id);

        return NoContent();
    }
}
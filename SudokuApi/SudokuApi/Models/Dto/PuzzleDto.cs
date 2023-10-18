using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SudokuApi.Models.Dto;

public class PuzzleDto
{
    public Level Level { get; set; }
    public Board Board { get; set; } = new Board();
}


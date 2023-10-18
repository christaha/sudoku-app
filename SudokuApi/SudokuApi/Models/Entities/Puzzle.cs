using System.Xml.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SudokuApi.Models;

public class Puzzle
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public Level Level { get; set; }

    public Board Board { get; set; } = new Board();

    public Board GenerateBoard()
    {
        var board = new Board();

        return new Board();
    }

}


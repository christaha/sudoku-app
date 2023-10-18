using System;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SudokuApi.Models;
using SudokuApi.Models.Dto;

namespace SudokuApi.Services
{
    public class PuzzleService
    {

        private readonly IMongoCollection<Puzzle> _puzzlesCollection;

        public PuzzleService(
            IOptions<SudokuDatabaseSettings> sudokuDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                sudokuDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                sudokuDatabaseSettings.Value.DatabaseName);

            _puzzlesCollection = mongoDatabase.GetCollection<Puzzle>(
                sudokuDatabaseSettings.Value.PuzzlesCollectionName);
        }


        public bool validateNewBoard(Board board)
        {
            if (board.Values.Count != 9)
            {
                return false;
            }

            for (int i = 0; i < 9; i++)
            {
                if (board.Values[i].Count < 9)
                {
                    return false;
                }
            }
            return true;
        }
        

        public async Task<List<Puzzle>> GetAsync() =>
            await _puzzlesCollection.Find(_ => true).ToListAsync();

        public async Task<Puzzle?> GetAsync(string id) =>
            await _puzzlesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Puzzle> CreateAsync(PuzzleDto newPuzzle) {
            Puzzle puzzle = new Puzzle() { Level = newPuzzle.Level, Board = newPuzzle.Board };
            await _puzzlesCollection.InsertOneAsync(puzzle);
            return puzzle;
    }

        public async Task UpdateAsync(string id, Puzzle updatedPuzzle) =>
            await _puzzlesCollection.ReplaceOneAsync(x => x.Id == id, updatedPuzzle);

        public async Task RemoveAsync(string id) =>
            await _puzzlesCollection.DeleteOneAsync(x => x.Id == id);
	}
}


using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using BowlingAlleyManager.Models;
using BowlingAlleyManager.Data;
using BowlingAlleyManager.Services.Interfaces;

namespace BowlingAlleyManager.Services
{
    public class ResultService : IResultService
    {
        private readonly IDbConnection _dbConnection;

        public ResultService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void RegisterMatchResult(long matchID, Dictionary<long, int> playerScores)
        {
            string matchExistsQuery = "SELECT COUNT(*) FROM Matches WHERE MatchID = @MatchID";
            int matchExists = _dbConnection.ExecuteScalar<int>(matchExistsQuery, new { MatchID = matchID });

            if (matchExists == 0)
            {
                Console.WriteLine($"Error: Match {matchID} does not exist.");
                return;
            }

            foreach (var entry in playerScores)
            {
                string insertScore = "INSERT INTO Results (MatchID, PlayerID, Score) VALUES (@MatchID, @PlayerID, @Score)";
                _dbConnection.Execute(insertScore, new { MatchID = matchID, PlayerID = entry.Key, Score = entry.Value });
            }
        }

        public long DetermineMatchWinner(long matchID)
        {
            string query = @"
            SELECT Players.PlayerID, Players.Name, Results.Score
            FROM Results
            JOIN Players ON Results.PlayerID = Players.PlayerID
            WHERE Results.MatchID = @MatchID
            ORDER BY Results.Score DESC
            LIMIT 1;
            ";

            var winner = _dbConnection.QueryFirstOrDefault<(long PlayerID, string Name, int Score)>(query, new { MatchID = matchID });

            if (winner.PlayerID > 0)
            {
                string updateWinner = "UPDATE Matches SET WinnerID = @PlayerID WHERE MatchID = @MatchID";
                _dbConnection.Execute(updateWinner, new { PlayerID = winner.PlayerID, MatchID = matchID });

                Console.WriteLine($"\nMatch {matchID} Winner: {winner.Name} with {winner.Score} points!");
                return winner.PlayerID;
            }

            Console.WriteLine($"No results found for Match {matchID}. Cannot determine a winner.");
            return -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using BowlingAlleyManager.Models;
using BowlingAlleyManager.Data;

namespace BowlingAlleyManager.Services
{
    public class ResultService
    {
        private const string ConnectionString = "Data Source=bowlinghall.db";

        public void RegisterMatchResult(long matchID, Dictionary<long, int> playerScores)
        {
            using var connection = Database.GetConnection();

            foreach (var entry in playerScores)
            {
                long playerID = entry.Key;
                int score = entry.Value;

                string insertScore = "INSERT INTO Results (MatchID, PlayerID, Score) VALUES (@MatchID, @PlayerID, @Score)";
                connection.Execute(insertScore, new { MatchID = matchID, PlayerID = playerID, Score = score });
            }

            long winnerID = DetermineWinner(playerScores);
            Console.WriteLine($"Match {matchID} finished! Winner: Player {winnerID} with score {playerScores[winnerID]}.");
        }

        private long DetermineWinner(Dictionary<long, int> playerScores)
        {
            long winnerID = -1;
            int highestScore = int.MinValue;

            foreach (var entry in playerScores)
            {
                if (entry.Value > highestScore)
                {
                    highestScore = entry.Value;
                    winnerID = entry.Key;
                }
            }

            return winnerID;
        }
    }
}

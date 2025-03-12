using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using BowlingAlleyManager.Models;
using BowlingAlleyManager.Factories;
using BowlingAlleyManager.Data;

namespace BowlingAlleyManager.Services
{
    public class MatchService
    {
        private const string ConnectionString = "Data Source=bowlinghall.db";

        public void CreateMatch(List<Player> players, int lane)
        {
            using var connection = Database.GetConnection();

            string insertMatch = "INSERT INTO Matches (Lane) VALUES (@Lane); SELECT last_insert_rowid();";
            int matchID = connection.ExecuteScalar<int>(insertMatch, new { Lane = lane });

            foreach (var player in players)
            {
                string insertParticipation = "INSERT INTO MatchParticipation (MatchID, PlayerID) VALUES (@MatchID, @PlayerID)";
                connection.Execute(insertParticipation, new { MatchID = matchID, PlayerID = player.PlayerID });
            }

            Console.WriteLine($"Match {matchID} created on lane {lane} with {players.Count} players.");
        }

        public List<Match> GetAllMatches()
        {
            using var connection = Database.GetConnection();
            var matches = connection.Query<Match>("SELECT * FROM Matches").AsList();
            return matches;
        }
    }
}

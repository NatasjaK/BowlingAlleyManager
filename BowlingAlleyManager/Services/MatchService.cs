using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using BowlingAlleyManager.Models;
using BowlingAlleyManager.Factories;
using BowlingAlleyManager.Data;
using BowlingAlleyManager.Services.Interfaces;

namespace BowlingAlleyManager.Services
{
    public class MatchService : IMatchService
    {
        private readonly IDbConnection _dbConnection;

        // Constructor with dependency injection
        public MatchService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void CreateMatch(List<Player> players, int lane)
        {
            string insertMatch = "INSERT INTO Matches (Lane) VALUES (@Lane); SELECT last_insert_rowid();";
            int matchID = _dbConnection.ExecuteScalar<int>(insertMatch, new { Lane = lane });

            foreach (var player in players)
            {
                string insertParticipation = "INSERT INTO MatchParticipation (MatchID, PlayerID) VALUES (@MatchID, @PlayerID)";
                _dbConnection.Execute(insertParticipation, new { MatchID = matchID, PlayerID = player.PlayerID });
            }

            Console.WriteLine($"Match {matchID} created on lane {lane} with {players.Count} players.");
        }

        public List<Match> GetAllMatches()
        {
            return _dbConnection.Query<Match>("SELECT * FROM Matches").AsList();
        }

        public List<Player> GetPlayersInMatch(long matchID)
        {
            string query = @"
            SELECT Players.PlayerID, Players.Name, Players.Email, Players.PhoneNr
            FROM MatchParticipation
            JOIN Players ON MatchParticipation.PlayerID = Players.PlayerID
            WHERE MatchParticipation.MatchID = @MatchID;
            ";

            return _dbConnection.Query<Player>(query, new { MatchID = matchID }).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using BowlingAlleyManager.Models;
using BowlingAlleyManager.Data;
using BowlingAlleyManager.Services.Interfaces;

namespace BowlingAlleyManager.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly IDbConnection _dbConnection;

        // Constructor with dependency injection
        public TournamentService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void CreateTournament(string name, DateTime startDate, DateTime endDate)
        {
            string insertTournament = "INSERT INTO Tournaments (Name, StartDate, EndDate) VALUES (@Name, @StartDate, @EndDate);";
            _dbConnection.Execute(insertTournament, new { Name = name, StartDate = startDate, EndDate = endDate });
        }

        public List<Tournament> GetAllTournaments()
        {
            return _dbConnection.Query<Tournament>("SELECT * FROM Tournaments").AsList();
        }
    }
}

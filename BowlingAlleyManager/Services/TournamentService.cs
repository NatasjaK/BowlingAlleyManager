using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;
using BowlingAlleyManager.Models;
using BowlingAlleyManager.Data;

namespace BowlingAlleyManager.Services
{
    public class TournamentService
    {
        private const string ConnectionString = "Data Source=bowlinghall.db";

        public void CreateTournament(string name, DateTime startDate, DateTime endDate)
        {
            using var connection = Database.GetConnection();
            string insertTournament = "INSERT INTO Tournaments (Name, StartDate, EndDate) VALUES (@Name, @StartDate, @EndDate);";
            connection.Execute(insertTournament, new { Name = name, StartDate = startDate, EndDate = endDate });

            Console.WriteLine($"Tournament '{name}' created from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}.");
        }

        public List<Tournament> GetAllTournaments()
        {
            using var connection = Database.GetConnection();
            return connection.Query<Tournament>("SELECT * FROM Tournaments").AsList();
        }
    }
}

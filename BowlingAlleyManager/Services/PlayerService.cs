using BowlingAlleyManager.Models;
using BowlingAlleyManager.Services.Interfaces;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Services
{
    public class PlayerService : IPlayerService
    {

        private readonly IDbConnection _dbConnection;
        // Konstruktor där databaskopplingen injiceras
        public PlayerService(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void RegisterPlayer(string name, string email, string phoneNr)
        {
            string sql = "INSERT INTO Players (Name, Email, PhoneNr) VALUES (@Name, @Email, @PhoneNr)";
            _dbConnection.Execute(sql, new { Name = name, Email = email, PhoneNr = phoneNr });
            Console.WriteLine($"Player {name} registered successfully.");
        }

        public List<Player> GetAllPlayers()
        {
            return _dbConnection.Query<Player>("SELECT * FROM Players").AsList();
        }
    }
}

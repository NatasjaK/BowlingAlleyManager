using BowlingAlleyManager.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BowlingAlleyManager.Services
{
    public class PlayerService
    {

        private const string ConnectionString = "Data Source=bowlinghall.db";

        public void RegisterPlayer(string name, string email, string phoneNr)
        {
            using var connection = new SqliteConnection(ConnectionString);
            string sql = "INSERT INTO Players (Name, Email, PhoneNr) VALUES (@Name, @Email, @PhoneNr)";
            connection.Execute(sql, new { Name = name, Email = email, PhoneNr = phoneNr });

            Console.WriteLine($"Player {name} registered successfully.");
        }

        public List<Player> GetAllPlayers()
        {
            using var connection = new SqliteConnection(ConnectionString);
            return connection.Query<Player>("SELECT * FROM Players").AsList();
        }
    }
}

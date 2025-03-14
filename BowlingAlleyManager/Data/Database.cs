using System;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BowlingAlleyManager.Data
{
    /// <summary>
    /// Database class responsible for initializing tables.
    /// Uses Dependency Injection for handling connections.
    /// </summary>
    public class Database
    {
        private readonly IDbConnection _connection;

        // Inject the database connection via the constructor
        public Database(IDbConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Initializes the database by creating required tables.
        /// </summary>
        public void Initialize()
        {
            _connection.Open();

            string createTables = @"
                CREATE TABLE IF NOT EXISTS Players (
                    PlayerID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Email TEXT UNIQUE NOT NULL,
                    PhoneNr TEXT UNIQUE NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Matches (
                    MatchID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Lane INTEGER NOT NULL,
                    WinnerID INTEGER NULL,
                    FOREIGN KEY (WinnerID) REFERENCES Players(PlayerID)
                );

                CREATE TABLE IF NOT EXISTS MatchParticipation (
                    MatchID INTEGER NOT NULL,
                    PlayerID INTEGER NOT NULL,
                    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID),
                    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID)
                );

                CREATE TABLE IF NOT EXISTS Results (
                    ResultID INTEGER PRIMARY KEY AUTOINCREMENT,
                    MatchID INTEGER NOT NULL,
                    PlayerID INTEGER NOT NULL,
                    Score INTEGER NOT NULL,
                    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID),
                    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID)
                );

                CREATE TABLE IF NOT EXISTS Tournaments (
                    TournamentID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    StartDate TEXT NOT NULL,
                    EndDate TEXT NOT NULL,
                    WinnerID INTEGER NULL,
                    FOREIGN KEY (WinnerID) REFERENCES Players(PlayerID)
                );
            ";

            _connection.Execute(createTables);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BowlingAlleyManager.Data
{
    class Database
    {
        private const string ConnectionString = "Data Source=bowlinghall.db;Version=3;";

        public static void Initialize()
        {
            using var connection = new SqliteConnection(ConnectionString);
            connection.Open();

            string createTables = @"
                CREATE TABLE IF NOT EXISTS Players (
                    PlayerID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Email TEXT UNIQUE NOT NULL,
                    PhoneNr TEXT UNIQUE NOT NULL
                );

                CREATE TABLE IF NOT EXISTS Matches (
                    MatchID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Lane INTEGER NOT NULL
                );

                CREATE TABLE IF NOT EXISTS MatchParticipation (
                    MatchID INTEGER NOT NULL,
                    PlayerID INTEGER NOT NULL,
                    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID),
                    FOREIGN KEY (PlayerID) REFERENCES Players(PlayerID)
                );

                CREATE TABLE IF NOT EXISTS Results (
                    MatchID INTEGER PRIMARY KEY,
                    WinnerID INTEGER NOT NULL,
                    FOREIGN KEY (MatchID) REFERENCES Matches(MatchID),
                    FOREIGN KEY (WinnerID) REFERENCES Players(PlayerID)
                );";

            connection.Execute(createTables);
        }

        public static IDbConnection GetConnection() => new SqliteConnection(ConnectionString);
    }
}

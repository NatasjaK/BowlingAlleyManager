using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using BowlingAlleyManager.Services.Interfaces;
using BowlingAlleyManager.Data;
using System.Data;
using Microsoft.Data.Sqlite;
using BowlingAlleyManager.Services;
using BowlingAlleyManager.Models;

namespace BowlingAlleyManager.Facades
{
    public class GameFacade
    {
        private readonly IPlayerService _playerService;
        private readonly IMatchService _matchService;
        private readonly IResultService _resultService;
        private readonly ITournamentService _tournamentService;
        private readonly Database _database;

        public GameFacade()
        {
            // Set up Dependency Injection container
            var serviceProvider = new ServiceCollection()
                .AddScoped<IDbConnection>(_ => new SqliteConnection("Data Source=bowlinghall.db"))
                .AddScoped<Database>()
                .AddScoped<IPlayerService, PlayerService>()
                .AddScoped<IMatchService, MatchService>()
                .AddScoped<IResultService, ResultService>()
                .AddScoped<ITournamentService, TournamentService>()
                .BuildServiceProvider();

            // Resolve dependencies
            _database = serviceProvider.GetRequiredService<Database>();
            _playerService = serviceProvider.GetRequiredService<IPlayerService>();
            _matchService = serviceProvider.GetRequiredService<IMatchService>();
            _resultService = serviceProvider.GetRequiredService<IResultService>();
            _tournamentService = serviceProvider.GetRequiredService<ITournamentService>();

            // Initialize database
            _database.Initialize();
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the bowling hall! =)");

            while (true)
            {
                Console.WriteLine("\nMENU:");
                Console.WriteLine("1. Register Player");
                Console.WriteLine("2. Show All Players");
                Console.WriteLine("3. Create Match");
                Console.WriteLine("4. Register Match Result");
                Console.WriteLine("5. Create Tournament");
                Console.WriteLine("6. Show Tournaments");
                Console.WriteLine("7. Determine Match Winner");
                Console.WriteLine("8. Exit\n");

                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterPlayer();
                        break;
                    case "2":
                        ShowAllPlayers();
                        break;
                    case "3":
                        CreateMatch();
                        break;
                    case "4":
                        RegisterMatchResult();
                        break;
                    case "5":
                        CreateTournament();
                        break;
                    case "6":
                        ShowTournaments();
                        break;
                    case "7":
                        DetermineMatchWinner();
                        break;
                    case "8":
                        Console.WriteLine("Exiting...");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
        }

        private void RegisterPlayer()
        {
            Console.Write("Enter player name: ");
            string name = Console.ReadLine().Trim();

            Console.Write("Enter player email: ");
            string email = Console.ReadLine().Trim();

            Console.Write("Enter player phone number: ");
            string phoneNr = Console.ReadLine().Trim();

            _playerService.RegisterPlayer(name, email, phoneNr);
        }

        private void ShowAllPlayers()
        {
            var players = _playerService.GetAllPlayers();
            if (players.Count == 0)
            {
                Console.WriteLine("No players registered yet.");
                return;
            }

            Console.WriteLine("\nRegistered Players:");
            foreach (var player in players)
            {
                Console.WriteLine($"- {player.Name} ({player.Email}, {player.PhoneNr})");
            }
        }

        private void CreateMatch()
        {
            var players = _playerService.GetAllPlayers();
            if (players.Count == 0)
            {
                Console.WriteLine("No players available. Register players first.");
                return;
            }

            Console.Write("Enter lane number: ");
            if (!int.TryParse(Console.ReadLine(), out int lane))
            {
                Console.WriteLine("Invalid lane number.");
                return;
            }

            List<Player> selectedPlayers = new List<Player>();

            Console.WriteLine("Select players by entering their numbers (comma-separated, e.g. 1,2,3):");

            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {players[i].Name}");
            }

            string[] input = Console.ReadLine().Split(',');
            foreach (string id in input)
            {
                if (int.TryParse(id.Trim(), out int playerIndex) && playerIndex > 0 && playerIndex <= players.Count)
                {
                    selectedPlayers.Add(players[playerIndex - 1]);
                }
            }

            if (selectedPlayers.Count < 1)
            {
                Console.WriteLine("At least one player must be selected.");
                return;
            }

            _matchService.CreateMatch(selectedPlayers, lane);
            Console.WriteLine($"Match created on lane {lane} with {selectedPlayers.Count} players.");
        }


        private void RegisterMatchResult()
        {
            var matches = _matchService.GetAllMatches();
            if (matches.Count == 0)
            {
                Console.WriteLine("No matches available.");
                return;
            }

            Console.WriteLine("Select a match by entering its number:");
            for (int i = 0; i < matches.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Match {matches[i].MatchID} on lane {matches[i].Lane}");
            }

            if (!int.TryParse(Console.ReadLine(), out int matchIndex) || matchIndex < 1 || matchIndex > matches.Count)
            {
                Console.WriteLine("Invalid match selection.");
                return;
            }

            long matchID = matches[matchIndex - 1].MatchID;
            var players = _matchService.GetPlayersInMatch(matchID);

            if (players.Count == 0)
            {
                Console.WriteLine($"No players found for Match {matchID}.");
                return;
            }

            Dictionary<long, int> playerScores = new Dictionary<long, int>();

            Console.WriteLine("Enter scores for each player:");
            foreach (var player in players)
            {
                Console.Write($"Score for {player.Name} (ID {player.PlayerID}): ");
                if (!int.TryParse(Console.ReadLine(), out int score))
                {
                    Console.WriteLine("Invalid score. Please enter a valid number.");
                    return;
                }
                playerScores[player.PlayerID] = score;
            }

            _resultService.RegisterMatchResult(matchID, playerScores);
            Console.WriteLine($"Match {matchID} results recorded!");
        }


        private void CreateTournament()
        {
            Console.Write("Enter tournament name: ");
            string name = Console.ReadLine().Trim();

            Console.Write("Enter start date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Invalid date format. Please enter in YYYY-MM-DD format.");
                return;
            }

            Console.Write("Enter end date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
            {
                Console.WriteLine("Invalid date format. Please enter in YYYY-MM-DD format.");
                return;
            }

            _tournamentService.CreateTournament(name, startDate, endDate);
            Console.WriteLine($"Tournament '{name}' created successfully!");
        }

        private void ShowTournaments()
        {
            var tournaments = _tournamentService.GetAllTournaments();
            if (tournaments.Count == 0)
            {
                Console.WriteLine("No tournaments available.");
                return;
            }

            Console.WriteLine("\nTournaments:");
            foreach (var tournament in tournaments)
            {
                Console.WriteLine($"- {tournament.Name} ({tournament.StartDate.ToShortDateString()} - {tournament.EndDate.ToShortDateString()})");
            }
        }

        private void DetermineMatchWinner()
        {
            Console.Write("Enter match ID: ");
            if (!long.TryParse(Console.ReadLine(), out long matchID))
            {
                Console.WriteLine("Invalid match ID. Please enter a valid number.");
                return;
            }

            _resultService.DetermineMatchWinner(matchID);
        }

    }
}

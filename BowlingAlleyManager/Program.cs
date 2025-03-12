using System;
using System.Collections.Generic;
using BowlingAlleyManager.Data;
using BowlingAlleyManager.Services;
using BowlingAlleyManager.Models;

class Program
{
    static PlayerService playerService = new PlayerService();
    static MatchService matchService = new MatchService();
    static ResultService resultService = new ResultService();
    static TournamentService tournamentService = new TournamentService();


    static void Main()
    {
        Console.WriteLine("Starting Bowling Hall System...");
        Database.Initialize();
        Console.WriteLine("Database initialized!");

        while (true)
        {
            Console.WriteLine("\nMENU:");
            Console.WriteLine("1. Register Player");
            Console.WriteLine("2. Show All Players");
            Console.WriteLine("3. Create Match");
            Console.WriteLine("4. Register Match Result");
            Console.WriteLine("5. Create Tournament");
            Console.WriteLine("6. Show Tournaments");
            Console.WriteLine("7. Exit");

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
                    Console.WriteLine("Exiting...");
                    return;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void RegisterPlayer()
    {
        Console.Write("Enter player name: ");
        string name = Console.ReadLine();

        Console.Write("Enter player email: ");
        string email = Console.ReadLine();

        Console.Write("Enter player phone number: ");
        string phoneNr = Console.ReadLine();

        playerService.RegisterPlayer(name, email, phoneNr);
    }

    static void ShowAllPlayers()
    {
        var players = playerService.GetAllPlayers();
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

    static void CreateMatch()
    {
        var players = playerService.GetAllPlayers();
        if (players.Count == 0)
        {
            Console.WriteLine("No players available. Register players first.");
            return;
        }

        Console.Write("Enter lane number: ");
        int lane = int.Parse(Console.ReadLine());

        List<Player> selectedPlayers = new List<Player>();

        Console.WriteLine("Select players by entering their ID (separated by spaces):");
        foreach (var player in players)
        {
            Console.WriteLine($"{player.PlayerID}. {player.Name}");
        }

        string[] input = Console.ReadLine().Split();
        foreach (string id in input)
        {
            if (long.TryParse(id, out long playerID))
            {
                var player = players.Find(p => p.PlayerID == playerID);
                if (player != null)
                {
                    selectedPlayers.Add(player);
                }
            }
        }

        if (selectedPlayers.Count < 1)
        {
            Console.WriteLine("At least one player must be selected.");
            return;
        }

        matchService.CreateMatch(selectedPlayers, lane);
    }

    static void RegisterMatchResult()
    {
        Console.Write("Enter match ID: ");
        long matchID = long.Parse(Console.ReadLine());  

        Dictionary<long, int> playerScores = new Dictionary<long, int>();  

        Console.WriteLine("Enter scores for each player:");
        foreach (var player in playerService.GetAllPlayers())
        {
            Console.Write($"Score for {player.Name} (ID {player.PlayerID}): ");
            int score = int.Parse(Console.ReadLine());  
            playerScores[player.PlayerID] = score;
        }

        resultService.RegisterMatchResult(matchID, playerScores);
    }

    static void CreateTournament()
    {
        Console.Write("Enter tournament name: ");
        string name = Console.ReadLine();

        Console.Write("Enter start date (YYYY-MM-DD): ");
        DateTime startDate = DateTime.Parse(Console.ReadLine());

        Console.Write("Enter end date (YYYY-MM-DD): ");
        DateTime endDate = DateTime.Parse(Console.ReadLine());

        tournamentService.CreateTournament(name, startDate, endDate);
    }

    static void ShowTournaments()
    {
        var tournaments = tournamentService.GetAllTournaments();
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



}


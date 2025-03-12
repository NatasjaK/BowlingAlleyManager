using System;
using System.Collections.Generic;
using BowlingAlleyManager.Data;
using BowlingAlleyManager.Services;
using BowlingAlleyManager.Models;

class Program
{
    static PlayerService playerService = new PlayerService();
    static MatchService matchService = new MatchService();

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
            Console.WriteLine("4. Exit");
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
            if (int.TryParse(id, out int playerID))
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
}

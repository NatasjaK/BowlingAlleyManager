using BowlingAlleyManager.Facades;

class Program
{
    static void Main()
    {
        var game = new GameFacade();
        game.Run();
    }
}

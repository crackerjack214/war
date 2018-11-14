using System;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a new instance of the game
            Game game = new Game();

            // Wait for spacebar to continue the game
            Console.WriteLine("Press Space to draw a card");
            do
            {
                while (!Console.KeyAvailable)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Spacebar)
                    {
                        if (game.gameEnd)
                        {
                            // If game has ended, start a new game
                            game = new Game();
                        } else {
                            // Execute a new turn
                            game.NewTurn();
                        }
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}

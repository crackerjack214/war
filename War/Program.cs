using System;

namespace War
{
    class Program
    {
        static void Main(string[] args)
        {
            // Generate a new instance of the game
            Game game = new Game(new string[2]{"Cylon", "Meatbag"});

            // Wait for spacebar to continue the game
            Console.WriteLine("Press Space to draw a card");
            do
            {
                while (!Console.KeyAvailable)
                {
                    switch(Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.Spacebar:
                            if (!game.gameEnd)
                            {
                                // Execute a new turn
                                game.NewTurn();
                            }
                            break;
                        case ConsoleKey.Enter:
                            if (game.gameEnd)
                            {
                                // If game has ended, start a new game
                                game = new Game(new string[2] { "Cylon", "Meatbag" });
                            }
                            break;
                        default:
                            break;
                    }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}

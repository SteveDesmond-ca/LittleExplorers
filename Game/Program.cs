using System;
using SimpleGPIO.Boards;

namespace LittleExplorers.Game
{
    public static class Program
    {
        public static void Main()
        {
            var pi = new RaspberryPi();
            var game = new Game(pi);
            game.Run();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace War_ConsoleApp
{
    class WarMain
    {
        static void Main(string[] args)
        {
            int winHeight = Console.LargestWindowHeight;
            int winWidth = Console.LargestWindowWidth;

            double wDouble =  winWidth*.5;
            double hDouble = winHeight * .75;
            Console.WindowWidth = (int) wDouble;
            Console.WindowHeight = (int) hDouble;

            WarGame game = new WarGame();
            bool val = game.promptWarGame();

            while (val)
            {
                game.newGame(true);
                val = game.runGame();
            }

            if (!val)
            {
                Environment.Exit(0);
            }

            Console.Read();
        }
    }
}

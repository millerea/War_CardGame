using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Begin War Main...\n");

            //TODO define controller / game play
            //TODO - how to deal
            //TODO - how to play 1v1
            //TODO - how to compare drawn cards?
            //TODO what else are you missing?

            WarGame game = new WarGame();
            //bool val = game.PromptWarGame(); //TODO put this back in for prompting
            bool val = true;
            if (val == true)//TODO put back in for prompting
            {
                game.NewGame();
            }
            else
            {
                //No game END / ESC
            }


            Console.Read();
        }
    }
}

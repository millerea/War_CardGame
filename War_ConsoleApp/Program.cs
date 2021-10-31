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

            //TODO define a deck of cards
            //TODO define controller / game play
            //TODO - how to shuffle
            //TODO - how to random draw
            //TODO - how to deal
            //TODO - how to play 1v1
            //TODO - how to compare drawn cards?
            //TODO what else are you missing?

            Deck deck = new Deck();
            deck.FillDeck();
            deck.PrintDeck();

            Console.Read();
        }
    }
}

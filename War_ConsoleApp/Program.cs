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
            //TODO - how to shuffle
            //TODO - how to random draw
            //TODO - how to deal
            //TODO - how to play 1v1
            //TODO - how to compare drawn cards?
            //TODO what else are you missing?


            //TODO define a deck of cards
            Deck deck = new Deck();
            deck.FillDeck();
            deck.PrintDeck(); //Deck prints and have a full deck

            //TODO Shuffle deck




            Console.Read();
        }
    }
}

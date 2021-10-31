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


            //TODO define a deck of cards
            Deck deck = new Deck();
            deck.FillDeck();
            //deck.PrintDeck(); //Deck prints and have a full deck

            //TODO Shuffle deck
            deck.ShuffleDeck(deck.GetDeck(), 52);
            //deck.PrintDeck();








            Console.Read();
        }
    }
}

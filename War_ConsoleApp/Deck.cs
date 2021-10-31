using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace War_ConsoleApp
{
    public class Deck
    {        
        public List<Card> GetDeck()
        {
            return mCards;
        }

        public void FillDeck()
        {
            //Can use a single loop utilising the mod operator % and Math.Floor
            //Using divition based on 13 cards in a suited
            for (int i = 0; i < 52; i++)
            {
                War_ConsoleApp.Card.Suite suite = (War_ConsoleApp.Card.Suite)(Math.Floor((decimal)i / 13));
                //Add 2 to value as a cards start a 2
                int val = i % 13 + 2;
                mCards.Add(new Card(val, suite));
            }
        }

        public void PrintDeck()
        {
            foreach (Card card in this.mCards)
            {
                Console.WriteLine(Name(card.mValue, card.mSuite));
            }
        }

        public List<Card> ShuffleDeck(List<Card> aDeck, int n)
        {
            DateTime dateTime = DateTime.Now;
            int timeMsSinceMidnight = (int)dateTime.TimeOfDay.TotalMilliseconds;
            Random rand = new Random(timeMsSinceMidnight);
   
            for (int l = n-1; l > 0; l--)
            {
                int j = rand.Next(0, l + 1);

                Card tE = aDeck[l];
                aDeck[l] = aDeck[j];
                aDeck[j] = tE;
            }

            return aDeck;
        }


        private string Name(int aValue, War_ConsoleApp.Card.Suite aSuite)
        {
            return NamedValue(aValue).ToString() + " of " + aSuite.ToString();
        }

        //Used to get full name, also usefull 
        //if you want to just get the named value
        private string NamedValue(int aValue)
        {
            string name = string.Empty;
            switch (aValue)
            {
                case (14):
                    name = "Ace";
                    break;
                case (13):
                    name = "King";
                    break;
                case (12):
                    name = "Queen";
                    break;
                case (11):
                    name = "Jack";
                    break;
                default:
                    name = aValue.ToString();
                    break;
            }
            return name;
        }

        private List<Card> mCards = new List<Card>();

    }
}
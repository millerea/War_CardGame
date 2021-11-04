using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace War_ConsoleApp
{
    public class Deck
    {        
        public List<Card> getDeck()
        {
            return mCards;
        }

        public void fillDeck()
        {
            for (int i = 0; i < 52; i++)
            {
                War_ConsoleApp.Card.Suite suite = (War_ConsoleApp.Card.Suite)(Math.Floor((decimal)i / 13));
                int val = i % 13 + 2;
                mCards.Add(new Card(val, suite));
            }
        }

        public void printDeck()
        {
            foreach (Card card in this.mCards)
            {
                Console.WriteLine(name(card.mValue, card.mSuite));
            }
        }

        public List<Card> shuffleDeck(List<Card> aDeck, int n)
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

        public Queue<Card>[] splitDeck(List<Card> aDeck)
        {
            Queue<Card>[] twoListArray = new Queue<Card>[2];
            twoListArray[0] = new Queue<Card>();
            twoListArray[1] = new Queue<Card>();
            
            for (int i = 0; i < 52; i++)
            {
                if (i % 2 == 0)
                {
                    twoListArray[0].Enqueue(aDeck[i]);
                }
                else
                {
                    twoListArray[1].Enqueue(aDeck[i]);
                }
            }
            return twoListArray;
        }




        public string name(int aValue, War_ConsoleApp.Card.Suite aSuite)
        {
            return namedValue(aValue).ToString() + " of " + aSuite.ToString();
        }

        private string namedValue(int aValue)
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
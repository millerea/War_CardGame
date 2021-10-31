using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_ConsoleApp
{
    public class Card
    {
        public enum Suite
        {
            Hearts = 0,
            Diamonds = 1,
            Clubs = 2,
            Spades = 3
        }

        public int mValue
        {
            get;
            set;
        }

        public Suite mSuite
        {
            get;
            set;
        }

        public Card(int aValue, Suite aSuite)
        {
            this.mValue = aValue;
            this.mSuite = aSuite;
        }
    }
}

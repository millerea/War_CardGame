using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_ConsoleApp
{
    public class Player
    {
        public Player(string aPlayerName, Queue<Card> aPlayerHand)
        {
            mPlayerName = aPlayerName;
            mPlayerHand = aPlayerHand;
            mWarsWon = 0;
            mIsInitialized = true;
        }

        public String mPlayerName
        {
            get;
            set;
        }
        public Queue<Card> mPlayerHand
        {
            get;
            set;
        }

        public int mWarsWon
        {
            get;
            set;
        }
        bool mIsInitialized = false;
    }
}

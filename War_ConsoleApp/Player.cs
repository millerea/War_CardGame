﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace War_ConsoleApp
{
    public class Player
    {
        public Player(string aPlayerName, Queue<Card> aPlayerHand)
        {
            this.mPlayerName = aPlayerName;
            this.mPlayerHand = aPlayerHand;
            this.mIsInitialized = false;
            this.mWarsWon = 0;
        }

        public string getPlayerName()
        {
            return mPlayerName;
        }
        public void setPlayerName(string aPlayerName)
        {
            mPlayerName = aPlayerName;
        }

        public Queue<Card> playerHand
        {
            get { return mPlayerHand; }
            set
            {
                mPlayerHand = value;   
            }
        }

        public int warsWon
        {
            get{return mWarsWon;}
            set
            {
                mWarsWon = value;
            }
        }

        public bool init
        {
            get { return mIsInitialized; }
            set
            {
                mIsInitialized = value;
            }
        }

        private Queue<Card> mPlayerHand;
        private string mPlayerName;
        private int mWarsWon;
        private bool mIsInitialized;

    }
}

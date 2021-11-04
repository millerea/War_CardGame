using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace War_ConsoleApp
{
    class WarGame
    {
        private bool playAgain()
        {
            bool response = false;

            string typedLine = Console.ReadLine().ToLower();

            while (string.IsNullOrEmpty(typedLine) ||
                !(typedLine.ToLower().Equals("yes") || typedLine.ToLower().Equals("no")))
            {
                Console.WriteLine("       Invalid response: Do you want to play 52 card war, please type Yes/No?");
                Console.WriteLine("       Yes/No?\n       ");
                typedLine = Console.ReadLine().ToLower();
            }

            if (typedLine.ToLower().Equals("yes"))
            {
                response = true;
                this.slowPrint(100, "\n       LAUNCHING GLOBAL THERMONEUCLAR WAR.......\n");
                this.slowPrint(50, "\n        Shuffling and Dealing Cards.......\n");
                Thread.Sleep(2000);
            }
            else
            {
                Console.Clear();
                Console.WriteLine("\n\n       You declined to play war.\n");
                this.slowPrint(100, "       A STRANGE GAME.  THE ONLY WINNING MOVE IS NOT TO PLAY.\n");
                this.slowPrint(100, "       HOW ABOUT A NICE GAME OF CHESS?\n");
                this.slowPrint(100, "       Have a nice day!\n\n");
                Thread.Sleep(2000);
                Environment.Exit(0);
            }
            return response;
        }

        public bool promptWarGame()
        {
            bool response = false;
            List<string> print = new List<string>();
            print.Add("");
            print.Add("     ***********************************************");
            print.Add("     **                                           **");
            print.Add("     **    Wargames inspired 52 Card WarGame      **");
            print.Add("     **                                           **");
            print.Add("     **                                           **");
            print.Add("     ** Written By: Eric Miller (Waynesville, OH) **");
            print.Add("     ** Delivered to TruPlay Games, Nov 2021      **");
            print.Add("     **                                           **");
            print.Add("     **                                           **");
            print.Add("     ***********************************************");
            print.Add("\n");

            foreach (string str in print)
            {
                Console.WriteLine(str);

            }

            this.slowPrint(100, "       GREETINGS PROFESSOR FALKEN.\n");
            this.slowPrint(100, "       HOW ARE YOU FEELING TODAY?\n");
            this.slowPrint(100, "       SHALL WE PLAY A GAME?\n");
            this.slowPrint(100, "       HOW ABOUT WE PLAY 52 CARD WAR?\n");
            this.slowPrint(100, "       ENTER Yes/No?\n       ");

            string typedLine = Console.ReadLine().ToLower();

            while (string.IsNullOrEmpty(typedLine) || 
                !(typedLine.ToLower().Equals("yes") || typedLine.ToLower().Equals("no")))
            {
                Console.WriteLine("       Invalid response: Do you want to play 52 card war, please type Yes/No?");
                Console.WriteLine("       Yes/No?       ");
                typedLine = Console.ReadLine().ToLower();
            }

            if (typedLine.ToLower().Equals("yes"))
            {
                response = true;
                this.slowPrint(100, "\n       LAUNCHING GLOBAL THERMONEUCLAR WAR.......\n");
                this.slowPrint(50, "\n       Shuffling and Dealing Cards.......\n");
                Thread.Sleep(2000);
            }
            else
            {
                Console.WriteLine("       You declined to play war.\n");
                this.slowPrint(150, "       A STRANGE GAME.  THE ONLY WINNING MOVE IS NOT TO PLAY.\n");
                this.slowPrint(150, "       HOW ABOUT A NICE GAME OF CHESS?\n");
                Console.WriteLine("       Have a nice day!");
                Thread.Sleep(2000);
            }
            return response;
        }

        public void newGame(bool reset)
        {
            if (reset)
            {
                mPlayerOne = null;
                mPlayerTwo = null;
                mDeck = null;
                mResults = new List<string>();
                mOutput = null;
                mOutString = "";
                mSimulate = false;
                mre = new ManualResetEvent(false);
            }

            this.mOutput = new OutputText();
            mOutString += "Starting new WarGame...\n";
            this.mDeck = new Deck();
            this.mDeck.fillDeck();
            List<Card> cardDeck = this.mDeck.getDeck();
            this.mDeck.shuffleDeck(cardDeck, 52);
            Queue<Card>[] aDealtDeck = this.mDeck.splitDeck(cardDeck);
            mPlayerOne = new Player("Player", aDealtDeck[0]);
            mPlayerTwo = new Player("Joshua", aDealtDeck[1]);
        }

        public bool runGame()
        {
            Console.Clear();
            int cycles = 0;

            while (cycles < 1000 && (mPlayerOne.playerHand.Count() > 0) && (mPlayerTwo.playerHand.Count() > 0) && 
                mPlayerOne.warsWon < 3 && mPlayerTwo.warsWon < 3)
            {
                List<string> resultsReturned = runSingleRound(mPlayerOne.playerHand, mPlayerTwo.playerHand);

                if (resultsReturned != null)
                {
                    mOutString+=resultsReturned.ElementAt(0) + " WON ," + resultsReturned.ElementAt(1) + "\n";
                }
                if (!mSimulate)
                {
                    // Start a thread that waits on the ManualResetEvent.
                    Thread t5 = new Thread(threadProc);
                    t5.Name = "Thread_5";
                    t5.Start();

                    string input = Console.ReadLine();

                    if (input.ToLower().Equals("s"))
                    {
                        mSimulate = true;
                    }
                }                
                Console.Clear();
                cycles++;
            }
            if (mPlayerOne.warsWon == 3)
            {
                mOutString += "\n";
                mOutString += "PLAYER WINS, 3 Wars Won!!!\n";
                mOutString += "Player Card count: " + mPlayerOne.playerHand.Count.ToString() + "\n";
                mOutString += "Joshua Card count: " + mPlayerTwo.playerHand.Count.ToString() + "\n";

            }
            else if (mPlayerTwo.warsWon == 3)
            {
                mOutString += "\n";
                mOutString += "Joshua WINS, 3 Wars Won!!!\n";
                mOutString += "Joshua Card count: " + mPlayerTwo.playerHand.Count.ToString() + "\n";
                mOutString += "Player Card count: " + mPlayerOne.playerHand.Count.ToString() + "\n";
                mOutString += "\nPlayer loses, better luck next time...\n";
            }
            else if (cycles >= 1000)
            {
                mOutString += "\n";
                mOutString += "       We have a draw, exceeded 1000 cycle hands of war with Joshua on the WOPR";
            }

            mOutString += "\n       Would you like to play again?";
            mOutput.writeLine(mOutString);
            mOutput.endLine();
            return playAgain();
        }

        private List<string> runSingleRound(Queue<Card> playerQueue, Queue<Card> computerQueue)
        {
            mResults.Clear();
            //results[0] = string winning player name
            //results[1] = winning condition 

            Card playerOneCard = playerQueue.Dequeue();
            Card computerCard = computerQueue.Dequeue();
            
            if (playerOneCard.mValue > computerCard.mValue)
            {
                string condition = "Player " + mDeck.name(playerOneCard.mValue, playerOneCard.mSuite);

                condition += " Beats Joshua ";
                condition += mDeck.name(computerCard.mValue, computerCard.mSuite);
                mResults.Insert(0, mPlayerOne.getPlayerName());
                mResults.Insert(1, condition);
                playerQueue.Enqueue(playerOneCard);
                playerQueue.Enqueue(computerCard);
                mOutput.cardPrintConsoleOutput(playerOneCard, computerCard, mResults);
                mOutput.writeCurrentMetrics(mPlayerOne, mPlayerTwo);
            }
            else if (playerOneCard.mValue < computerCard.mValue)
            {
                string condition = "Joshua " + mDeck.name(computerCard.mValue, computerCard.mSuite);

                condition += " Beats Player ";
                condition += mDeck.name(playerOneCard.mValue, playerOneCard.mSuite);
                mResults.Insert(0, mPlayerTwo.getPlayerName());
                mResults.Insert(1, condition);
                computerQueue.Enqueue(playerOneCard);
                computerQueue.Enqueue(computerCard);
                mOutput.cardPrintConsoleOutput(playerOneCard, computerCard, mResults);
                mOutput.writeCurrentMetrics(mPlayerOne, mPlayerTwo);
            }
            else if (playerOneCard.mValue == computerCard.mValue)
            {
                Console.Clear();

                string condition = "Player " + mDeck.name(playerOneCard.mValue, playerOneCard.mSuite);

                condition += " Ties Joshua ";
                condition += mDeck.name(computerCard.mValue, computerCard.mSuite);

                mResults.Insert(0, "WAR");
                mResults.Insert(1, condition);
                mOutput.cardPrintConsoleOutput(playerOneCard, computerCard, mResults);
                Console.WriteLine("                            *** WAR DECLARED ***");

                mResults = adjudicateWar(playerQueue, computerQueue);

                if (mResults.ElementAt(0).Contains("Joshua"))
                {
                    computerQueue.Enqueue(playerOneCard);
                    computerQueue.Enqueue(computerCard);
                }
                else
                {
                    playerQueue.Enqueue(playerOneCard);
                    playerQueue.Enqueue(computerCard);
                }
                mOutput.writeCurrentMetrics(mPlayerOne, mPlayerTwo);
            }
            return mResults;
        }

        private List<string> adjudicateWar(Queue<Card> aPlayerQueue, Queue<Card> aComputerQueue)
        {
            Queue<Card> sixCards = new Queue<Card>();
            Card playerCard = null;
            bool playerOut = false;
            Card computerCard = null;
            bool computerOut = false;
            bool exit = false;

            do
            {
                if (aPlayerQueue.Count() > 4)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        sixCards.Enqueue(aPlayerQueue.Dequeue());
                    }
                }
                if (aComputerQueue.Count() > 4)
                {
                    for (int c = 0; c < 3; c++)
                    {
                        sixCards.Enqueue(aComputerQueue.Dequeue());
                    }
                }

                playerCard = aPlayerQueue.Dequeue();
                computerCard = aComputerQueue.Dequeue();

                if (aPlayerQueue.Count == 0)
                {
                    playerOut = true;
                    exit = true;
                }
                if (aComputerQueue.Count == 0)
                {
                    computerOut = true;
                    exit = true;
                }
                if (playerCard.mValue > computerCard.mValue)
                {
                    sixCards.Enqueue(playerCard);
                    sixCards.Enqueue(computerCard);
                    exit = true;

                    if (!playerOut && !computerOut)
                    {
                        mResults.Insert(0, "Player");
                        mResults.Insert(1, "Player " + mDeck.name(playerCard.mValue, playerCard.mSuite) +
                                        " beats Joshua " + mDeck.name(computerCard.mValue, computerCard.mSuite));
                    }
                    else if (computerOut)
                    {
                        mResults.Insert(0, "Player Wins Game!!!");
                        mResults.Insert(1, "Player " + mDeck.name(playerCard.mValue, playerCard.mSuite) +
                                        " beats Joshua " + mDeck.name(computerCard.mValue, computerCard.mSuite));
                    }

                    for (int z = sixCards.Count; z > 0 ; z--)
                    {
                        aPlayerQueue.Enqueue(sixCards.Dequeue());
                    }

                    this.mPlayerOne.warsWon++;
                }
                else if (playerCard.mValue < computerCard.mValue)
                {
                    sixCards.Enqueue(playerCard);
                    sixCards.Enqueue(computerCard);
                    exit = true;

                    if (!playerOut && !computerOut)
                    {
                        mResults.Insert(0, "Joshua");
                        mResults.Insert(1, "Joshua " + mDeck.name(computerCard.mValue, computerCard.mSuite) +
                                        " beats Player " + mDeck.name(playerCard.mValue, playerCard.mSuite));
                    }
                    else if (playerOut)
                    {
                        mResults.Insert(0, "Joshua Wins Game!!!");
                        mResults.Insert(1, "Joshua " + mDeck.name(computerCard.mValue, computerCard.mSuite) +
                                        " beats Player " + mDeck.name(playerCard.mValue, playerCard.mSuite));
                    }

                    for (int z = sixCards.Count; z > 0; z--)
                    {
                        aComputerQueue.Enqueue(sixCards.Dequeue());
                    }

                    this.mPlayerTwo.warsWon++;
                }
                else if (playerCard.mValue == computerCard.mValue)
                {
                    sixCards.Enqueue(playerCard);
                    sixCards.Enqueue(computerCard);

                    string condition = "Player " + mDeck.name(playerCard.mValue, playerCard.mSuite);
                    condition += " Ties Joshua ";
                    condition += mDeck.name(computerCard.mValue, computerCard.mSuite);

                    mResults.Insert(0, "WAR");
                    mResults.Insert(1, condition);
                    mOutput.cardPrintConsoleOutput(playerCard, computerCard, mResults);
                    Console.WriteLine("                            *** WAR DECLARED ***");
                }

                mOutput.cardPrintConsoleOutput(playerCard, computerCard, mResults);

            } while (!exit && playerCard.mValue == computerCard.mValue);

            return mResults;
        }

        private static void threadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine("\nReady for the next round?  Press ENTER for the next round...");
            Console.WriteLine("Ready to simulate the end?  Type S and Press ENTER");

            mre.WaitOne();

        }


        private void slowPrint(int aDelay, string str)
        {
            foreach (char c in str)
            {
                Console.Write(c);
                Thread.Sleep(aDelay);
            }
        }

        private Player mPlayerOne = null;
        private Player mPlayerTwo = null;
        private Deck mDeck = null;
        private List<string> mResults = new List<string>();

        private OutputText mOutput;
        private string mOutString;
        private bool mSimulate = false;
        
        private static ManualResetEvent mre = new ManualResetEvent(false);
    }
}

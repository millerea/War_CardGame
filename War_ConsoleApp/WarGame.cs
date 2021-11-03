using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Timers;
using System.Threading;

namespace War_ConsoleApp
{
    class WarGame
    {
        public bool PromptWarGame()
        {
            bool response = false;

            SlowPrint(100, "\n  GREETINGS PROFESSOR FALKEN.\n");
            SlowPrint(100, "  HOW ARE YOU FEELING TODAY?\n");
            SlowPrint(100, "  WOULD YOU LIKE TO PLAY A GAME?\n");
            SlowPrint(100, "  HOW ABOUT WE PLAY 52 CARD WAR?\n");
            SlowPrint(200, "  Yes/No?\n");

            string typedLine = Console.ReadLine().ToLower();

            while (string.IsNullOrEmpty(typedLine) || 
                !(typedLine.ToLower().Equals("yes") || typedLine.ToLower().Equals("no")))
            {
                Console.WriteLine("Invalid response: Do you want to play 52 card war, please type Yes/No?");
                Console.WriteLine("Yes/No? ");
                typedLine = Console.ReadLine().ToLower();
            }

            if (typedLine.ToLower().Equals("yes"))
            {
                response = true;
                Console.WriteLine("Launching Global Thermo Nuclear WAR");
            }
            else
            {
                Console.WriteLine("You declined to play war.\n");

                SlowPrint(150, "A STRANGE GAME.  THE ONLY WINNING MOVE IS NOT TO PLAY.\n");
                SlowPrint(150, "HOW ABOUT A NICE GAME OF CHESS?\n");

                Console.WriteLine("Have a nice day!");
            }

            return response;
        }

        public void StartGame()
        {
            this.RunGame(mPlayerOne, mPlayerTwo);
        }

        public void NewGame()
        {
            //Populate players
            //Populate deck
            //Populate start
            //Populate actions

            this.mOutput = new OutputText();
            mOutString += "Starting new WarGame...\n";
            
            //TODO define a deck of cards
            this.mDeck = new Deck();
            this.mDeck.FillDeck();

            //deck.PrintDeck(); //Deck prints and have a full deck

            List<Card> cardDeck = this.mDeck.GetDeck();
            //TODO Shuffle deck
            this.mDeck.ShuffleDeck(cardDeck, 52);
            //deck.PrintDeck();

            //Converting from a list/deck of cards into seperate queues for players
            Queue<Card>[] aDealtDeck = this.mDeck.SplitDeck(cardDeck);

            //Begin game with two players, Player 1 is Person, Player 2 is Joshua
            mPlayerOne = new Player("Player", aDealtDeck[0]);
            mPlayerTwo = new Player("Joshua", aDealtDeck[1]);

            mTimer.Interval = 1000;
            mTimer.Elapsed += myTimer_Tick;
            mTimer.AutoReset = true;
            mTimer.Enabled = true;
            mTimer.Start();
        }


        private void RunGame(Player aPlayerOne, Player aPlayerTwo)
        {
            Console.Clear();
            int cycles = 0;
            //Per https://www.thesprucecrafts.com/war-card-game-rules-411145
            //Winning
            //The first player to win the entire deck of cards is the winner. 
            //Alternatively, because winning the entire deck can take a long time, 
            //the first player to win three wars is the winner. 

            while (cycles < 1000 && (aPlayerOne.playerHand.Count() > 0) && (aPlayerTwo.playerHand.Count() > 0) && 
                aPlayerOne.warsWon < 3 && aPlayerTwo.warsWon < 3)
            {
                List<string> resultsReturned = RunSingleRound(aPlayerOne.playerHand, aPlayerTwo.playerHand);

                if (resultsReturned != null)
                {
                    mOutString+=resultsReturned.ElementAt(0) + " WON ," + resultsReturned.ElementAt(1) + "\n";
                }

                if (!mSimulate)
                {
                    // Start a thread that waits on the ManualResetEvent.
                    Thread t5 = new Thread(ThreadProc);
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

            if (aPlayerOne.warsWon == 3)
            {
                mOutString += "\n";
                mOutString += "PLAYER WINS, 3 Wars Won!!!\n";
                mOutString += "Player Card count: " + aPlayerOne.playerHand.Count.ToString() + "\n";
                mOutString += "Joshua Card count: " + aPlayerTwo.playerHand.Count.ToString() + "\n";

            }
            else if (aPlayerTwo.warsWon == 3)
            {
                mOutString += "\n";
                mOutString += "Joshua WINS, 3 Wars Won!!!\n";
                mOutString += "Joshua Card count: " + aPlayerTwo.playerHand.Count.ToString() + "\n";
                mOutString += "Player Card count: " + aPlayerOne.playerHand.Count.ToString() + "\n";
            }

            else if (cycles >= 1000)
            {
                mOutString += "\n";
                mOutString += "We have a draw, exceeded 1000 cycle hands of war with Joshua on the WOPR";
            }
            mOutput.WriteLine(mOutString);
            mOutput.EndLine();
        }

        private List<string> RunSingleRound(Queue<Card> playerQueue, Queue<Card> computerQueue)
        {
            mResults.Clear();
            //results[0] = string winning player name
            //results[1] = winning condition 

            Card playerOneCard = playerQueue.Dequeue();
            Card computerCard = computerQueue.Dequeue();
            
            if (playerOneCard.mValue > computerCard.mValue)
            {
                string condition = "Player " + mDeck.Name(playerOneCard.mValue, playerOneCard.mSuite);
                condition += " Beats Joshua ";
                condition += mDeck.Name(computerCard.mValue, computerCard.mSuite);

                mResults.Insert(0, mPlayerOne.getPlayerName());
                mResults.Insert(1, condition);

                playerQueue.Enqueue(playerOneCard);
                playerQueue.Enqueue(computerCard);

                mOutput.CardPrintConsoleOutput(playerOneCard, computerCard, mResults);
                mOutput.WriteCurrentMetrics(mPlayerOne, mPlayerTwo);
            }
            else if (playerOneCard.mValue < computerCard.mValue)
            {
                string condition = "Joshua " + mDeck.Name(computerCard.mValue, computerCard.mSuite);
                condition += " Beats Player ";
                condition += mDeck.Name(playerOneCard.mValue, playerOneCard.mSuite);

                mResults.Insert(0, mPlayerTwo.getPlayerName());
                mResults.Insert(1, condition);

                computerQueue.Enqueue(playerOneCard);
                computerQueue.Enqueue(computerCard);

                mOutput.CardPrintConsoleOutput(playerOneCard, computerCard, mResults);
                mOutput.WriteCurrentMetrics(mPlayerOne, mPlayerTwo);
            }
            else if (playerOneCard.mValue == computerCard.mValue)
            {
                Console.Clear();
                //War condition
                

                string condition = "Player " + mDeck.Name(playerOneCard.mValue, playerOneCard.mSuite);
                condition += " Ties Joshua ";
                condition += mDeck.Name(computerCard.mValue, computerCard.mSuite);

                mResults.Insert(0, "WAR");
                mResults.Insert(1, condition);
                mOutput.CardPrintConsoleOutput(playerOneCard, computerCard, mResults);
                Console.WriteLine("                            *** WAR DECLARED ***");

                mResults = AdjudicateWar(playerQueue, computerQueue);

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

                mOutput.WriteCurrentMetrics(mPlayerOne, mPlayerTwo);
            }

            return mResults;
        }


        private List<string> AdjudicateWar(Queue<Card> aPlayerQueue, Queue<Card> aComputerQueue)
        {
            //Tie need to have a War
            //3-Cards per player face down (wager)
            //4th card face up 

            //4th card winner takes all 10 cards
            //two original tieing + 2players*(3cards) + 2 show cards
            //2 + 6 + 2 = 10 cards to move

            //If 4th turn cards are tied then face up next cards until winner

            //If cards are low and can't face down correct number then the last card up

            //TODO start here by preparing war


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
                        mResults.Insert(1, "Player " + mDeck.Name(playerCard.mValue, playerCard.mSuite) +
                                        " beats Joshua " + mDeck.Name(computerCard.mValue, computerCard.mSuite));
                    }
                    else if (computerOut)
                    {
                        mResults.Insert(0, "Player Wins Game!!!");
                        mResults.Insert(1, "Player " + mDeck.Name(playerCard.mValue, playerCard.mSuite) +
                                        " beats Joshua " + mDeck.Name(computerCard.mValue, computerCard.mSuite));
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
                        mResults.Insert(1, "Joshua " + mDeck.Name(computerCard.mValue, computerCard.mSuite) +
                                        " beats Player " + mDeck.Name(playerCard.mValue, playerCard.mSuite));
                    }
                    else if (playerOut)
                    {
                        mResults.Insert(0, "Joshua Wins Game!!!");
                        mResults.Insert(1, "Joshua " + mDeck.Name(computerCard.mValue, computerCard.mSuite) +
                                        " beats Player " + mDeck.Name(playerCard.mValue, playerCard.mSuite));
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

                    string condition = "Player " + mDeck.Name(playerCard.mValue, playerCard.mSuite);
                    condition += " Ties Joshua ";
                    condition += mDeck.Name(computerCard.mValue, computerCard.mSuite);

                    mResults.Insert(0, "WAR");
                    mResults.Insert(1, condition);
                    mOutput.CardPrintConsoleOutput(playerCard, computerCard, mResults);
                    Console.WriteLine("                            *** WAR DECLARED ***");
                }

                mOutput.CardPrintConsoleOutput(playerCard, computerCard, mResults);

            } while (!exit && playerCard.mValue == computerCard.mValue);

            return mResults;
        }


        private static void ThreadProc()
        {
            string name = Thread.CurrentThread.Name;

            Console.WriteLine("\nReady for the next round?  Press ENTER for the next round...");
            Console.WriteLine("Ready to simulate the end?  Type S and Press ENTER");

            mre.WaitOne();

        }

        private void myTimer_Tick(object sender, EventArgs e)
        {
            if (mTime > 0)
            {
                Console.WriteLine("War Game starting in: " + (mTime).ToString());
            }
            else
            {
                mTimer.Stop();
                mTimer.Close();
                this.StartGame();
            }

            mTime -= 1;
        }

        private static void SlowPrint(int aDelay, string str)
        {
            foreach (char c in str)
            {
                Console.Write(c);
                Thread.Sleep(aDelay);
            }
        }

        private int mTime = 5;
        private System.Timers.Timer mTimer = new System.Timers.Timer();
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

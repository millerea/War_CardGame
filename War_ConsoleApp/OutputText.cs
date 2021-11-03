using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;


namespace War_ConsoleApp
{
    class OutputText
    {
        public void StartWindowSize()
        {
            int winHeight = Console.LargestWindowHeight; //64
            int winWidth = Console.LargestWindowWidth;  //160

        }

        public void CardPrintConsoleOutput(Card aPlayerCard, Card aComputerCard, List<string> aResults)
        {
            //Print player
            int pLength = aPlayerCard.mValue.ToString().Length;
            pLength += aPlayerCard.mSuite.ToString().Length;
            string pString = "        --P:          --                --J:           --";
            string empty = pString.Substring(9, pLength);
            pString = pString.Replace(empty, "-P:" + aPlayerCard.mValue.ToString() + "_" + aPlayerCard.mSuite.ToString());

            //Print Joshua
            pLength = aComputerCard.mValue.ToString().Length;
            pLength += aComputerCard.mSuite.ToString().Length;

            empty = pString.Substring(pString.IndexOf("J"), pLength);
            pString = pString.Replace(empty, "J:" + aComputerCard.mValue.ToString() + "_" + aComputerCard.mSuite.ToString());

            List<string> output = new List<string>();
            output.Add("\n");
            output.Add("                           WOPR WarGame                         ");
            output.Add("----------------------------------------------------------------");
            output.Add("                           Player vs. Joshua                    ");
            output.Add("        --------------------                --------------------");
            output.Add("        --                --                --                --");
            output.Add(pString);
            output.Add("        --                --                --                --");
            output.Add("        --                --                --                --");
            output.Add("        --                --                --                --");
            output.Add("        --                --                --                --");
            output.Add("        --                --                --                --");
            output.Add("        --                --                --                --");
            output.Add("        --                --                --                --");
            output.Add("        --------------------                --------------------");
            output.Add("            Player                              Joshua          ");
            output.Add("\n");
            output.Add("                 "+aResults.ElementAt(1)+"\n");

            foreach (string str in output)
            {
                Console.WriteLine(str);
            }
        }

        public void WriteCurrentMetrics(Player aPlayer, Player aComputer)
        {
            Console.WriteLine("                 Player cards remaining: " + aPlayer.playerHand.Count);
            Console.WriteLine("                 Player has Won " + aPlayer.warsWon.ToString() + " War\n");
            Console.WriteLine("                 Joshua cards remaining: " + aComputer.playerHand.Count);
            Console.WriteLine("                 Joshua has Won " + aComputer.warsWon.ToString() + " War");
        }


        public void WriteLine(string aLine)
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(aLine);
            Console.WriteLine(aLine);
        }

        public void EndLine()
        {
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Thank you for playing...");
            sw.Close();
        }

        private FileStream fs = new FileStream("./Output/WarGame_Output.txt", FileMode.Append);
    }
}

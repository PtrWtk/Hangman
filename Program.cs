/*I'm sorry for all the code repetition and lack of objects and metods to solve this and shorten the code in this patchwork 
 * but I haven't written anything in a while and this is my first time with c# so that's the best I could do on such short notice
 * in between my current job and everyday responsibilities. Tried to comment what seemed less intuitive or just to make it more transparent though. :) */


using System;
using System.IO;
namespace Hangman_Simple
{
    class Program
    {
        static void Main()
        {
            bool playAgain = true;
            while (playAgain)
            {
                NewGame();
                Console.Write(" Want to play again?    Y/N: ");
                char tempC = (char)Console.Read();
                if (tempC != 'y')
                    playAgain = false;
            }

            Console.ReadLine();

        }

        static void NewGame()
        {
            DateTime startT = DateTime.Now;
            int cia = 26;//Number of letter in alphabet
            int guessCount = 0; //wrong guesses counter
            int overallGuessCount = 0;
            int guessLimit = 10;//change limit to 10
            bool guessed = false;//used to track if player guessed the word
            bool haveGuesses = true;
            char[] guessesSoFar = new char[cia]; //stores used letters

            //Drawing the capitol
            Random random = new Random();
            int lineNr = random.Next(1, 183);

            //grabbing the capitol and country from file and setting variables
            string[] lines = File.ReadAllLines("countries_and_capitals.txt"); //reading whole file
            string line = lines[lineNr]; //saving the capitol and country
            int separatorPos = line.IndexOf("|"); // finding separator position
            string sCapitol = line.Substring(separatorPos + 2).ToLower(); //saving capitol name and removing upper case for ease of use
            string sCountry = line.Substring(0, separatorPos - 1); //saving country
            char[] sCapitolArray = new char[line.Length]; //redundant (could have been solved differently) but left to limit reediting the code
            char[] sGuessArray = new char[line.Length]; //stores 
            int sCapLength = sCapitol.Length;

            //Setting up main variable for guesswork
            for (int i = 0; i < sCapLength; i++)
            {
                sCapitolArray[i] = sCapitol[i];
                sGuessArray[i] = '_';
            }

            Console.WriteLine("Welcome to Hangman");
            Console.WriteLine("Rules are simple:");
            Console.WriteLine("1. You are guessing a capitol name of a country.");
            Console.WriteLine("2. You lose when you make " + guessLimit + " mystakes.");
            Console.WriteLine("3. You can attempt to guess the whole word at the risk of losing 2 chances.");
            Console.WriteLine("Good luck!");
            Console.WriteLine("(press ENTER when ready)");
            
            while (!guessed && haveGuesses)
            {
                Console.ReadLine();
                Console.Clear();

                //deciding on the gameplay
                Console.WriteLine("You have " + (guessLimit - guessCount) + " attempts left.");
                for (int i = 0; i < sGuessArray.Length; i++)
                    Console.Write(sGuessArray[i] + " ");
                Console.WriteLine("\n\nIf you want to:\n");
                Console.WriteLine("# Press 1 and ENTER to guess a SINGLE LETTER");
                Console.WriteLine("# Press 2 and ENTER to try to guess the WHOLE WORD ");

                int gi = 0;
                string tempString = Console.ReadLine();
                if (tempString == "1")
                    gi = 1;
                else
                    if (tempString == "2")
                    gi = 2;


                switch (gi)
                {
                    case 1://single letter routine
                        //generating the screen
                        Console.Clear();
                        Console.WriteLine("You have " + (guessLimit - guessCount) + " attempts left, your guesses so far are:");
                        for (int i = 0; i < overallGuessCount; i++)
                        {
                            Console.Write(guessesSoFar[i] + " ");
                        }
                        Console.WriteLine("\n\nand the word is:");
                        for (int i = 0; i < sGuessArray.Length; i++)
                            Console.Write(sGuessArray[i] + " ");

                        //reading the guess
                        Console.WriteLine("\n\nPlease input your letter: ");
                        string tempStg = Console.ReadLine();
                        char tempChar = tempStg[0];

                        
                        
                        //determining if correct letter and what to do next
                        if (!sCapitol.Contains(tempChar))
                        {
                            //checking if letter already used
                            bool alreadyUsed = false;
                            for (int i = 0; i < overallGuessCount; i++)
                            {
                                if (tempChar == guessesSoFar[i])
                                {
                                    alreadyUsed = true;
                                    break;
                                }
                            }

                            //guess repeating or incrementing guess count
                            if (alreadyUsed)
                            {
                                Console.Clear();
                                Console.WriteLine("Letter " + tempChar + " already used, try again. (press ENTER)");
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("Secret word does NOT contain letter " + tempChar + ". Try again. (press ENTER)");
                                guessesSoFar[overallGuessCount] = tempChar;
                                overallGuessCount++;
                                guessCount++;
                            }
                        }
                        else
                        {
                            //checking if letter already used
                            bool alreadyUsed = false;
                            for (int i = 0; i < overallGuessCount; i++)
                            {
                                if (tempChar == guessesSoFar[i])
                                {
                                    alreadyUsed = true;
                                    break;
                                }
                            }

                            //guess repeating or adding the letters to the guess
                            if (alreadyUsed)
                            {
                                Console.Clear();
                                Console.WriteLine("Letter " + tempChar + " already used, try again. (press ENTER)");
                            }
                            else
                            {
                                //adding letter to displayed letters
                                for (int i = 0; i < sCapLength; i++)
                                    if (sCapitolArray[i] == tempChar)
                                        sGuessArray[i] = tempChar;

                                //adding letter to already used characters
                                guessesSoFar[overallGuessCount] = tempChar;


                                //displaying new guesses
                                Console.Clear();
                                Console.Write("Good job! Secret word contains letter " + tempChar + " and now it's: ");
                                for (int i = 0; i < sCapLength; i++)
                                    Console.Write(sGuessArray[i] + " ");
                                Console.WriteLine("\n\n(press ENTER)");
                                
                                //increasing the counter of guesses
                                overallGuessCount++;
                                
                            }
                            for (int i = 0; i < sCapLength; i++)
                                if (sGuessArray[i] == sCapitolArray[i])
                                    guessed = true;
                                else
                                {
                                    guessed = false;
                                    break;
                                }

                        }
                        break;

                    case 2://whole word routine
                        Console.Clear();
                        for (int i = 0; i < sGuessArray.Length; i++)
                            Console.Write(sGuessArray[i] + " ");
                        Console.Write("\n\nInput the word : ");
                        string gWord = Console.ReadLine();

                        if (gWord != sCapitol)
                        {
                            Console.Clear();
                            Console.WriteLine("Wrong answer. Try again.");
                            Console.ReadLine();
                            guessCount = guessCount + 2;
                            break;
                        }
                        else
                        {
                            guessed = true;
                            break;
                        }

                    default:
                        break;

                }

                if (guessLimit <= guessCount)
                {
                    haveGuesses = false;
                }

            }//while end
            
            TimeSpan runTime = DateTime.Now.Subtract(startT);

            //determining the outcome
            if (guessed)
            {
                TimeSpan highScore = TimeSpan.Parse(File.ReadAllText("highscore.txt").Substring(0, 16));
                Console.Clear();
                Console.Write("Congrats! It took you " + runTime.Minutes + ":" + runTime.Seconds + "s to finish.");

                if (runTime < highScore)
                {
                    Console.Write(" You've also set a new record.\n\nPlease enter your name: ");
                    string name = Console.ReadLine();
                    File.WriteAllText("highscore.txt", (runTime.ToString() + " | " + name + " | " + sCapitol + " | " + overallGuessCount));
                }
                else
                {
                    Console.Write(" Current record is " + highScore.Minutes + ":" + highScore.Seconds + "s.");
                }
            }
            else
            {
                Console.Clear();
                Console.Write("You're out of guesses but you have a last chance. What's the capitol of " + sCountry + "? ");
                string tempString = Console.ReadLine().ToLower();
                if (tempString == sCapitol)
                {
                    TimeSpan highScore = TimeSpan.Parse(File.ReadAllText("highscore.txt").Substring(0, 16));
                    Console.Clear();
                    Console.Write("Congrats! It took you " + runTime.Minutes + ":" + runTime.Seconds + "s to finish.");

                    if (runTime < highScore)
                    {
                        Console.Write(" You've also set a new record.\n\nPlease enter your name: ");
                        string name = Console.ReadLine();
                        File.WriteAllText("highscore.txt", (runTime.ToString() + " | " + name + " | " + sCapitol + " | " + overallGuessCount));
                    }
                    else
                    {
                        Console.Write(" Current record is " + highScore.Minutes + ":" + highScore.Seconds + "s.");
                    }
                }
                else
                {
                    Console.WriteLine("\n\nIt took you " + runTime.Minutes + ":" + runTime.Seconds + "s to finish. Better luck next time!");
                }
            }
        }
    }
}
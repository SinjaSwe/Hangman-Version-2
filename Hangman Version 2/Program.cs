using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Hangman_Version_2
{
    class Program
    {
        //static int guessesAvailable;
        static int noOfGuesses = 0;
        static string wordToGuess = GetRandomWord();
        static List<char> guessedLetters = new List<char>();
        static bool won = false;

        static void Main(string[] args)
        {            
            StartGame();
        }
        static void StartGame()
        {
            wordToGuess = GetRandomWord();
                        
            bool keepLooping = true;

            while (keepLooping)
            {
                Console.WriteLine("Welcome to Hangman.");
                Console.WriteLine("------Options------");
                Console.WriteLine("1: Play");
                Console.WriteLine("2: Exit");

                double selection = AskForNumber();
                                
                switch (selection)
                {
                    case 1:
                        PlayHangman(); // Please 1 to play
                        break;

                    case 2:
                        keepLooping = false; //they wish to exit
                        Console.WriteLine("Thank you for playing Hangman!");
                        break;

                    default:
                        Console.WriteLine("not a valid selection. Please try again.");
                        break;
                }
            }
        }
        static void PlayHangman()
        {
            Console.WriteLine(" ________    ");
            Console.WriteLine(" |/   |      ");
            Console.WriteLine(" |   (_)     ");
            Console.WriteLine(" |   /|-     ");
            Console.WriteLine(" |    |      ");
            Console.WriteLine(" |   / }     ");
            Console.WriteLine(" |           ");
            Console.WriteLine(" |___        ");
            
            string guess;
            int guessesAvailable = 10;
            int noOfGuesses = 0;
            int remainingTries;
            remainingTries = guessesAvailable - noOfGuesses;

            Console.WriteLine(wordToGuess);//Only show when testing
                       
            // LIST TO CAPTURE GUESSED LETTERS
            List<char> guessedLettersList = new List<char>();
            
            // LOOP FOR PLAYING THE GAME
            while (remainingTries >= 1 && !won)
            {                
                remainingTries = guessesAvailable - noOfGuesses;
                Console.WriteLine($"No. of guesses remaining: {remainingTries}");                
                
                string wordToDisplay = DisplayWord(guessedLettersList, wordToGuess);
                Console.WriteLine(wordToDisplay);                

                if (!wordToDisplay.Contains("_"))
                {
                    won = false;
                    Console.WriteLine($"\nCongrats! You won! The hidden word was {wordToGuess}");
                    UserPlayAgain();
                }
                else if (remainingTries <= 0)
                {
                    Console.WriteLine($"You have no more guesses left. You lost! The word was {wordToGuess}");
                    UserPlayAgain();
                }
                else
                {               
                    guess = InputGuess("\nPlease guess a letter or a word\n"); // Call method to get user input and returns the guess value   

                    if (guess.Length > 1)
                    {
                        GuessWholeWord(guess, wordToGuess); //Method if user enters a word rather than a single letter. Written like this, calls the method (i.e runs the code within the method.
                        noOfGuesses++;
                    }
                    else
                    {
                        char guessAsChar;
                        guessAsChar = Convert.ToChar(guess);
                        noOfGuesses++;                        
                        GuessALetter(guessAsChar, guessedLettersList, wordToGuess);  // Method if user enters a single letter
                        /*if (guessedLettersList.Contains(guessAsChar))
                        {
                            noOfGuesses--; //THIS DOES NOT WORK
                        }
                        */
                    }
                }
            }
        }
        static string GetRandomWord() //METHOD: Random word generator
        {
            Random randomWord = new Random();
            string[] arrayOfWords = { "house", "robot", "garden", "children", "volvo", "nerd", "elephant", "school", "table" };
            string wordToGuess;
            {
                int index = randomWord.Next(arrayOfWords.Length);
                wordToGuess = arrayOfWords[index].ToString();
            }
            return wordToGuess;
        }
                
        static string InputGuess(string textToPrint) //Method for user input
        {
            Console.WriteLine(textToPrint);
            string guess = Console.ReadLine();
            return guess;
        }
        static void GuessWholeWord(string guess, string wordToGuess) // Definition. Need to pass two strings above
        {           
            if (guess == wordToGuess)
            {
                Console.WriteLine("\nAmazing job! Well done! You guessed the correct word."); //need to if else function here 
                UserPlayAgain();
            }
            else
            {
                Console.WriteLine("\nBoo hoo. Wrong! Better luck next time");
                return;
            }           
        }
        
        static void GuessALetter(char guessAsChar, List<char> guessedLettersList, string wordToGuess)
        {
            if (guessedLettersList.Contains(guessAsChar))
            {
                noOfGuesses--; 
                Console.WriteLine($"\nYou have already guessed {guessAsChar} dummy! Try a different letter.");
                return;
            }
            else if (wordToGuess.Contains(guessAsChar))            
            {                
                Console.WriteLine($"\nWell done! You guessed correctly. The hidden word contains {guessAsChar}.");
                guessedLettersList.Add(guessAsChar);
                Console.WriteLine("");
                //MISSING CHAR ARRAY CONTAINING GUESSES                
                //char [] rightLetters = new char [11];               
               
                return;                
            }
            else
            {
                Console.WriteLine($"\nBoo hoo! The letter {guessAsChar} does not exist in the word.");
                guessedLettersList.Add(guessAsChar);
                StringBuilder wrongGuesses = new StringBuilder();
                wrongGuesses.Append(guessAsChar);
                Console.WriteLine("You have guessed the following wrong letters: {0}", wrongGuesses, " ");

                return;
            }
        }
        static string DisplayWord(List<char> guessedLettersList, string wordToGuess)    //Method to display hidden word      
        {
            string displayWord = " "; // empty string to fill 
            if (guessedLettersList.Count == 0)
            {
                foreach (char letter in wordToGuess)
                {
                    displayWord += "_ ";
                }
                return displayWord;
            }
            foreach (char letter in wordToGuess)
            {
                bool correctTry = false;
                foreach (char key in guessedLettersList)
                {
                    if (key == letter)
                    {
                        displayWord += key + " ";
                        correctTry = true;
                        break;
                    }
                    else
                    {
                        correctTry = false;
                    }
                }
                if (correctTry == false)
                {
                    displayWord += "_";
                }
            }
            return displayWord;
        }                                                        
        static double AskForNumber()
        {
            bool notNumber = false;

            double number = 0;

            do
            {
                Console.Write("Please enter a valid number; ");
                try
                {
                    number = double.Parse(Console.ReadLine());
                    notNumber = false;
                }
                catch (ArgumentNullException)
                {
                    Console.WriteLine("I do not understand your input.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Your number was too big for this game.");
                }
                catch (FormatException)
                {
                    Console.WriteLine("Was unable to read your entry. \nYou must use a number and not text.");
                }
                catch
                {
                    Console.WriteLine("Some error happened.");
                    notNumber = true;
                }
                finally
                {
                    Console.WriteLine("      ");
                    ///Console.WriteLine($"You selected: {number}");
                }
            } while (notNumber);

            return number;

        }
        static void UserPlayAgain()
        {
            Console.WriteLine("\nReturn to start screen? Answer y or n to choose."); 
            char answer = Console.ReadKey().KeyChar;

            if (Char.ToLower(answer) == 'y')
            {
                StartGame();
            }   
        }
        static int CountGuesses(string guess, char guessAsChar, List<char> guessedLettersList, string wordToGuess)
        {
            int numberOfGuesses = 0;


            if (guessedLettersList.Contains(guessAsChar))
            {
                //nochange
            }
            else if (wordToGuess.Contains(guessAsChar))
            {
                numberOfGuesses++;
            }
            else
            {
                numberOfGuesses++;
            }

            return numberOfGuesses;
        }

        /*

    static void RightCharArray()
    {
        guessedOne = true;
        GuessedChars.Add(CurrentWord[i]);
    https://code.sololearn.com/c3A1jQq8x559/#cs
    }*/


        /*

        }*/
    }
}

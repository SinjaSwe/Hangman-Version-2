using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace Hangman_Version_2
{
    class Program
    {
        public static int guessesAvailable = 10;
        public static int noOfGuesses = 0;
        public static string wordToGuess = GetRandomWord();
        public static List<char> guessedLetters = new List<char>();
        public static bool won = false;

        static void Main(string[] args)
        {
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
            string guess;
            int remainingTries;
            string wordToDisplay = DisplayWord(guessedLetters, wordToGuess);
            remainingTries = guessesAvailable - noOfGuesses;
            Console.WriteLine(guessesAvailable);
            Console.WriteLine(noOfGuesses);
            Console.WriteLine(wordToGuess);//Only show when testing
            Console.WriteLine(wordToDisplay);            

            guess = InputGuess("\nPlease guess a letter or a word"); // Call method to get user input and returns the guess value        
            
            //QUESTION FOR PER. WHY ISNT MY LOOP PROPERLY WORKING?

            while (remainingTries < guessesAvailable && !won)
            {
                 if (guess.Length > 1)
                {
                    GuessWholeWord(guess, wordToGuess); //Method if user enters a word rather than a single letter. Written like this, calls the method (i.e runs the code within the method.
                    noOfGuesses++;
                }
                else
                {
                    GuessALetter(guess, guessedLetters, wordToGuess, wordToDisplay);  // Method if user enters a single letter
                    noOfGuesses++;
                }
            }
        }

        static string GetRandomWord() //METHOD: Random word generator
        {
            Random randomWord = new Random();
            string[] arrayOfWords = { "house", "robot", "garden", "children", "volvo", "nerd", "elephant", "school", "table" };
            string wordToGuess;
            {
                int index = randomWord.Next(arrayOfWords.Length - 1);
                wordToGuess = arrayOfWords[index].ToString();
            }
            return wordToGuess;
        }
        static string DisplayWord(List<char> guessedLetters, string wordToGuess)    //Method to display hidden word      
        {
            string displayWord = " "; // empty string to fill 
            if (guessedLetters.Count == 0)
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
                foreach (char key in guessedLetters)
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
        static void StoreGuesses(string guess)
        {
            List<char> guessedLetters = new List<char>(); 
            Console.WriteLine("You have guesses the following: " + guess);
        }
        static void GuessWholeWord(string guess, string wordToGuess) // Definition. Need to pass two strings above
        {
            string wordToDisplay = DisplayWord(guessedLetters, wordToGuess); 

            if (guess == wordToGuess)
            {
                Console.WriteLine("\nAmazing job! Well done! You guessed the correct word."); //need to if else function here 
                UserPlayAgain(); 
            }
            else
            {
                Console.WriteLine("\nBoo hoo. Wrong! Better luck next time");
            }
        }
        static void GuessALetter(string guess, List<char> guessedLetters, string wordToGuess, string wordToDisplay)
        {
            if (guessedLetters.Contains(Convert.ToChar(guess)))
            {
                Console.WriteLine($"\nYou have already guessed {guess} dummy! Try a different letter.");
                noOfGuesses--; 
            }
            else if (wordToGuess.Contains(guess))
            {
                if (!wordToDisplay.Contains("_"))
                {
                    won = true;
                    Console.WriteLine($"\nCongrats! You won! The hidden word was {wordToGuess}");
                    UserPlayAgain();
                }
                else
                {
                    Console.WriteLine($"\nWell done! You guessed correctly. The hidden word contains {guess}.");                    
                }                   
            }
            else
            {
                Console.WriteLine($"\nBoo hoo! The letter {guess} does not exist in the word.");               
            }
        }
        static string InputGuess(string textToPrint) //Method for user input
        {
            Console.WriteLine(textToPrint);
            string guess = Console.ReadLine();
            return guess;
        }                
        static void NoOfGuesses(string wordToGuess, int remainingTries)        
        {
            
            if (remainingTries <= 0)
            {
                Console.WriteLine($"You have no more guesses left. You lost! The word was {wordToGuess}");
            }

            else
                Console.WriteLine($"Guess again. You have {remainingTries} guesses left.");
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
            Console.WriteLine("Do you want to play again? Answer y or n to choose.");
            string playAgain = Console.ReadLine();
            if (playAgain == "n")
            {
                Environment.Exit(1);
            }
            Console.Clear();
        }


    
        /*static void RightCharArray()
        {
            guessedOne = true;
            GuessedChars.Add(CurrentWord[i]);
        https://code.sololearn.com/c3A1jQq8x559/#cs
        }*/


        /*
         static bool CheckForLetter(StringBuilder stringBuilder, char letter)//METHOD: StringBuilder
        {
            string text = stringBuilder.ToString();

            if (String.IsNullOrEmpty(text))
            {
                if (text.IndexOf(letter) == -1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
                return false;
            }
        }*/            
    }
}

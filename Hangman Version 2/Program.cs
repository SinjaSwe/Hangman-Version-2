﻿using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Hangman_Version_2
{
    class Program
    {
        static void Main(string[] args)
        {
            string guess;
            string wordToGuess = GetRandomWord();           

            Console.WriteLine(wordToGuess);
            guess = InputGuess("Please guess a letter or a word"); // Call method to get user input
            GuessWholeWord(guess, wordToGuess); //Method if user enters a word rather than a single letter. Written like this, calls the method (i.e runs the code within the method.
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

        //method for is Bool methold. Is it right, finish, correct, right letter, word

        static void GuessWholeWord(string guess, string wordToGuess) // Defination. Need to pass two strings above
        {
            if (guess == wordToGuess)
            {
                
                Console.WriteLine("\nBoo hoo. Wrong! Better luck next time");
            }
            else
            {
                Console.WriteLine("\nAmazing job! Well done! You guessed the correct word.");
            }
        }

        static void GuessALetter(string guess, string wordToGuess)
        {
            //have you guessed the letter before
            // if correct
            // if wrong

        }

        static string InputGuess(string textToPrint) //Method for user input
        {
            Console.WriteLine(textToPrint);
            string guess = Console.ReadLine();
            return guess;
        }

        static void StoreGuesses(string guess)
        {
            string userGuesses;
            List<string> userGuesses = new List<string>();
            Console.WriteLine("You have guesses the following: " + guess);
        }

        static void DisplayWord;
        {
        
        }
                
        


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

        //SWITCH: Play, Exit 
        // COUNT DOWN OF TRIES
        //METHOD: CHECK WHOLE WORD

        /*static string GuessWholeWord (string wordToGuess, string guess)
        

        static int GuessOneLetter
        {

        }

        */



        //METHOD: CHECK ONE LETTER
        //METHOD: COUNTER
        //METHOD: DISPLAY WORD
        //METHOD: RETRY


        
    }
}

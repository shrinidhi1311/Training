// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to implement a number guessing game with input validation and a maximum of seven attempts.
using static System.Console;
class Program {
   static void Main () {
      int n = new Random ().Next (1, 101), attempts = 1, guess;
      string message = "Guess a number from 1 to 100:";
      Print (message);
      while (attempts <= 7) {
         guess = ReadGuess ();
         if (guess == n) {
            message = $"You guessed it!\nAttempts: {attempts}\nPress any key...";
            break;
         } else if (attempts == 7) {
            message = $"No attempts left!\nCorrect Number: {n}\nPress any key...";
            break;
         }
         Print ($"Too {(guess < n ? "low" : "high")}! Try again.\nGuess again:");
         attempts++;
      }
      Print (message);
      ReadKey ();
   }

   static int ReadGuess () {
      int guess;
      while (!int.TryParse (ReadLine (), out guess) || guess < 1 || guess > 100) Print ("Invalid input.Try again:");
      return guess;
   }

   static void Print (string text) => WriteLine (text);
}
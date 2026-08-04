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
      const int MAX_ATTEMPTS = 7;
      string message = "Guess a number from 1 to 100:";
      Print (message);
      while (attempts <= MAX_ATTEMPTS && ReadGuess (out guess)) {
         bool won = guess == n, lastAttempt = attempts == MAX_ATTEMPTS;
         if (won || lastAttempt) {
            message = won ? $"You guessed it!\nAttempts: {attempts}\nPress any key..."
               : $"No attempts left!\nCorrect Number: {n}\nPress any key...";
            break;
         }
         Print ($"Too {(guess < n ? "low" : "high")}! Try again.\nGuess again:");
         attempts++;
      }
      Print (message);
      ReadKey ();
   }

   // Reads and validates the user's guess.
   static bool ReadGuess (out int guess) {
      while (!int.TryParse (ReadLine (), out guess) || guess < 1 || guess > 100) Print ("Invalid input.Try again:");
      return true;
   }

   // Displays a message on the console.
   static void Print (string text) => WriteLine (text);
}
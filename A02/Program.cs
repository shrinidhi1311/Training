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
      bool won, lastAttempt;
      string message = "Guess a number from 1 to 100:";
      Print (message);
      while (ReadGuess (out guess)) {
         won = guess == n;
         lastAttempt = attempts == MAX_ATTEMPTS;
         message = won ? $"You guessed it!\nAttempts: {attempts}\nPress any key..."
            : lastAttempt ? $"No attempts left!\nCorrect Number: {n}\nPress any key..."
            : $"Too {(guess < n ? "low" : "high")}! Try again.\nGuess again:";
         Print (message);
         if (won || lastAttempt) break;
         attempts++;
      }
      ReadKey ();
   }

   // Reads and validates the user's guess
   static bool ReadGuess (out int guess) {
      while (!int.TryParse (ReadLine (), out guess) || guess < 1 || guess > 100) Print ("Invalid input. Try again:");
      return true;
   }

   // Displays a message on the console
   static void Print (string text) => WriteLine (text);
}
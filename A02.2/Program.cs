// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to guess the user's number using binary search.
using static System.Console;

class Program {
   static void Main () {
      int low = 1, high = 100, guess;
      ConsoleKey response;
      WriteLine ("Think of a number between 1 and 100, and I will try to guess it.\n" +
                 "Y: Correctly guessed\nL: My guess is too low\nH: My guess is too high.");
      while (low <= high) {
         guess = (low + high) / 2;
         response = ReadResponse (guess);
         Write (response);
         if (response == ConsoleKey.Y) {
            WriteLine ($"\nI guessed it!\nYour number is {guess}.\nPress any key...");
            ReadKey (true);
            return;
         }
         if (response == ConsoleKey.H) high = guess - 1;
         else low = guess + 1;
      }
      WriteLine ("\nI couldn't guess your number.\nPress any key...");
      ReadKey (true);
   }

   // Reads the user's response
   static ConsoleKey ReadResponse (int guess) {
      Write ($"\nIs your number {guess,2}? (Y)es, (L)ow, (H)igh: ");
      while (true) {
         ConsoleKey response = ReadKey (true).Key;
         if (response == ConsoleKey.Y || response == ConsoleKey.L || response == ConsoleKey.H)
            return response;
      }
   }
}
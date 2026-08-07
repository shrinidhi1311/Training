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
      Print ("Think of a number between 1 and 100, and I will try to guess it.\n" +
             "Y: Correctly guessed \nL: My guess is too low  \nH: My guess is too high.");
      bool gameActive = true;
      while (gameActive) {
         guess = (low + high) / 2;
         Response response = ReadResponse (guess);
         Write (response);
         if (response == Response.Yes) {
            Print ($"\nI guessed it!\nYour number is {guess}.\nPress any key..");
            break;
         }
         if (response == Response.High) high = guess - 1;
         else low = guess + 1;
         gameActive = low <= high;
      }
      if (!gameActive) Print ("\nI couldn't guess your number.\nPress any key..");
      ReadKey (true);
   }

   // Reads the user's response
   static Response ReadResponse (int guess) {
      Print ($"\nIs your number {guess,2}? (Y)es, (L)ow, (H)igh: ");
      for (; ; ) {
         switch (ReadKey (true).Key) {
            case ConsoleKey.Y: return Response.Yes;
            case ConsoleKey.L: return Response.Low;
            case ConsoleKey.H: return Response.High;
         }
      }
   }

   // Displays a message on the console
   static void Print (string message) => Write (message);

   enum Response { Yes, Low, High }
}
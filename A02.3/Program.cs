// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to determine the user's number using binary representation.
using static System.Console;

class Program {
   static void Main () {
      Write ("Think of a number between 1 and 100." + "\nAnswer each question with Y or N.");
      int number = 0, divisor = 1;
      while (divisor <= 64) {
         Write ($"\nIs the remainder when divided by {divisor * 2} less than {divisor}? (Y/N): ");
         number += GetResponse () * divisor;
         divisor *= 2;
      }
      WriteLine ($"\nYour number is {number}.");
   }

   // Reads and validates the user's response and returns the corresponding bit
   static int GetResponse () {
      ConsoleKey response = 0;
      while (!(response is ConsoleKey.Y or ConsoleKey.N)) response = ReadKey (true).Key;
      Write (response);
      return response == ConsoleKey.N ? 1 : 0;
   }
}
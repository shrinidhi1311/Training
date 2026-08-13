// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to determine the user's number using binary representation.
using static System.Console;

class Program {
   static void Main () {
      int number = 0, divisor = 2;
      WriteLine ("Think of a number between 1 and 100." + "\nAnswer each question with Y or N.");
      while (divisor <= 128) {
         number += ReadBit (divisor) * (divisor / 2);
         divisor *= 2;
      }
      WriteLine ($"\nYour number is {number}.");
   }

   // Reads the user's response and returns the corresponding binary bit
   static int ReadBit (int divisor) {
      int half = divisor / 2;
      Write (divisor == 2 ? "\nIs your number odd? (Y/N): "
                          : $"\nIs the remainder when divided by {divisor} " +
                            $"between {half} and {divisor - 1}? (Y/N): ");
      ConsoleKey response = 0;
      while (!(response is ConsoleKey.Y or ConsoleKey.N))
         response = ReadKey (true).Key;
      Write (response);
      return response == ConsoleKey.Y ? 1 : 0;
   }
}
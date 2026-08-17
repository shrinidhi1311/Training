// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to determine the user's number using binary representation.
using System.Text;
using static System.Console;

class Program {
   static void Main () {
      Write ("Think of a number between 1 and 127.\nAnswer each question with Y or N.");
      int remainder = 0, divisor = 1, bit;
      string prompt;
      StringBuilder sb = new ();
      while (divisor <= 64) {
         prompt = $"\nWhen the number is divided by {divisor * 2, 3}, Is the remainder {remainder, 3}? (Y/N): ";
         bit = GetResponse (prompt);
         sb.Insert (0, bit);
         remainder += bit * divisor;
         divisor *= 2;
      }
      WriteLine ($"\nYour number is {Convert.ToInt32 (sb.ToString (), 2)}.");
   }

   // Returns the user's response to the given prompt as an integer
   static int GetResponse (string prompt) {
      Write (prompt);
      ConsoleKey key = 0;
      while (!(key is ConsoleKey.Y or ConsoleKey.N)) key = ReadKey (true).Key;
      Write (key);
      return key == ConsoleKey.Y ? 0 : 1;
   }
}
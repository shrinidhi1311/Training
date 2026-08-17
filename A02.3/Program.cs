// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to determine the user's number using binary representation.
using static System.Console;
using System.Text;

class Program {
   static void Main () {
      Write ("Think of a number between 1 and 127.\nAnswer each question with Y or N.");
      int remainder = 0, bit, number, bitValue = 1;
      StringBuilder binary = new ();
      while (bitValue <= 64) {
         bit = GetResponse ($"\nWhen the number is divided by {bitValue * 2, 3}, " +
                            $"is the remainder {remainder, 3}? (Y/N): ");
         binary.Insert (0, bit.ToString ());
         remainder += bit * bitValue;
         bitValue *= 2;
      }
      number = Convert.ToInt32 (binary.ToString (), 2);
      WriteLine ($"\nYour number is {number}.");
   }

   // Displays the prompt, reads and validates the user's response, and returns the corresponding binary bit
   static int GetResponse (string prompt) {
      Write (prompt);
      ConsoleKey key = 0;
      while (!(key is ConsoleKey.Y or ConsoleKey.N))
         key = ReadKey (true).Key;
      Write (key);
      return key == ConsoleKey.Y ? 0 : 1;
   }
}
// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to identify valid words using a given set of letters, calculate their scores and rank them.
using static System.Console;
class Program {

   static void Main () {
      char[] allowedLetters = ['U', 'X', 'A', 'L', 'T', 'N', 'E'];
      string[] words = File.ReadAllLines ("words.txt");
      Dictionary<string, int> result = new ();
      foreach (string word in words)
         if (IsValid (word)) result[word] = Score (word);
      int totalScore = 0;
      foreach (var item in result.OrderByDescending (x => x.Value).ThenBy (x => x.Key)) {
         if (IsPangram (item.Key)) ForegroundColor = ConsoleColor.Green;
         else ResetColor ();
         totalScore += item.Value;
         WriteLine ($"{item.Value,3}. {item.Key}");
      }
      WriteLine ($"----\n{totalScore,3} Total");

      // Checks whether the word meets the required letter and length conditions
      bool IsValid (string word)
         => word.Length >= 4 && word.Contains (allowedLetters[0])
                             && word.All (allowedLetters.Contains);

      // Calculates the score based on word length and pangram status
      int Score (string word)
         => word.Length == 4 ? 1 : IsPangram (word) ? word.Length + 7 : word.Length;

      // Checks whether the word contains all the allowed letters
      bool IsPangram (string word) => allowedLetters.All (word.Contains);
   }
}
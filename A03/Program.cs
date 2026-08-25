// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to identify valid words using a given set of letters, calculate their scores and rank them.
using System.Reflection;
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      var assembly = Assembly.GetExecutingAssembly ();
      var resourceName = assembly.GetManifestResourceNames ()
         .First (name => name.EndsWith ("words.txt"));
      using var stream = assembly.GetManifestResourceStream (resourceName)!;
      using var reader = new StreamReader (stream);
      string[] words = reader.ReadToEnd ()
                             .Split (["\r\n", "\r", "\n"], StringSplitOptions.RemoveEmptyEntries);
      Dictionary<string, int> result = [];
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
   }

   #region Implementation -------------------------------------------
   // Checks whether the word meets the required letter and length conditions
   static bool IsValid (string word)
       => word.Length >= 4 && word.Contains (sAllowedLetters[0])
                           && word.All (sAllowedLetters.Contains);

   // Calculates the score based on word length and pangram status
   static int Score (string word)
       => word.Length == 4 ? 1 : IsPangram (word) ? word.Length + 7 : word.Length;

   // Checks whether the word contains all the allowed letters
   static bool IsPangram (string word) => sAllowedLetters.All (word.Contains);
   #endregion

   #region Fields ---------------------------------------------------
   static readonly char[] sAllowedLetters = ['U', 'X', 'A', 'L', 'T', 'N', 'E'];
   #endregion
}
#endregion
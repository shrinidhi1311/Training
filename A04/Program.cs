// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to find the occurrence of all letters in words.txt and display the top 7 letters with
// their occurrences.
// ------------------------------------------------------------------------------------------------
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
      Dictionary<char, int> freq = [];
      string words = reader.ReadToEnd ();
      foreach (char c in words)
         if (char.IsLetter (c)) freq[c] = freq.TryGetValue (c, out int value) ? ++value : 1;
      WriteLine ("Seven most frequently occurring letters and their occurrences\n");
      foreach (var ch in freq.OrderByDescending (x => x.Value).Take (7))
         WriteLine ($"{ch.Key} occurred {ch.Value} times.");
   }
}
#endregion
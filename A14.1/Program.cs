// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Finds anagrams in a word list and sorts them by the number of words in each group.
// ------------------------------------------------------------------------------------------------

#region class Program -----------------------------------------------------------------------------
class Program {
   #region Methods --------------------------------------------------
   /// <summary>Groups words into anagrams and sorts the groups by their count in descending order</summary>
   static void Main () {
      var anagrams = File.ReadAllLines ("words.txt")
         .Select (w => w.Trim ().ToUpper ()).Where (w => w.Length > 0).Distinct ()
         .GroupBy (w => string.Concat (w.Order ()))
         .Where (g => g.Count () > 1)
         .ToLookup (g => g.Count ());
      foreach (var sizeGroup in anagrams.OrderByDescending (s => s.Key))
         foreach (var g in sizeGroup)
            Console.WriteLine ($"{sizeGroup.Key} {string.Join (" ", g)}");
   }
   #endregion
}
#endregion
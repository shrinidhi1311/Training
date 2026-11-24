// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// WordBank.cs
// Loads the puzzle words (secret word candidates) and the dictionary (valid guesses) for Wordle.
// ------------------------------------------------------------------------------------------------

#region class WordBank ----------------------------------------------------------------------------
class WordBank {
   #region Constructor ----------------------------------------------
   /// <summary>Loads the 5-letter words from the puzzle file and the dictionary file</summary>
   public WordBank (string puzzleFile, string dictFile) {
      mPuzzle = Read (puzzleFile);
      mDictionary = [.. Read (dictFile)];
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns a randomly chosen secret word from the puzzle list</summary>
   public string GetRandomWord () => mPuzzle[Random.Shared.Next (mPuzzle.Length)];

   /// <summary>Returns true if the word is found in the dictionary</summary>
   public bool Contains (string word) => mDictionary.Contains (word);
   #endregion

   #region Implementation -------------------------------------------
   // Reads the 5-letter words of a file, converted to uppercase
   static string[] Read (string file)
      => [.. File.ReadAllLines (file).Select (w => w.Trim ().ToUpper ()).Where (w => w.Length == 5)];
   #endregion

   #region Private Data ---------------------------------------------
   readonly string[] mPuzzle;
   readonly HashSet<string> mDictionary;
   #endregion
}
#endregion
// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Wordle.cs
// Implements the Wordle game by handling user input, word validation, and result display.
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

#region class Wordle ------------------------------------------------------------------------------
class Wordle {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes the game with supplied word bank and picks the secret word</summary>
   public Wordle (WordBank bank) {
      mBank = bank;
      mAnswer = bank.GetRandomWord ();
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Entry point: loads the word lists and starts the game</summary>
   static void Main () => new Wordle (new WordBank ("puzzle-5.txt", "dict-5.txt")).Play ();

   /// <summary>Plays the game: draw the screen, read one key and update the state, until over</summary>
   public void Play () {
      OutputEncoding = Encoding.UTF8;
      CursorVisible = false;
      Clear ();
      DrawScreen ();
      while (!mFinished) {
         ProcessKey (ReadKey (true));
         DrawScreen ();
      }
      SetCursorPosition (0, MESSAGEROW + 2);
      CursorVisible = true;
   }
   #endregion

   #region Implementation -------------------------------------------
   // Handles one key press. Any key first dismisses the previous message
   void ProcessKey (ConsoleKeyInfo key) {
      mMessage = "";
      if (key.Key is ConsoleKey.Backspace or ConsoleKey.LeftArrow) {
         if (mCurrent.Length > 0) mCurrent = mCurrent[..^1];
      } else if (key.Key == ConsoleKey.Enter) {
         if (mCurrent.Length == LETTERS) CheckGuess ();
      } else if (char.IsAsciiLetter (key.KeyChar) && mCurrent.Length < LETTERS)
         mCurrent += char.ToUpper (key.KeyChar);
   }

   // Submits the typed word: validate, color it, then check for a win or the last try
   void CheckGuess () {
      if (!mBank.Contains (mCurrent)) {
         ShowMessage ($"{mCurrent} is not a valid word", ConsoleColor.Yellow);
         return;
      }
      var result = Evaluate (mCurrent);
      for (int i = 0; i < LETTERS; i++) {
         int k = mCurrent[i] - 'A';
         if (result[i] > mKeyboard[k]) mKeyboard[k] = result[i];
      }
      mGuesses.Add (mCurrent);
      mResults.Add (result);
      if (mCurrent == mAnswer) {
         mFinished = true;
         ShowMessage ($"You guessed the word {mAnswer}!", ConsoleColor.Green);
      } else if (mGuesses.Count == TRIES) {
         mFinished = true;
         ShowMessage ($"The word was {mAnswer}", ConsoleColor.Red);
      }
      mCurrent = "";
   }

   // Marks exact matches first, then checks unmatched letters to handle repeated letters
   ELetterState[] Evaluate (string guess) {
      var result = new ELetterState[LETTERS];
      var unmatched = new int[26];
      for (int i = 0; i < LETTERS; i++) {
         if (guess[i] == mAnswer[i]) result[i] = ELetterState.Green;
         else unmatched[mAnswer[i] - 'A']++;
      }
      for (int i = 0; i < LETTERS; i++) {
         if (result[i] == ELetterState.Green) continue;
         int k = guess[i] - 'A';
         if (unmatched[k] > 0) {
            result[i] = ELetterState.Blue;
            unmatched[k]--;
         } else result[i] = ELetterState.Gray;
      }
      return result;
   }

   // Redraws the complete screen by overprinting it
   void DrawScreen () {
      int gridLeft = WindowWidth / 2 - 5;
      for (int row = 0; row < TRIES; row++) {
         SetCursorPosition (gridLeft, row);
         for (int col = 0; col < LETTERS; col++) {
            var (ch, color) = CellView (row, col);
            Print (ch, color);
         }
      }
      SetCursorPosition (WindowWidth / 2 - 9, TRIES);
      Write (new string ('-', 17));
      int keyLeft = WindowWidth / 2 - 7;
      for (int i = 0; i < 26; i++) {
         if (i % KEYSPERROW == 0) SetCursorPosition (keyLeft, TRIES + 1 + i / KEYSPERROW);
         Print ((char)('A' + i), ColorState[(int)mKeyboard[i]]);
      }
      SetCursorPosition (0, MESSAGEROW);
      Write (new string (' ', WindowWidth - 1));
      SetCursorPosition ((WindowWidth - mMessage.Length) / 2, MESSAGEROW);
      ForegroundColor = mMessageColor;
      Write (mMessage);
      ResetColor ();
   }

   // Returns the character and color to show in a grid cell: a submitted letter, a letter being
   // typed, the circle (next position) or a dot
   (char, ConsoleColor) CellView (int row, int col) {
      if (row < mGuesses.Count) return (mGuesses[row][col], ColorState[(int)mResults[row][col]]);
      if (row == mGuesses.Count && !mFinished) {
         if (col < mCurrent.Length) return (mCurrent[col], ConsoleColor.White);
         if (col == mCurrent.Length) return (CIRCLE, ConsoleColor.White);
      }
      return (DOT, ConsoleColor.White);
   }

   // Displays a character in the specified foreground color
   static void Print (char c, ConsoleColor color) {
      ForegroundColor = color;
      Write ($"{c} ");
      ResetColor ();
   }

   // Sets the message (and its color) to be shown below the keyboard
   void ShowMessage (string text, ConsoleColor color) => (mMessage, mMessageColor) = (text, color);
   #endregion

   #region Private Data ---------------------------------------------
   readonly WordBank mBank;
   readonly string mAnswer;                                    // The secret word
   readonly List<string> mGuesses = [];                        // Submitted words
   readonly List<ELetterState[]> mResults = [];                 // Colors of each submitted word
   readonly ELetterState[] mKeyboard = new ELetterState[26];     // Best status of each letter A..Z
   string mCurrent = "";                                       // The word being typed
   string mMessage = "";
   ConsoleColor mMessageColor;
   bool mFinished;
   // Colors indexed by LetterState: Unused, Gray, Blue, Green
   static readonly ConsoleColor[] ColorState = [ConsoleColor.White, ConsoleColor.DarkGray,
                                             ConsoleColor.Blue, ConsoleColor.Green];
   #endregion

   #region Constants ------------------------------------------------
   const int TRIES = 6;
   const int LETTERS = 5;
   const int KEYSPERROW = 7;
   const int MESSAGEROW = 12;
   const char DOT = '\u00b7';
   const char CIRCLE = '\u25cc';
   #endregion
}
#endregion

#region Enum ELetterState -------------------------------------------
//Status of a letter; the order matters (a higher value is a better status)
enum ELetterState {
   Unused,     // Not tried yet (white)
   Gray,       // Letter is not in the word
   Blue,       // Letter is in the word, but at another position
   Green       // Letter is at the right position
}
#endregion
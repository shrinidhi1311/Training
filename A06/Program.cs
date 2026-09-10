// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to find solutions to the Eight Queens problem using backtracking.
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      WriteLine ("Eight Queens Problem");
      Write ("Would you like to see (A)ll solutions or (U)nique solutions only? ");
      ConsoleKey response;
      while ((response = ReadKey (true).Key) is not (ConsoleKey.A or ConsoleKey.U)) ;
      Clear ();
      bool iShowUnique = response == ConsoleKey.U;
      PrintSolutions (SolveQueens (iShowUnique), iShowUnique ? "Unique Solutions" : "All Solutions");
   }

   #region Implementation -------------------------------------------
   // Finds all valid queen placements using backtracking.
   static List<int[]> SolveQueens (bool findUnique) {
      List<int[]> solutions = [];
      HashSet<string> symmetries = [];
      int[] positions = new int[N];
      bool[] iColumns = new bool[N];
      bool[] iDiagonals = new bool[2 * N - 1];
      bool[] iAntiDiagonals = new bool[2 * N - 1];
      PlaceQueen (0);
      return solutions;

      // Recursively places queens on the board.
      void PlaceQueen (int row) {
         for (int column = 0; column < N; column++) {
            int diagonal = row - column + N - 1;
            int antiDiagonal = row + column;
            if (iColumns[column] || iDiagonals[diagonal] || iAntiDiagonals[antiDiagonal])
               continue;
            positions[row] = column;
            iColumns[column] = true;
            iDiagonals[diagonal] = true;
            iAntiDiagonals[antiDiagonal] = true;
            if (row < N - 1) PlaceQueen (row + 1);
            else {
               int[] solution = [.. positions];
               if (!findUnique || IsUnique (solution)) {
                  solutions.Add (solution);
                  if (findUnique) AddSymmetries (solution);
               }
            }
            iColumns[column] = false;
            iDiagonals[diagonal] = false;
            iAntiDiagonals[antiDiagonal] = false;
         }
      }

      // Stores the rotated and mirrored versions of a solution.
      void AddSymmetries (int[] solution) {
         for (int i = 0; i < 4; i++) {
            solution = RotateBoard (solution);
            symmetries.Add (ArrayToString (solution));
            symmetries.Add (ArrayToString (Mirror (solution)));
         }
      }

      string ArrayToString (int[] solution) => string.Join (",", solution);

      // Rotates a queen placement by 90 degrees.
      static int[] RotateBoard (int[] positions) {
         int[] rotated = new int[N];
         for (int row = 0; row < N; row++) rotated[positions[row]] = N - row - 1;
         return rotated;
      }

      // Creates the mirror image of a queen placement.
      static int[] Mirror (int[] positions) => [.. positions.Reverse ()];

      // Checks if a solution already exists in the list of symmetries.
      bool IsUnique (int[] test) => !symmetries.Contains (ArrayToString (test));
   }

   // Displays the solutions and allows navigation between them.
   static void PrintSolutions (List<int[]> solutions, string title) {
      OutputEncoding = Encoding.UTF8;
      int solutionNumber = 0;
      while (true) {
         SetCursorPosition (0, 0);
         WriteLine (title);
         WriteLine ($"Solution {solutionNumber + 1,2} of {solutions.Count}\n");
         PrintBoard (solutions[solutionNumber]);
         WriteLine ("\n[←] Back    [→] Next    [Esc] Exit");
         ConsoleKey key;
         while ((key = ReadKey (true).Key) is not
                (ConsoleKey.RightArrow or ConsoleKey.LeftArrow or ConsoleKey.Escape)) ;
         switch (key) {
            case ConsoleKey.RightArrow:
               solutionNumber = Math.Min (solutionNumber + 1, solutions.Count - 1);
               break;
            case ConsoleKey.LeftArrow:
               solutionNumber = Math.Max (solutionNumber - 1, 0);
               break;
            case ConsoleKey.Escape:
               Clear ();
               return;
         }
      }
   }

   // Displays a queen placement as a chess board.
   static void PrintBoard (int[] queens) {
      WriteLine (sTopBorder);
      for (int row = 0; row < N; row++) {
         Write (VERTICAL);
         for (int column = 0; column < N; column++)
            Write ((queens[row] == column ? QUEEN : EMPTY) + VERTICAL);
         WriteLine ();
         if (row < N - 1) WriteLine (sMiddleBorder);
      }
      WriteLine (sBottomBorder);
   }

   // Creates a board border using the specified pattern.
   static string Border (string pattern)
       => pattern[0] + string.Join (pattern[1], Enumerable.Repeat (HORIZONTAL, N)) + pattern[2];
   #endregion

   #region Fields ---------------------------------------------------
   static readonly string sTopBorder = Border (TOP);
   static readonly string sMiddleBorder = Border (MID);
   static readonly string sBottomBorder = Border (BOTTOM);
   #endregion

   #region Constants ------------------------------------------------
   const string TOP = "┌┬┐";
   const string MID = "├┼┤";
   const string BOTTOM = "└┴┘";
   const string VERTICAL = "│";
   const string HORIZONTAL = "────";
   const string EMPTY = "    ";
   const string QUEEN = " ♕  ";
   const int N = 8;
   #endregion
}
#endregion
// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Implementation of file name parser with state machine.
// ------------------------------------------------------------------------------------------------
using static System.Console;
using static EState;

#region class Program -----------------------------------------------------------------------------
class Program {
   #region Methods --------------------------------------------------
   /// <summary>Runs sample paths through ParsePath and prints a result table</summary>
   static void Main () {
      var testCases = new[]{
            @"C:\WORK\R.TXT", @"D:\A\B.C", @"C:\DIR\APP\CSV\JIR.TXT", @"Z:\ABC\FILE.EXE",
            @"E:\DOCS\REPORT.PDF", @"C:\A\B\C\D\E\F\G\FILE.TXT", @"M:\PROJECTS\SRC\MAIN.CS",
            @"F:\X\Y.Z", @"A:\TEMP\LOG.TXT", @"H:\MUSIC\ROCK\SONG.MPG",
            @"C:\README.TXT", @"C:\WORK\r.TXT", @"CZ:\ABC\R.TXT", @"C:\ABC1\FILE.TXT",
            @"C:\ABC\.TXT", @"C:\ABC\FILE.", @"C:\ABC\FILE", @"C:\ABC\FILE.TAR.GZ",
            @"C:\AB C\FILE.TXT", @"C:\ABC\\FILE.TXT", @"C:\WORK\R.TXT", @"D:\A\B.C",
            @"C:\A\B\C\D\E\F\G\FILE.TXT", @"M:\PROJECTS\SRC\MAIN.CS",
            @"C:\work\r.txt", @"c:\WORK\R.TXT",@"C:\DIR\APP\CSV\JIR.TXT", @"Z:\ABC\FILE.EXE",
            @"CZ:\ABC\R.TXT", @":\ABC\R.TXT", @"C\ABC\R.TXT", @"1:\ABC\R.TXT",
            @"C:ABC\R.TXT", @"C:\\ABC\R.TXT", @"C:\ABC\\R.TXT", @"C:\ABC\R.TXT\", @"C:/ABC/R.TXT",
            @"C:\R.TXT", @"C:\AB C\R.TXT", @"C:\ABC1\R.TXT", @"C:\AB.C\R.TXT",
            @"C:\ABC\.TXT", @"C:\ABC\R-1.TXT",
            @"C:\ABC\FILE", @"C:\ABC\FILE.", @"C:\ABC\FILE.T1", @"C:\ABC\FILE.TAR.GZ",
            @"README.TXT", @"C:\ABC", @"C:\", @"C:", @"C:\ABC\R~.TXT", "", " "
        };
      WriteLine ($"| {"Input", -28}| {"Drive", -6}| {"Directory", -16}| {"Filename", -10}| " +
                 $"{"Extension", -10}| Verdict");
      WriteLine (new string ('-', 94));
      int valid = 0;
      foreach (var input in testCases) {
         var (isValid, drive, directory, filename, extension) = ParsePath (input);
         if (isValid) valid++;
         Write ($"| {input, -28}| {Show (drive), -6}| {Show (directory), -16}| " +
                $"{Show (filename), -10}| {Show (extension), -10}| ");
         ForegroundColor = isValid ? ConsoleColor.Green : ConsoleColor.Red;
         WriteLine (isValid ? "VALID" : "INVALID");
         ResetColor ();
      }
      WriteLine ($"\nValid: {valid}/{testCases.Length}");
   }
   #endregion

   #region Implementation -------------------------------------------
   // Parses X:\DIR\NAME.EXT with a state machine; returns validity and its parts
   static (bool, string, string, string, string) ParsePath (string input) {
      EState s = A;
      Action none = () => { }, todo;
      bool isValid;
      string drive = "", directory = "", filename = "", extension = "";
      foreach (var ch in input + '~') {
         (s, todo) = (s, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, () => drive += ch),
            (B, ':') => (C, none),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z') => (E, () => filename += ch),
            (E, '\\') => (D, () => (directory, filename) = (directory == "" ? filename
                                                           : directory + "," + filename, "")),
            (E, '.') when directory != "" => (F, none),
            (F or G, >= 'A' and <= 'Z') => (G, () => extension += ch),
            (G, '~') => (H, none),
            _ => (Z, none)
         };
         todo ();
      }
      isValid = s == H;
      return isValid ? (true, drive, directory, filename, extension) : (false, "", "", "", "");
   }

   // Returns "-" for an empty field so blank columns are visible
   static string Show (string s) => s == "" ? "-" : s;
   #endregion
}
#endregion

#region Enum EState -------------------------------------------------
/// <summary>States of the path parser; Z is the reject state and H is the accept state</summary>
public enum EState {
   A,   // Start: expects drive letter
   B,   // Drive read: expects ':'
   C,   // Colon read: expects '\'
   D,   // After '\': expects name letter
   E,   // In name: letter, '\' (directory), or '.'
   F,   // After '.': expects extension letter
   G,   // In extension: letter or '~' (end)
   H,   // Accept
   Z    // Reject
}
#endregion
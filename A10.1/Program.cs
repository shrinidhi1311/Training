namespace Parse;

using static System.Console;
using static Program.EState;
#region class Program -----------------------------------------------------------------------------
class Program {
   #region Methods -------------------------------------------------
   static void Main () {
      var testCases = new[] {
            @"C:\WORK\R.TXT", @"D:\A\B.C", @"C:\DIR\APP\CSV\JIR.TXT", @"Z:\ABC\FILE.EXE",
            @"E:\DOCS\REPORT.PDF", @"C:\A\B\C\D\E\F\G\FILE.TXT", @"M:\PROJECTS\SRC\MAIN.CS",
            @"F:\X\Y.Z", @"A:\TEMP\LOG.TXT", @"H:\MUSIC\ROCK\SONG.MPG",
            @"C:\README.TXT", @"C:\WORK\r.TXT", @"CZ:\ABC\R.TXT", @"C:\ABC1\FILE.TXT",
            @"C:\ABC\.TXT", @"C:\ABC\FILE.", @"C:\ABC\FILE", @"C:\ABC\FILE.TAR.GZ",
            @"C:\AB C\FILE.TXT", @"C:\ABC\\FILE.TXT",
            @"Cz:\abc\def\r.txt", @"C:\abc\def\readme.txt", @"C:\Readme.txt", @"C:\abc\.bcf",
            @"C:\abc\bcf.", @"Readme.txt", @"C:\abc\def", @"C:\abc d", @"\abcd\Readme.txt", " ",
            @"C:\ab.c\def\r.txt", @"C:\abc:d", @".\abc", ".abc", "abc", @"C:\abc6\def\r.txt",
            @"C:\DIR\ARCHIVE.TAR.GZ", @"C:\work\r.txt", @"C:\\work~\\r.txt~",
            @"C:\A\B\C\D\E\F\G\FILE.TXT", @"C:\DIR\\FILE.TXT",@"C:\DIR\APP\CSV\JIR.TXT"
      };
      WriteLine ($"{"Input",-28}{"Drive",-8}{"Directory",-16}{"Filename",-11}{"Extension",-18}");
      foreach (var testCase in testCases) {
         WriteLine (ParsePath (testCase));
      }

   }

   #endregion

   #region Implementation ------------------------------------------
   static (string, string, string, string) ParsePath (string input) {
      EState s = A;
      Action none = () => { }, todo;
      string drive = "", directory = "", filename = "", extension = "";
      foreach (var ch in input + '~') {
         (s, todo) = (s, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, () => drive += ch),
            (B, ':') => (C, none),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z') => (E, () => filename += ch),
            (E, '\\') => (F, () => (directory, filename) = (filename, "")),
            (F or G, >= 'A' and <= 'Z') => (G, () => filename += ch),
            (G, '\\') => (F, () => (directory, filename) = (directory + "," + filename, "")),
            (G, '.') => (H, none),
            (H or I, >= 'A' and <= 'Z') => (I, () => extension += ch),
            (I, '~') => (J, none),
            _ => (Z, none)
         };
         todo ();
      }
      return (s == J) ? (drive, directory, filename, extension) : ("", "", "", "");
   }
   #endregion

   #region Enum EState ---------------------------------------------
   public enum EState {
      A, B, C, D, E, F, G, H, I, J, Z
   }
   #endregion
}
#endregion
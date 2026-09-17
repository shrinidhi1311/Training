// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Program to convert string to Double
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region Class Program -----------------------------------------------------------------------------
class Program {
   static void Main () {
      string[] samples = ["42.75", "-18.625", "3.14e2", "-2.5e-3", "100","0.000045", "+78.5",
                          "9e4", "25.0","7.", ".75", "4.5e", "8e+", "5.2e-2.5","12abc", "--45",
                          "3..14", "1.2*3", "-0.0045","  56.789  ", "90E2", "+7.25e+2", "0.5e-4"];
      for (int i = 0; i < samples.Length; i++) {
         string input = samples[i];
         double parsed = DoubleParse (input);
         double expected = double.TryParse (input, out double value) ? value : NAN;
         WriteLine ($"Testcase {i + 1}");
         WriteLine ($"  Input     : \"{input}\"");
         WriteLine ($"  Result    : {parsed}");
         WriteLine ($"  Expected  : {expected}");
         WriteLine ();
      }
   }

   #region Implementation -------------------------------------------
   // Parses the input string and returns a double value.
   static double DoubleParse (string input) {
      input = input.Trim ();
      int position = 0, numSign = 1, digitCount = 0, len = input.Length;
      double number = 0;
      if (len == 0) return NAN;
      if (input[position] is '+' or '-') {
         numSign = input[position++] == '-' ? -1 : 1;
      }
      while (position < len && char.IsDigit (input[position])) {
         number = number * 10 + input[position++] - '0';
         digitCount++;
      }
      if (position < len && input[position] == '.') {
         position++;
         int decDigits = 0, decNum = 0;
         while (position < len && char.IsDigit (input[position])) {
            decNum = (input[position++] - '0') + (decNum * 10);
            decDigits++;
            digitCount++;
         }
         number += decNum / Math.Pow (10, decDigits);
      }
      if (digitCount == 0) return NAN;
      if (position < len && input[position] is 'e' or 'E') {
         position++;
         int expSign = 1, expVal = 0, expDigits = 0;
         if (position < len && input[position] is '+' or '-') {
            expSign = input[position++] == '-' ? -1 : 1;
         }
         while (position < len && char.IsDigit (input[position])) {
            expVal = expVal * 10 + input[position++] - '0';
            expDigits++;
         }
         if (expDigits == 0) return NAN;
         number *= Math.Pow (10, expVal * expSign);
      }
      return position == len ? numSign * number : NAN;
   }
   #endregion

   #region Constant -------------------------------------------------
   const double NAN = double.NaN;
   #endregion
}
#endregion
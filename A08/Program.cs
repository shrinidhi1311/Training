// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Program.cs
// Runs test cases for the expression evaluator
// ------------------------------------------------------------------------------------------------
namespace Eval;

#region class Program -----------------------------------------------------------------------------
class Program {
   #region Methods --------------------------------------------------
   static void Main () {
      var eval = new Evaluator ();
      Console.WriteLine ("Expression Evaluator\n---------------------");
      Console.WriteLine (
          "Try 2+3*4, sin(30) or x = 5  |  'test' runs tests  |  'exit' quits\n");
      while (true) {
         Console.Write ("> ");
         string input = Console.ReadLine ()?.Trim ().ToLowerInvariant () ?? "";
         if (input == "exit") break;
         if (input == "test") { RunTests (eval); continue; }
         try {
            double result = eval.Evaluate (input);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine (result);
         } catch (Exception ex) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine (ex.Message);
         }
         Console.ResetColor ();
      }
   }
   #endregion

   #region Implementation -------------------------------------------
   // Runs test cases and compares actual results with expected results
   static void RunTests (Evaluator eval) {
      (string Input, double? Expected)[] cases = [
         // Basic arithmetic
         ("2+3*4", 14),
         ("10-3", 7),
         ("10/2", 5),
         ("2^3", 8),
         // Unary minus
         ("-5", -5),
         ("5-3", 2),
         ("-2+3", 1),
         ("2*-3", -6),
         ("(-4)", -4),
         ("-(2+3)", -5),
         ("--5", 5),
         ("---5", -5),
         // Precedence and associativity
         ("-2^2", -4),
         ("2^-3", 0.125),
         ("(-2)^2", 4),
         ("2^3^2", 512),
         ("-2*-3", 6),
         ("-(2*3)+4", -2),
         // Functions
         ("sqrt(9)", 3),
         ("sin(30)", 0.5),
         ("sin(-30)", -0.5),
         ("cos(60)", 0.5),
         ("abs(-5)", null),
         // Variable assignments
         ("x=10", 10),
         ("x+5", 15),
         ("x=-10", -10),
         ("x", -10),
         // Invalid expressions
         ("5/0", null),
         ("sqrt(-1)", null),
         ("log(0)", null),
         ("(2+3", null),
         ("2+3)", null),
         ("2**3", null),
         ("", null)
      ];
      int passed = 0;
      for (int i = 0; i < cases.Length; i++) {
         var (input, expected) = cases[i];
         try {
            double result = eval.Evaluate (input);
            bool isPassed = expected.HasValue && Math.Abs (result - expected.Value) < 1e-9;
            PrintResult (i + 1, input, result.ToString (),
                         expected?.ToString () ?? "EvalException", isPassed);
            if (isPassed) passed++;
         } catch (EvalException) {
            bool isPassed = expected is null;
            PrintResult (i + 1, input, "EvalException",
                         expected?.ToString () ?? "EvalException", isPassed);
            if (isPassed) passed++;
         } catch (Exception ex) {
            PrintResult (i + 1, input, ex.GetType ().Name,
                         expected?.ToString () ?? "EvalException", false);
         }
      }
      Console.WriteLine ($"\nPassed: {passed}/{cases.Length}");
   }

   // Prints the test input, actual result, expected result, and status
   static void PrintResult (int number, string input, string result,
                           string expected, bool passed) {
      Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      Console.WriteLine ($"Testcase {number} {(passed ? "PASS" : "FAIL")}");
      Console.WriteLine ($"  Input    : \"{input}\"");
      Console.WriteLine ($"  Result   : {result}");
      Console.WriteLine ($"  Expected : {expected}");
      Console.ResetColor ();
   }
   #endregion
}
#endregion
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
      Console.WriteLine ("Try 2+3*4, sin(30) or x = 5  |  'test' runs tests\n");
      while (true) {
         Console.Write ("> ");
         string? line = Console.ReadLine ();
         if (line is null) break;
         string input = line.Trim ().ToLowerInvariant ();
         if (input == "") break;
         if (input == "test") { RunTests (eval); break; }
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
         ("sin(30)^2", 0.25),
         ("cos(60)", 0.5),
         ("sin(-30)^2", 0.25),
         ("abs(-5)", null),
         // Variable assignments
         ("x=10", 10),
         ("x+5", 15),
         ("x=-10", -10),
         ("x", -10),
         // Invalid expressions
         ("5/0", null),
         ("2)+(3", null),
         (")2(", null),
         ("sqrt(-1)", null),
         ("log(0)", null),
         ("(2+3", null),
         ("2+3)", null),
         ("2**3", null),
         ("", null)
      ];
      Console.WriteLine ($"| {"#",3} | {"Input",-12}| {"Result",-45}| {"Expected",-14}| Verdict");
      Console.WriteLine (new string ('-', 95));
      int passed = 0;
      for (int i = 0; i < cases.Length; i++) {
         var (input, expected) = cases[i];
         try {
            double result = eval.Evaluate (input);
            bool isPassed = expected.HasValue && Math.Abs (result - expected.Value) < TOLERANCE;
            PrintResult (i + 1, input, result.ToString (),
                         expected?.ToString () ?? "EvalException", isPassed);
            if (isPassed) passed++;
         } catch (EvalException ex) {
            bool isPassed = expected is null;
            PrintResult (i + 1, input, $"{ex.Message}",
                         expected?.ToString () ?? "EvalException", isPassed);
            if (isPassed) passed++;
         } catch (Exception ex) {
            PrintResult (i + 1, input, $"{ex.GetType ().Name}: {ex.Message}",
                         expected?.ToString () ?? "EvalException", false);
         }
      }
      Console.WriteLine ($"\nPassed: {passed}/{cases.Length}");
   }

   // Prints one aligned table row; only the verdict is coloured
   static void PrintResult (int number, string input, string result,
                           string expected, bool passed) {
      Console.Write ($"| {number,3} | {input,-12}| {result,-45}| {expected,-14}| ");
      Console.ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      Console.WriteLine (passed ? "PASS" : "FAIL");
      Console.ResetColor ();
   }
   #endregion

   #region Private Data ---------------------------------------------
   const double TOLERANCE = 1e-9;
   #endregion
}
#endregion
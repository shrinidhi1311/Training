// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TestCases.cs
// Tests the generic queue implementation.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class Program -----------------------------------------------------------------------------
// Tests the generic queue implementation.
class Program {
   static void Main () {
      WriteLine ("Test Results");
      WriteLine ("------------");
      Test1 ();
      Test2 ();
      Test3 ();
      Test4 ();
      Test5 ();
      Test6 ();
      WriteLine ($"\nResult: {sPassedTests}/{sTestNumber} Tests Passed");
   }

   #region Implementation -------------------------------------------
   // Tests a single element and empty state
   static void Test1 () {
      var queue = new TQueue<int> ();
      queue.Enqueue (25);
      Print ("Single element and empty state", queue.Dequeue () == 25 && queue.IsEmpty);
   }

   // Tests FIFO order
   static void Test2 () {
      var queue = new TQueue<int> ();
      for (int i = 1; i <= 3; i++) queue.Enqueue (i);
      Print ("FIFO order", VerifyOrder (queue, 1, 3));
   }

   // Tests circular movement
   static void Test3 () {
      var queue = new TQueue<int> ();
      for (int i = 1; i <= 4; i++) queue.Enqueue (i);
      queue.Dequeue ();
      queue.Dequeue ();
      queue.Enqueue (5);
      queue.Enqueue (6);
      Print ("Circular order", VerifyOrder (queue, 3, 6));
   }

   // Tests automatic resizing
   static void Test4 () {
      var queue = new TQueue<int> ();
      for (int i = 0; i < 8; i++) queue.Enqueue (i);
      Print ("Resize", VerifyOrder (queue, 0, 7));
   }

   // Tests resizing after circular wrapping
   static void Test5 () {
      var queue = new TQueue<int> ();
      for (int i = 1; i <= 4; i++) queue.Enqueue (i);
      queue.Dequeue ();
      queue.Dequeue ();
      for (int i = 5; i <= 7; i++) queue.Enqueue (i);
      Print ("Wrapped resize", VerifyOrder (queue, 3, 7));
   }

   // Tests Dequeue on an empty queue
   static void Test6 () {
      var queue = new TQueue<int> ();
      bool passed = false;
      try {
         queue.Dequeue ();
      } catch (InvalidOperationException) {
         passed = true;
      }
      Print ("Empty Dequeue", passed);
   }

   // Checks dequeue order against expected values
   static bool VerifyOrder (TQueue<int> queue, int start, int end) {
      while (start <= end) {
         if (queue.IsEmpty || queue.Dequeue () != start) return false;
         start++;
      }
      return queue.IsEmpty;
   }

   // Displays the test result
   static void Print (string name, bool passed) {
      sTestNumber++;
      if (passed) sPassedTests++;
      ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      WriteLine ($"{sTestNumber}. {name,-20} : {(passed ? "PASS" : "FAIL")}");
      ResetColor ();
   }
   #endregion

   #region Private data ---------------------------------------------
   static int sTestNumber, sPassedTests;
   #endregion
}
#endregion
// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TestCases.cs
// Tests the generic queue implementation.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region Class Program -----------------------------------------------------------------------------
/// <summary>Tests the generic queue implementation.</summary>
class Program {
   static int mTestNumber;
   static int mPassedTests;
   static void Main () {
      WriteLine ("Test Results");
      WriteLine ("------------");
      Test1 ();
      Test2 ();
      Test3 ();
      Test4 ();
      Test5 ();
      Test6 ();
      Test7 ();
      WriteLine ();
      WriteLine ($"Result: {mPassedTests}/{mTestNumber} Tests Passed");
   }

   #region Implementation -------------------------------------------
   // Tests a single element.
   static void Test1 () {
      var queue = new TQueue<int> ();
      queue.Enqueue (25);
      var res = new List<int> { queue.Dequeue () };
      Print ("Single element", res.SequenceEqual ([25]));
   }

   // Tests FIFO order.
   static void Test2 () {
      var queue = new TQueue<int> ();
      for (int i = 1; i <= 3; i++) queue.Enqueue (i * 10);
      var res = new List<int> ();
      while (!queue.IsEmpty) res.Add (queue.Dequeue ());
      Print ("FIFO order", res.SequenceEqual ([10, 20, 30]));
   }

   // Tests circular movement.
   static void Test3 () {
      var queue = new TQueue<int> ();
      for (int i = 1; i <= 4; i++) queue.Enqueue (i);
      queue.Dequeue ();
      queue.Dequeue ();
      queue.Enqueue (5);
      queue.Enqueue (6);
      var res = new List<int> ();
      while (!queue.IsEmpty) res.Add (queue.Dequeue ());
      Print ("Circular order", res.SequenceEqual ([3, 4, 5, 6]));
   }

   // Tests the empty state.
   static void Test4 () {
      var queue = new TQueue<int> ();
      queue.Enqueue (10);
      queue.Dequeue ();
      Print ("Empty state", queue.IsEmpty);
   }

   // Tests automatic resizing.
   static void Test5 () {
      var queue = new TQueue<int> ();
      for (int i = 0; i < 8; i++) queue.Enqueue (i);
      var res = new List<int> ();
      while (!queue.IsEmpty) res.Add (queue.Dequeue ());
      Print ("Resize", res.SequenceEqual ([0, 1, 2, 3, 4, 5, 6, 7]));
   }

   // Tests resizing after circular wrapping.
   static void Test6 () {
      var queue = new TQueue<int> ();
      for (int i = 1; i <= 4; i++) queue.Enqueue (i * 10);
      queue.Dequeue ();
      queue.Dequeue ();
      for (int i = 5; i <= 7; i++) queue.Enqueue (i * 10);
      var res = new List<int> ();
      while (!queue.IsEmpty) res.Add (queue.Dequeue ());
      Print ("Wrapped resize", res.SequenceEqual ([30, 40, 50, 60, 70]));
   }

   // Tests Dequeue on an empty queue.
   static void Test7 () {
      var queue = new TQueue<int> ();
      bool passed = false;
      try {
         queue.Dequeue ();
      } catch (InvalidOperationException) {
         passed = true;
      }
      Print ("Empty Dequeue", passed);
   }

   // Displays the test result.
   static void Print (string name, bool passed) {
      mTestNumber++;
      if (passed) mPassedTests++;
      ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      WriteLine ($"{mTestNumber}. {name,-20} : {(passed ? "PASS" : "FAIL")}");
      ResetColor ();
   }
   #endregion
}
#endregion
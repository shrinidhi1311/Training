// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TestCases.cs
// Compares the custom queue with the built-in Queue<int>.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class Program -----------------------------------------------------------------------------
// Tests the generic queue implementation.
class Program {
   static void Main () {
      WriteLine ("Queue Validation");
      WriteLine ("-----------------");
      Test1 ();
      Test2 ();
      Test3 ();
      Test4 ();
      Test5 ();
      Test6 ();
      WriteLine ($"\nResult: {sPassedTests}/{sTestNumber} Tests Passed");
   }

   #region Implementation -------------------------------------------
   // Tests a single element
   static void Test1 () {
      var queue = new TQueue<int> ();
      var reference = new Queue<int> ();
      Enqueue (queue, reference, 25, 25);
      Print ("Single element", Compare (queue, reference));
   }

   // Tests FIFO order
   static void Test2 () {
      var queue = new TQueue<int> ();
      var reference = new Queue<int> ();
      Enqueue (queue, reference, 1, 3);
      Print ("FIFO order", Compare (queue, reference));
   }

   // Tests circular movement
   static void Test3 () {
      var queue = new TQueue<int> ();
      var reference = new Queue<int> ();
      Enqueue (queue, reference, 1, 4);
      bool passed = CompareDequeued (queue, reference, 2);
      Enqueue (queue, reference, 5, 6);
      Print ("Circular order", Compare (queue, reference) && passed);
   }

   // Tests automatic resizing
   static void Test4 () {
      var queue = new TQueue<int> ();
      var reference = new Queue<int> ();
      Enqueue (queue, reference, 1, 8);
      Print ("Resize", Compare (queue, reference));
   }

   // Tests resizing after circular wrapping
   static void Test5 () {
      var queue = new TQueue<int> ();
      var reference = new Queue<int> ();
      Enqueue (queue, reference, 1, 4);
      bool passed = CompareDequeued (queue, reference, 2);
      Enqueue (queue, reference, 5, 7);
      Print ("Wrapped resize", Compare (queue, reference) && passed);
   }

   // Tests Dequeue on an empty queue
   static void Test6 () {
      var queue = new TQueue<int> ();
      var reference = new Queue<int> ();
      bool passed = ThrowsEmptyDequeue (queue.Dequeue) && ThrowsEmptyDequeue (reference.Dequeue);
      Print ("Empty Dequeue", passed);
   }

   // Adds a range of values to both queues
   static void Enqueue (TQueue<int> queue, Queue<int> reference, int start, int end) {
      for (int i = start; i <= end; i++) {
         queue.Enqueue (i);
         reference.Enqueue (i);
      }
   }

   // Removes and compares a specified number of elements
   static bool CompareDequeued (TQueue<int> queue, Queue<int> reference, int count) {
      bool passed = true;
      for (int i = 0; i < count; i++) passed &= queue.Dequeue () == reference.Dequeue ();
      return passed;
   }

   // Compares remaining elements and checks that both queues empty together
   static bool Compare (TQueue<int> queue, Queue<int> reference) {
      bool passed = true;
      while (!queue.IsEmpty || reference.Count > 0) {
         if (queue.IsEmpty || reference.Count == 0) {
            passed = false;
            break;
         }
         passed &= queue.Dequeue () == reference.Dequeue ();
      }
      return passed;
   }

   // Checks whether a dequeue operation throws on an empty queue
   static bool ThrowsEmptyDequeue (Func<int> dequeue) {
      try {
         dequeue ();
      } catch (InvalidOperationException) {
         return true;
      }
      return false;
   }

   // Displays the test result
   static void Print (string name, bool passed) {
      sTestNumber++;
      if (passed) sPassedTests++;
      Write ($"{sTestNumber}. {name,-22} : ");
      ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      WriteLine (passed ? "PASS" : "FAIL");
      ResetColor ();
   }
   #endregion

   #region Private data ---------------------------------------------
   static int sTestNumber, sPassedTests;
   #endregion
}
#endregion
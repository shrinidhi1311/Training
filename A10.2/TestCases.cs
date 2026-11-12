// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TestCases.cs
// Testing the double ended queue implementation.
// ------------------------------------------------------------------------------------------------
using static System.Console;

#region class Program -----------------------------------------------------------------------------
class Program {
   #region Methods --------------------------------------------------
   static void Main () {
      WriteLine ("Queue / Deque Validation");
      WriteLine ("------------------------");
      TestOrder ("Single element from Tail to Head", EDequeOrder.TailToHead, 1);
      TestOrder ("Single element from Head to Tail", EDequeOrder.HeadToTail, 1);
      TestOrder ("FIFO order", EDequeOrder.TailToHead, 3);
      TestOrder ("Circular order", EDequeOrder.TailToHead, 6, skip: 2, split: 4);
      TestOrder ("Resize", EDequeOrder.TailToHead, 8);
      TestOrder ("Wrapped resize", EDequeOrder.TailToHead, 7, skip: 2, split: 4);
      TestOrder ("Head in, tail out", EDequeOrder.HeadToTail, 3);
      TestOrder ("Head wrapped resize", EDequeOrder.HeadToTail, 7, skip: 2, split: 4);
      TestOrder ("Stack order", EDequeOrder.HeadToHead, 3);
      TestMixed ("Mixed ends with resize");
      TestEmpty ("Empty Dequeue");
      WriteLine ($"\nResult: {sPassedTests}/{sTestNumber} Tests Passed");
   }
   #endregion

   #region Implementation -------------------------------------------
   // Tests insertion and removal order for different combinations of queue ends
   static void TestOrder (string name, EDequeOrder ends, int count, int skip = 0, int split = -1) {
      if (split < 0) split = count;
      TDeque<int> queue = new ();
      Action<int> add = ends == EDequeOrder.TailToHead ? queue.EnqueueTail : queue.EnqueueHead;
      Func<int> remove = ends == EDequeOrder.HeadToTail ? queue.DequeueTail : queue.DequeueHead;
      for (int i = 0; i < split; i++) add (sData[i]);
      for (int i = 0; i < skip; i++) remove ();
      for (int i = split; i < count; i++) add (sData[i]);
      int[] expected = sData[skip..count];
      if (ends == EDequeOrder.HeadToHead) Array.Reverse (expected);
      bool passed = true;
      foreach (int value in expected) passed &= remove () == value;
      Print (name, passed && queue.IsEmpty);
   }

   // Tests interleaved insertion and removal operations at both ends
   static void TestMixed (string name) {
      TDeque<int> queue = new ();
      bool passed = true;
      queue.EnqueueTail (1);
      queue.EnqueueHead (2);
      passed &= queue.DequeueTail () == 1;
      queue.EnqueueTail (3);
      passed &= queue.DequeueHead () == 2;
      queue.EnqueueHead (4);
      queue.EnqueueTail (5);
      queue.EnqueueHead (6);
      queue.EnqueueTail (7);
      passed &= queue.DequeueTail () == 7;
      passed &= queue.DequeueHead () == 6;
      passed &= queue.DequeueTail () == 5;
      passed &= queue.DequeueHead () == 4;
      passed &= queue.DequeueHead () == 3;
      Print (name, passed && queue.IsEmpty);
   }

   // Tests whether dequeue operations throw InvalidOperationException when the queue is empty
   static void TestEmpty (string name) {
      TDeque<int> queue = new ();
      bool passed = ThrowsEmptyDequeue (queue.DequeueHead) && ThrowsEmptyDequeue (queue.DequeueTail);
      queue.EnqueueTail (sData[0]);
      queue.DequeueHead ();
      passed &= ThrowsEmptyDequeue (queue.DequeueHead) && ThrowsEmptyDequeue (queue.DequeueTail);
      Print (name, passed);
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

   // Displays the test result and updates the test counters
   static void Print (string name, bool passed) {
      sTestNumber++;
      if (passed) sPassedTests++;
      Write ($"{sTestNumber, -2}. {name, -32} : ");
      ForegroundColor = passed ? ConsoleColor.Green : ConsoleColor.Red;
      WriteLine (passed ? "PASS" : "FAIL");
      ResetColor ();
   }
   #endregion

   #region Private Data ---------------------------------------------
   static int sTestNumber, sPassedTests;
   static readonly int[] sData = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
   #endregion

   #region Enum EDequeOrder -----------------------------------------
   enum EDequeOrder { TailToHead, HeadToTail, HeadToHead }
   #endregion
}
#endregion
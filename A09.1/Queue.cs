// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// Queue.cs
// Generic queue implementation using a circular buffer.
// ------------------------------------------------------------------------------------------------

#region Class TQueue ------------------------------------------------------------------------------
//Implements a generic FIFO queue using an array.
public class TQueue<T> {
   #region Properties -----------------------------------------------
   //Gets whether the queue is empty.
   public bool IsEmpty => mCount == 0;
   #endregion

   #region Methods --------------------------------------------------
   //Adds an element to the end of the queue.
   public void Enqueue (T value) {
      if (mCount == mItems.Length) Resize ();
      int index = (mHead + mCount) % mItems.Length;
      mItems[index] = value;
      mCount++;
   }

   //Removes and returns the first element.
   public T Dequeue () {
      if (mCount == 0) throw new InvalidOperationException ("Queue is empty");
      T value = mItems[mHead];
      mItems[mHead] = default!;
      mHead = (mHead + 1) % mItems.Length;
      mCount--;
      return value;
   }
   #endregion

   #region Implementation -------------------------------------------
   // Creates a larger array and preserves FIFO order.
   void Resize () {
      var newItems = new T[mItems.Length * 2];
      for (int i = 0; i < mCount; i++) newItems[i] = mItems[(mHead + i) % mItems.Length];
      mItems = newItems;
      mHead = 0;
   }
   #endregion

   #region Fields ---------------------------------------------------
   T[] mItems = new T[4];
   int mHead;
   int mCount;
   #endregion
}
#endregion
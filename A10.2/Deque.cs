// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TDeque.cs
// Double Ended Queue implementation with circular buffer.
// ------------------------------------------------------------------------------------------------

#region class TDeque ------------------------------------------------------------------------------
/// <summary>Implements a generic FIFO queue using a circular buffer</summary>
public class TDeque<T> {
   #region Properties -----------------------------------------------
   /// <summary>Gets whether the queue is empty</summary>
   public bool IsEmpty => mCount == 0;
   #endregion

   #region Methods --------------------------------------------------
   ///<summary>Adds an element in the front</summary>
   public void EnqueueHead (T value) {
      if (mCount == mItems.Length) Resize ();
      mHead = (mHead - 1 + mItems.Length) % mItems.Length;
      mItems[mHead] = value;
      mCount++;
   }

   /// <summary>Adds an element to the end of the queue</summary>
   public void EnqueueTail (T value) {
      if (mCount == mItems.Length) Resize ();
      int index = (mHead + mCount) % mItems.Length;
      mItems[index] = value;
      mCount++;
   }

   /// <summary>Removes and returns the element at the head</summary>
   public T DequeueHead () {
      if (mCount == 0) throw new InvalidOperationException ("Queue is empty");
      T value = mItems[mHead];
      mItems[mHead] = default!;
      mHead = (mHead + 1) % mItems.Length;
      mCount--;
      return value;
   }

   /// <summary>Removes and returns the element at the tail</summary>
   public T DequeueTail () {
      if (mCount == 0) throw new InvalidOperationException ("Queue is empty");
      int index = (mHead + mCount - 1) % mItems.Length;
      T value = mItems[index];
      mItems[index] = default!;
      mCount--;
      return value;
   }
   #endregion

   #region Implementation -------------------------------------------
   // Creates a larger array and preserves FIFO order
   void Resize () {
      var newItems = new T[mItems.Length * 2];
      for (int i = 0; i < mCount; i++)
         newItems[i] = mItems[(mHead + i) % mItems.Length];
      mItems = newItems;
      mHead = 0;
   }
   #endregion

   #region Private data ---------------------------------------------
   T[] mItems = new T[4];
   int mHead, mCount;
   #endregion
}
#endregion

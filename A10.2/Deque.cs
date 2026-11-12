// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2026.
// Copyright (c) TRUMPF Metamation India.
// ------------------------------------------------------------------------------------------------
// TDeque.cs
// Double Ended Queue implementation with circular buffer.
// ------------------------------------------------------------------------------------------------

#region class TDeque ------------------------------------------------------------------------------
/// <summary>Implements a generic double-ended queue using a circular buffer</summary>
public class TDeque<T> {
   #region Properties -----------------------------------------------
   /// <summary>Gets whether the queue is empty</summary>
   public bool IsEmpty => mCount == 0;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Adds an element at the head (front) of the deque</summary>
   public void EnqueueHead (T value) => Enqueue (value, EState.Head);

   /// <summary>Adds an element at the tail (end) of the deque</summary>
   public void EnqueueTail (T value) => Enqueue (value, EState.Tail);

   /// <summary>Removes and returns the element at the head</summary>
   public T DequeueHead () => Dequeue (EState.Head);

   /// <summary>Removes and returns the element at the tail</summary>
   public T DequeueTail () => Dequeue (EState.Tail);
   #endregion

   #region Implementation -------------------------------------------
   // Inserts an element at the specified end of the deque
   void Enqueue (T value, EState state) {
      if (mCount == mItems.Length) Resize ();
      int len = mItems.Length;
      if (state is EState.Head) {
         mHead = (mHead - 1 + len) % len;
         mItems[mHead] = value;
      } else {
         mItems[mTail] = value;
         mTail = (mTail + 1) % len;
      }
      mCount++;
   }

   // Removes and returns an element from the specified end of the deque
   T Dequeue (EState state) {
      if (mCount == 0) throw new InvalidOperationException ("Queue is empty");
      int len = mItems.Length;
      T value;
      if (state is EState.Head) {
         value = mItems[mHead];
         mItems[mHead] = default!;
         mHead = (mHead + 1) % len;
      } else {
         mTail = (mTail - 1 + len) % len;
         value = mItems[mTail];
         mItems[mTail] = default!;
      }
      mCount--;
      return value;
   }

   // Doubles the capacity, copies the elements in order starting at index 0
   void Resize () {
      int len = mItems.Length;
      var newItems = new T[len * 2];
      for (int i = 0; i < mCount; i++)
         newItems[i] = mItems[(mHead + i) % len];
      (mItems, mHead, mTail) = (newItems, 0, mCount);
   }
   #endregion

   #region Private data ---------------------------------------------
   T[] mItems = new T[4];
   int mHead, mCount, mTail;  // mTail = index of the next free slot at the end
   #endregion

   #region Enum EState ----------------------------------------------
   enum EState {
      Head, Tail
   }
   #endregion
}
#endregion
// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Tokenizer.cs
// Converts an input expression into tokens
// ------------------------------------------------------------------------------------------------
using System.Globalization;

namespace Eval;

#region class Tokenizer ---------------------------------------------------------------------------
class Tokenizer {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes the tokenizer with an evaluator and an expression</summary>
   public Tokenizer (Evaluator eval, string text) {
      mText = text; mN = 0; mEval = eval;
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the next token from the expression</summary>
   public Token Next () => mPrev = NextToken ();
   #endregion

   #region Implementation -------------------------------------------
   // Reads the next token from the text
   Token NextToken () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         switch (ch) {
            case ' ' or '\t': continue;
            case (>= '0' and <= '9') or '.':
               return GetNumber ();
            case '(' or ')':
               return new TPunctuation (ch);
            case '-' or '+' when mPrev is null or TOperator or TPunctuation { Punct: '(' }:
               return new TOpUnary (ch);
            case '+' or '-' or '*' or '/' or '^' or '=':
               return new TOpArithmetic (ch);
            case >= 'a' and <= 'z':
               return GetIdentifier ();
            default:
               return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }

   // Reads an identifier and returns a function or variable token
   Token GetIdentifier () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN]);
         if (ch is >= 'a' and <= 'z') {
            mN++;
            continue;
         }
         break;
      }
      string sub = mText[start..mN].ToLowerInvariant ();
      if (TOpFunction.IsFunction (sub)) return new TOpFunction (sub);
      int i = mN;
      while (i < mText.Length && mText[i] is ' ' or '\t') i++;
      if (i < mText.Length && mText[i] == '(') return new TError ($"Unknown function: {sub}");
      return new TVariable (mEval, sub);
   }

   // Reads a number and returns a literal or an error token
   Token GetNumber () {
      int start = mN - 1;
      while (mN < mText.Length) {
         char ch = mText[mN];
         if (ch is (>= '0' and <= '9') or '.') {
            mN++;
            continue;
         }
         break;
      }
      // Now, mN points to the first character of mText that is not part of the number
      string sub = mText[start..mN];
      if (double.TryParse (sub, NumberStyles.Float, CultureInfo.InvariantCulture, out double f))
         return new TLiteral (f);
      return new TError ($"Invalid number: {sub}");
   }
   #endregion

   #region Private Data ---------------------------------------------
   readonly Evaluator mEval; // The evaluator that owns this
   readonly string mText;    // The input text we're parsing through
   int mN;
   Token? mPrev;             // Last token returned; decides whether '-' is unary or binary
   #endregion
}
#endregion
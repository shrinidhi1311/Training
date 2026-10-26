// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Tokenizer.cs
// Converts an input expression into tokens
// ------------------------------------------------------------------------------------------------
namespace Eval;

#region class Tokenizer ---------------------------------------------------------------------------
class Tokenizer {
   #region Constructor ----------------------------------------------
   /// <summary>Initializes the tokenizer with an evaluator and an expression</summary>
   public Tokenizer (Evaluator eval, string text) {
      mText = text;
      mN = 0;
      mEval = eval;
   }
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the next token from the expression</summary>
   public Token Next () {
      while (mN < mText.Length) {
         char ch = char.ToLower (mText[mN++]);
         switch (ch) {
            case ' ' or '\t':
               continue;
            case (>= '0' and <= '9') or '.':
               mExpectOperand = false;
               return GetNumber ();
            case '(':
               mExpectOperand = true;
               return new TPunctuation (ch);
            case ')':
               mExpectOperand = false;
               return new TPunctuation (ch);
            case '-' or '+' when mExpectOperand:
               return new TOpUnary (mEval, ch);
            case '+' or '-' or '*' or '/' or '^' or '=':
               mExpectOperand = true;
               return new TOpArithmetic (mEval, ch);
            case >= 'a' and <= 'z': {
                  Token token = GetIdentifier ();
                  mExpectOperand = token is TOpFunction;
                  return token;
               }
            default:
               return new TError ($"Unknown symbol: {ch}");
         }
      }
      return new TEnd ();
   }
   #endregion

   #region  Implementation-------------------------------------------
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
      string sub = mText[start..mN];
      if (sFuncs.Contains (sub)) return new TOpFunction (mEval, sub);
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
      string sub = mText[start..mN];
      if (double.TryParse (sub, out double f)) return new TLiteral (f);
      return new TError ($"Invalid number: {sub}");
   }
   #endregion

   #region Private data ---------------------------------------------
   readonly Evaluator mEval;
   readonly string mText;
   int mN;
   bool mExpectOperand = true;
   static readonly HashSet<string> sFuncs = [
      "sin", "cos", "tan", "sqrt", "log", "exp", "asin", "acos", "atan"
   ];
   #endregion
}
#endregion
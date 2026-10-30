// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Evaluator.cs
// Evaluates arithmetic expressions and variable assignments
// ------------------------------------------------------------------------------------------------
namespace Eval;

#region class EvalException -----------------------------------------------------------------------

class EvalException : Exception {
   /// <summary>Initializes an exception with an error message</summary>
   public EvalException (string message) : base (message) { }
}
#endregion

#region class Evaluator ---------------------------------------------------------------------------
/// <summary>Evaluates expressions with operators, functions, and variable assignments</summary>
class Evaluator {
   #region Methods --------------------------------------------------
   public double Evaluate (string text) {
      mOperands.Clear ();
      mOperators.Clear ();
      mBasePriority = 0;
      List<Token> tokens = [];
      var tokenizer = new Tokenizer (this, text);
      for (; ; ) {
         var token = tokenizer.Next ();
         if (token is TEnd) break;
         if (token is TError err) throw new EvalException (err.Message);
         tokens.Add (token);
      }

      // Check if this is a variable assignment
      TVariable? tVariable = null;
      if (tokens.Count > 2 && tokens[0] is TVariable tvar &&
          tokens[1] is TOpArithmetic { Op: '=' }) {
         tVariable = tvar;
         tokens.RemoveRange (0, 2);
      }
      foreach (var token in tokens) Process (token);
      while (mOperators.Count > 0) ApplyOperator ();
      if (mOperands.Count != 1) throw new EvalException ("Invalid expression");
      if (mBasePriority != 0) throw new EvalException ("Mismatched Parentheses");
      double result = mOperands.Pop ();
      if (tVariable != null) mVars[tVariable.Name] = result;
      return result;
   }

   /// <summary>Returns the value stored for a variable</summary>
   public double GetVariable (string name) {
      if (mVars.TryGetValue (name, out double f)) return f;
      throw new EvalException ($"Unknown variable: {name}");
   }
   #endregion

   #region Implementation -------------------------------------------
   // Processes a token and performs the required stack operation
   void Process (Token token) {
      switch (token) {
         case TNumber num:
            mOperands.Push (num.Value);
            break;
         case TOperator op:
            op.FinalPriority = op.Priority + mBasePriority;
            // Unary minus must not reduce an operator already on the stack
            if (op is not (TOpUnary or TOpFunction))
               while (!ShouldPush (op)) ApplyOperator ();
            mOperators.Push (op);
            break;
         case TPunctuation p:
            mBasePriority += p.Punct == '(' ? PARENPRIORITY : -PARENPRIORITY;
            if (mBasePriority < 0) throw new EvalException ("Mismatched Parentheses");
            break;
         default:
            throw new EvalException ($"Unknown token: {token}");
      }
   }

   // Pops an operator and evaluates it using the operand stack
   void ApplyOperator () {
      var op = mOperators.Pop ();
      switch (op) {
         case TOpUnary unary:
            if (mOperands.Count < 1) throw new EvalException ("Missing operand for unary minus");
            mOperands.Push (unary.Evaluate (mOperands.Pop ()));
            break;
         case TOpFunction func:
            if (mOperands.Count < 1) throw new EvalException ("Missing function argument");
            mOperands.Push (func.Evaluate (mOperands.Pop ()));
            break;
         case TOpArithmetic arith:
            if (mOperands.Count < 2) throw new EvalException ("Missing operand");
            double right = mOperands.Pop ();
            double left = mOperands.Pop ();
            mOperands.Push (arith.Evaluate (left, right));
            break;
      }
   }

   // Determines whether the operator can be pushed onto the operator stack
   bool ShouldPush (TOperator op) {
      if (mOperators.Count == 0) return true;
      TOperator prev = mOperators.Peek ();
      return prev.FinalPriority < op.FinalPriority ||
         (prev.FinalPriority == op.FinalPriority && op is TOpArithmetic { Op: '^'});
   }
   #endregion

   #region Private Data ---------------------------------------------
   readonly Dictionary<string, double> mVars = [];
   readonly Stack<double> mOperands = new ();
   readonly Stack<TOperator> mOperators = new ();
   const int PARENPRIORITY = 10;   // Must exceed the highest operator priority
   int mBasePriority;
   #endregion
}
#endregion
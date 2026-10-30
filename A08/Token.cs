// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch- July 2026.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------------------------------------
// Token.cs
// Defines token types used by the expression evaluator
// ------------------------------------------------------------------------------------------------
namespace Eval;

#region class Token -------------------------------------------------------------------------------
abstract class Token {
}
#endregion

#region class TNumber -----------------------------------------------------------------------------
abstract class TNumber : Token {
   #region Properties -----------------------------------------------
   /// <summary>Gets the numeric value represented by the token</summary>
   public abstract double Value { get; }
   #endregion
}
#endregion

#region class TLiteral ----------------------------------------------------------------------------
class TLiteral : TNumber {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes a numeric literal</summary>
   public TLiteral (double f) => mValue = f;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the literal numeric value</summary>
   public override double Value => mValue;
   #endregion

   #region Methods --------------------------------------------------
   public override string ToString () => $"literal:{Value}";
   #endregion

   #region Private Data ---------------------------------------------
   readonly double mValue;
   #endregion
}
#endregion

#region class TVariable ---------------------------------------------------------------------------
class TVariable : TNumber {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes a variable token</summary>
   public TVariable (Evaluator eval, string name) => (Name, mEval) = (name, eval);
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the variable name</summary>
   public string Name { get; private set; }
   /// <summary>Gets the current value of the variable</summary>
   public override double Value => mEval.GetVariable (Name);
   #endregion

   #region Methods --------------------------------------------------
   public override string ToString () => $"var:{Name}";
   #endregion

   #region Private Data ---------------------------------------------
   readonly Evaluator mEval;
   #endregion
}
#endregion

#region class TOperator ---------------------------------------------------------------------------
abstract class TOperator : Token {
   #region Properties -----------------------------------------------
   /// <summary>Gets the operator precedence</summary>
   public abstract int Priority { get; }
   public int FinalPriority { get; set; }
   #endregion
}
#endregion

#region class TOpArithmetic -----------------------------------------------------------------------
class TOpArithmetic : TOperator {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes an arithmetic operator</summary>
   public TOpArithmetic (char ch) => Op = ch;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the arithmetic operator symbol</summary>
   public char Op { get; private set; }
   /// <summary>Gets the precedence of the arithmetic operator</summary>
   public override int Priority => sPriority[Op];
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the operator symbol and its precedence</summary>
   public override string ToString () => $"op:{Op}:{Priority}";

   /// <summary>Evaluates an arithmetic operation using two operands</summary>
   public double Evaluate (double a, double b) => Op switch {
      '+' => a + b,
      '-' => a - b,
      '*' => a * b,
      '/' => b == 0 ? throw new EvalException ("Division by zero") : a / b,
      '^' => Math.Pow (a, b),
      _ => throw new EvalException ($"Unknown operator: {Op}")
   };
   #endregion

   #region Private Data ---------------------------------------------
   static readonly Dictionary<char, int> sPriority = new () {
      ['='] = 0,
      ['+'] = 1,
      ['-'] = 1,
      ['*'] = 2,
      ['/'] = 2,
      ['^'] = 4
   };
   #endregion
}
#endregion

#region class TOpUnary ----------------------------------------------------------------------------
class TOpUnary : TOperator {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes a unary operator</summary>
   public TOpUnary (char ch) => Op = ch;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the unary operator symbol</summary>
   public char Op { get; private set; }
   /// <summary>Gets the precedence of unary minus</summary>
   public override int Priority => 3;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Evaluates a unary plus or minus operation</summary>
   public double Evaluate (double f) => Op == '+' ? f : -f;
   public override string ToString () => $"unary:{Op}:{Priority}";
   #endregion
}
#endregion

#region class TOpFunction -------------------------------------------------------------------------
class TOpFunction : TOperator {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes a function operator</summary>
   public TOpFunction (string name) => Func = name;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the function name</summary>
   public string Func { get; private set; }
   /// <summary>Gets the function operator precedence; must exceed '^' so sin(30)^2 = (sin 30)^2</summary>
   public override int Priority => 6;
   #endregion

   #region Methods --------------------------------------------------
   /// <summary>Returns the function name and its precedence</summary>
   public override string ToString () => $"func:{Func}:{Priority}";

   public double Evaluate (double f) => Func switch {
      "sin" => Math.Sin (D2R (f)),
      "cos" => Math.Cos (D2R (f)),
      "tan" => Math.Tan (D2R (f)),
      "sqrt" => f < 0 ? throw new EvalException ("sqrt requires a non-negative value")
                      : Math.Sqrt (f),
      "log" => f <= 0 ? throw new EvalException ("log requires a value greater than zero")
                      : Math.Log (f),
      "exp" => Math.Exp (f),
      "asin" => R2D (Math.Asin (f)),
      "acos" => R2D (Math.Acos (f)),
      "atan" => R2D (Math.Atan (f)),
      _ => throw new EvalException ($"Unknown function: {Func}")
   };

   /// <summary>Converts degrees to radians</summary>
   static double D2R (double f) => f * Math.PI / 180;

   /// <summary>Converts radians to degrees</summary>
   static double R2D (double f) => f * 180 / Math.PI;
   #endregion
}
#endregion

#region class TPunctuation ------------------------------------------------------------------------
class TPunctuation : Token {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes a punctuation token</summary>
   public TPunctuation (char ch) => Punct = ch;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the punctuation character</summary>
   public char Punct { get; private set; }
   #endregion

   #region Methods --------------------------------------------------
   public override string ToString () => $"punct:{Punct}";
   #endregion
}
#endregion

#region class TEnd --------------------------------------------------------------------------------
class TEnd : Token {
   #region Methods --------------------------------------------------
   public override string ToString () => "end";
   #endregion
}
#endregion

#region class TError ------------------------------------------------------------------------------
class TError : Token {
   #region Constructors ---------------------------------------------
   /// <summary>Initializes an error token with a message</summary>
   public TError (string message) => Message = message;
   #endregion

   #region Properties -----------------------------------------------
   /// <summary>Gets the error message</summary>
   public string Message { get; private set; }
   #endregion

   #region Methods --------------------------------------------------
   public override string ToString () => $"error:{Message}";
   #endregion
}
#endregion
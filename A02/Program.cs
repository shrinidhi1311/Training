using static System.Console;
class Program {
   static void Main () {
      int n = new Random ().Next (1, 101), attempts = 1,guess;
      Print ("Guess a number from 1 to 100:");
      while (attempts <= 7) {
         guess = ReadGuess ();
         if (guess == n) break;
         Print ($"Too {(guess < n ? "low" : "high")}! Try again.\nGuess again:");
         attempts++;
      }
      if (attempts <= 7) Print ($"You guessed it!\nAttempts: {attempts}\nPress any key...");
      else Print ($"No attempts left!\nCorrect Number: {n}\nPress any key...");
      ReadKey ();
   }

   static int ReadGuess () {
      int guess;
      while (!int.TryParse (ReadLine (), out guess) || guess < 1 || guess > 100) Print ("Invalid input.Try Again:");
      return guess;
   }

   static void Print (string message) => WriteLine (message);
}
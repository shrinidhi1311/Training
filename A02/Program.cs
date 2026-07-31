using static System.Console;
class Program {
   static void Main () {
      int randomNumber = new Random ().Next (1, 101), attempts = 1, guess;
      Print ("Guess a number from 1 to 100:");
      guess = ReadGuess ();
      while (guess != randomNumber) {
         if (guess < randomNumber) Print ("Too low! Try again.");
         else Print ("Too high! Try again.");
         Print ("Guess again:");
         guess = ReadGuess ();
         attempts++;
      }
      Print ($"Congrats on guessing the correct number!\nTotal attempts taken: {attempts}\nPress any key to exit.");
      ReadKey ();
   }

   static int ReadGuess () {
      int guess;
      while (!int.TryParse (ReadLine (), out guess))
         Print ("Invalid input. Please enter a valid number.");
      return guess;
   }

   static void Print (string message) {
      WriteLine (message);
   }
}
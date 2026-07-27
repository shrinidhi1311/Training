using System;

class Program {
   static void Main () {
      int n = new Random ().Next (1, 101);
      int attempts = 1;

      Console.WriteLine ("Guess a number from 1 to 100:");
      int guess = int.Parse (Console.ReadLine ());

      while (guess != n) {
         if (guess < n) {
            Console.WriteLine ("Too low! Try again.");
         } else {
            Console.WriteLine ("Too high! try again.");
         }

         if (attempts >= 7) {
            Console.WriteLine ("Too many attempts!");
            Console.WriteLine ($"The correct number was {n}.");
            Console.WriteLine ("Press any key to exit...");
            Console.ReadKey ();
            return;
         }

         Console.WriteLine ("Guess a number:");
         guess = int.Parse (Console.ReadLine ());
         attempts++;
      }

      Console.WriteLine ("Congrats! You guessed the number correctly!");
      Console.WriteLine ($"You guessed it in {attempts} attempt(s).");
      Console.WriteLine ("Press any key to exit...");
      Console.ReadKey ();
   }
}
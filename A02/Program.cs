using System;
class Program {
   static void Main () {
      int n = new Random ().Next (1, 101);

      Console.WriteLine ("Guess a number from 1 to 100:");
      int guess = int.Parse (Console.ReadLine ());
      while (guess != n) {
         if ((guess < n)) {
            Console.WriteLine ("too low! Try again.");
         } else {
            Console.WriteLine ("too high!Try again");
         }
         Console.WriteLine ("guess a number :");
         guess = int.Parse (Console.ReadLine ());
      }
      Console.WriteLine ("Congrats! You guessed the number correctly");
      Console.WriteLine ("Press any key to exit...");
      Console.ReadKey ();
   }
}
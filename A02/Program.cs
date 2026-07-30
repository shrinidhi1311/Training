using System;
class Program {
   static void Main () {
      int n = new Random ().Next (1, 101), attempts = 1;
      print ("Guess a number from 1 to 100:");
      int guess = int.Parse (Console.ReadLine ());
      while (guess != n) {
         if (guess < n) {
            print ("Too low! Try again.");
         } else {
            print ("Too high! Try again.");
         }
         print ("Guess again:");
         guess = int.Parse (Console.ReadLine ());
         attempts++;
      }
      if (guess == n) {
         print ("Congrats on guessing the correct number!");
         print ($"Total attempts taken : {attempts}");
      } else {
         print ("Too many attempts!");
         print ($"The correct number was {n}");
      }
      print ("press any key to exit.");
      Console.ReadKey ();
   }
   static void print (string display) {
      Console.WriteLine (display);
   }
}

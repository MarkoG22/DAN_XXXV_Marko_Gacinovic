using System;
using System.Threading;

namespace Zadatak_1
{
    class Program
    {
        // two static variables for inputs
        static uint players;
        static uint number;

        // random object and variable for getting the random number
        static Random rnd = new Random();
        static int num;

        // object for locking thread
        static readonly object locker = new object();

        static void Main(string[] args)
        {
            string option = null;

            // loop for the Main Menu
            do
            {
                // thread sleep to avoid overlapping
                Thread.Sleep(500);

                Console.WriteLine("\nWELCOME");
                Console.WriteLine("1. Play the game");
                Console.WriteLine("2. Exit");
                Console.Write("Please choose an option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        // two threads for two methods
                        Thread play = new Thread(() => Play());
                        Thread Thread_Generator = new Thread(() => CreatePlayers(players));
                        // starting the threads
                        play.Start();
                        play.Join();
                        Thread_Generator.Start();
                        Thread_Generator.Join();
                        break;
                    case "2":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please choose option 1 or 2.");
                        break;
                }
            } while (!option.Equals("2"));

            Console.ReadLine();
        }        

        /// <summary>
        /// method for entering number of players and guessing number
        /// </summary>
        static void Play()
        {
            // inputs and validations
            Console.Write("Please enter the number of players: ");
            bool validPlayers = uint.TryParse(Console.ReadLine(), out players);

            while (!validPlayers)
            {
                Console.Write("Wrong input, please try again: ");
                validPlayers = uint.TryParse(Console.ReadLine(), out players);
            }

            Console.Write("Please enter the guessing number (1-100): ");
            bool validNumber = uint.TryParse(Console.ReadLine(), out number);

            while (!validNumber || !(number<101))
            {
                Console.Write("Wrong input, please try again: ");
                validNumber = uint.TryParse(Console.ReadLine(), out number);
            }

            Console.WriteLine("\nUser entered {0} players and guessing number is {1}.", players, number);            
        }

        /// <summary>
        /// method for creating threads for each player
        /// </summary>
        /// <param name="players"></param>
        static void CreatePlayers(uint players)
        {
            for (int i = 0; i < players; i++)
            {
                // creating thread for the player
                Thread t = new Thread(() => GuessTheNumber(number));
                t.Name = "Player_" + (i + 1);
                // starting the thread
                t.Start();                
            }
        }

        /// <summary>
        /// method for guessing the number
        /// </summary>
        /// <param name="number"></param>
        static void GuessTheNumber(uint number)
        {
            Thread.Sleep(100);            

            // loop for guessing the number until it's guessed
            while (num != number)
            {
                // locking the thread
                Monitor.Enter(locker);

                // getting the random number
                num = rnd.Next(0, 101);

                Console.WriteLine("\n{0} tried to guess with number {1}.", Thread.CurrentThread.Name, num);

                // checking if parity is guessed
                if (number % 2 == num % 2)
                {
                    Console.WriteLine("{0} guessed the parity of the guessing number!", Thread.CurrentThread.Name);
                }

                // checking if the number is guessed
                // if yes - writing the message
                // if no - opening locker for the next thread
                if (num == number)
                {
                    Console.WriteLine("\nCongratulations {0}! You won the game, guessing number was: {1}", Thread.CurrentThread.Name, number);
                }
                else
                {
                    Monitor.Exit(locker);
                }
            }
           
        }
    }
}

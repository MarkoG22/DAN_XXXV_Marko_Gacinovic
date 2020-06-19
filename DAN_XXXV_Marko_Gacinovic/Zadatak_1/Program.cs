using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static uint players = 0;
        static uint number = 0;

        static Random rnd = new Random();
        static int num;

        static readonly object locker = new object();

        static void Main(string[] args)
        {
            string option = null;  

            
            do
            {
                Thread.Sleep(500);

                Console.WriteLine("\nWELCOME");
                Console.WriteLine("1. Play the game");
                Console.WriteLine("2. Exit");
                Console.Write("Please choose an option: ");
                option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        Thread play = new Thread(() => Play());                        
                        Thread Thread_Generator = new Thread(() => CreatePlayers(players));
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

        static void Play()
        {
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

        static void CreatePlayers(uint players)
        {
            for (int i = 0; i < players; i++)
            {
                Thread t = new Thread(() => GuessTheNumber(number));
                t.Name = "Player_" + (i + 1);
                t.Start();
                
            }
        }

        static void GuessTheNumber(uint number)
        {
            Thread.Sleep(100);            

            while (num != number)
            {
                Monitor.Enter(locker);

                num = rnd.Next(0, 101);

                Console.WriteLine("\n{0} tried to guess with number {1}.", Thread.CurrentThread.Name, num);

                if (number % 2 == num % 2)
                {
                    Console.WriteLine("{0} guessed the parity of the guessing number!", Thread.CurrentThread.Name);
                }

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

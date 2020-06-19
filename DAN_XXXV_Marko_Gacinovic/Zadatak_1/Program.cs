using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak_1
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.ReadLine();
        }

        static void Play()
        {
            Console.Write("Please enter the number of players: ");
            bool validPlayers = uint.TryParse(Console.ReadLine(), out uint players);

            while (!validPlayers)
            {
                Console.Write("Wrong input, please try again: ");
                validPlayers = uint.TryParse(Console.ReadLine(), out players);
            }

            Console.Write("Please enter the guessing number (1-100): ");
            bool validNumber = uint.TryParse(Console.ReadLine(), out uint number);

            while (!validNumber || !(number<101))
            {
                Console.Write("Wrong input, please try again: ");
                validNumber = uint.TryParse(Console.ReadLine(), out number);
            }

            Console.WriteLine("User entered {0} players and guessing number is {1}.", players, number);
        }
    }
}

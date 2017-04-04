using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeorMoreConsole {
    class Program {
        static void Main(string[] args) {
            Console.Write("Enter the number of players: ");
            int playerNum = Convert.ToInt32(Console.ReadLine());
            List<Player> players = new List<Player>();
            for(int i = 0; i < playerNum) {
                Console.Write("\nEnter name for player 1: ");
                Player player = new Player(Console.ReadLine());
                players.Add(player);
            }

            const int NUMBER_OF_DICE = 5;

        }
    }
}

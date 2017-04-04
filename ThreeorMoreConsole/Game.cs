using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeorMoreConsole {
    class Game {
        private Die[] dice;
        private Queue<Player> players;
        private List<HistoryEntry> history;
        private bool gameOver;
        private int turnNumber;
        private int WINNING_SCORE;

        public Game(int scoreToWin, Player[] players, Die[] dice) {
            if(scoreToWin <= 0) {
                throw new Exception("Score needed to win must be a positive integer greater than zero.");
            }
            this.WINNING_SCORE = scoreToWin;
            this.turnNumber = 1;
            this.gameOver = false;
            history = new List<HistoryEntry>();
            history.Add(new HistoryEntry("Game Start"));

            foreach(Player player in players) {
                this.players.Enqueue(player);
            }

            this.dice = dice;
        }

        public void startGame() {
            do {
                nextTurn();
            } while (!gameOver);
        }

        private void nextTurn() {
            Player activePlayer = players.Dequeue();
            do {
                getDiceToRollNext();
            } while (!allDiceRolled());
        }


        private bool allDiceRolled() {
            foreach(Die die in dice) {
                if (!die.Rolled) {
                    return false;
                }
            }
            return true;
        }

        private int getDiceToRollNext() {
            Console.WriteLine("Select which dice to roll next: ");
            List<int> validDice = new List<int>();
            for(int i = 0; i<dice.Length;i++) {
                if (!dice[i].Rolled) {
                    Console.WriteLine("Die ({0})",i);
                }
            }
           
            bool valid = false;
            int dieNum = 0;
            do {
                Console.Write("\nEnter a number: ");
                string input = Console.ReadLine();
                
                valid = int.TryParse(input, out dieNum);
                if (valid) {
                    valid = validDice.Contains(dieNum);
                }
                if (!valid) {
                    Console.WriteLine("Invalid choice...");
                }
            } while (!valid);
            return dieNum;
        }
    }
}

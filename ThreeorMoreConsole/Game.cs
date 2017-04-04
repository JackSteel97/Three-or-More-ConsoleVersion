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
                int die = getDiceToRollNext();
                dice[die].roll();              
            } while (!allDiceRolled());

            bool reroll;
            activePlayer.Points += analyseDiceForScore(countDiceValues(), out reroll);
        }

        private Dictionary<int, int> countDiceValues() {
            Dictionary<int, int> numberOccurrences = new Dictionary<int, int>();
            foreach(Die die in dice) {
                int value = die.Value;
                if (numberOccurrences.ContainsKey(value)) {
                    numberOccurrences[value]++;
                } else {
                    numberOccurrences.Add(value, 1);
                }
            }
            return numberOccurrences;
        }

        private int analyseDiceForScore(Dictionary<int, int> numberOccurrences, out bool reroll) {
            reroll = false;
            if (numberOccurrences.ContainsValue(5)) {
                //5 of a kind
                return 12;
            } else if (numberOccurrences.ContainsValue(4)) {
                //4 of a kind
               return 6;
            } else if (numberOccurrences.ContainsValue(3)) {
                //3 of a kind
                return 3;
            } else if (numberOccurrences.ContainsValue(2)) {
                //2 of a kind
                reroll = true;             
            }
            return 0;
        }

        private void alertToTwoMatches() {
            Console.WriteLine("You have two of a kind and may re-throw remaining dice.");
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

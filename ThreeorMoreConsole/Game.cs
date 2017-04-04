using System;
using System.Collections.Generic;


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
                outputRolledDice();
            } while (!allDiceRolled());

            bool reroll;
            Dictionary<int, int> numberOccurrences = countDiceValues();
            activePlayer.Points += analyseDiceForScore(numberOccurrences, out reroll);
            history.Add(new HistoryEntry(turnNumber, activePlayer, dice));

            if (reroll) {
                alertToTwoMatches();
                //find numbers that match
                int doubleNumber = 0;
                foreach (KeyValuePair<int, int> entry in numberOccurrences) {
                    if(entry.Value == 2) {
                        doubleNumber = entry.Key;
                    }
                }
                //reset other dice
                foreach(Die die in dice) {
                    if (die.Value != doubleNumber) {
                        die.Rolled = false;
                    }
                }
                //reroll
                do {
                    int die = getDiceToRollNext();
                    dice[die].roll();
                    outputRolledDice();
                } while (!allDiceRolled());

                numberOccurrences = countDiceValues();
                activePlayer.Points += analyseDiceForScore(numberOccurrences, out reroll, true);
                history.Add(new HistoryEntry(turnNumber, activePlayer, dice,"after re-rolling"));
            }
            if (activePlayer.Points >= WINNING_SCORE) {
                endGame(activePlayer);
            }
           
            players.Enqueue(activePlayer);
            turnNumber++;
        }

        private void endGame(Player winner) {
            gameOver = true;
            Console.WriteLine("Game Over! {0} won with a score of {1} in {2} turns.", winner.Name, winner.Points, turnNumber);
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

        private int analyseDiceForScore(Dictionary<int, int> numberOccurrences, out bool reroll, bool secondRoll = false) {
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
                if (!secondRoll) {
                    reroll = true;
                }
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

        private void outputRolledDice() {
            for(int i = 0; i<dice.Length; i++) {
                if (dice[i].Rolled) {
                    Console.WriteLine("Die {0}: {1}", i + 1,dice[i].Value);
                }
            }
        }

        private int getDiceToRollNext() {
            Console.WriteLine("Select which dice to roll next: ");
            List<int> validDice = new List<int>();
            for(int i = 0; i<dice.Length;i++) {
                if (!dice[i].Rolled) {
                    Console.WriteLine("Die ({0})",i+1);
                    validDice.Add(i + 1);
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
            return dieNum-1;
        }
    }
}

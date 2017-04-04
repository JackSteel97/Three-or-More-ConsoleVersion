using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeorMoreConsole {
    class HistoryEntry {
        private int turnNumber;
        private Player activePlayer;
        private Die[] dice;

        public int TurnNumber {
            get {
                return this.turnNumber;
            }
        }
        public Player ActivePlayer {
            get {
                return this.activePlayer;
            }
        }
        public Die[] Dice {
            get {
                return this.dice;
            }
        }

        public HistoryEntry(int turnNumber, Player activePlayer, Die[] dice) {
            if(activePlayer == null || dice == null || dice.Length == 0) {
                throw new FormatException("No parameters can be null or zero-length");
            }
            this.turnNumber = turnNumber;
            this.activePlayer = activePlayer;
            this.dice = dice;
        }

        public string getReadableFormat() {
            string output = "";
            output = string.Format("Turn: {0} \nPlayer: {2} \nDice: ", this.turnNumber, this.activePlayer.Name);
            foreach(Die die in dice) {
                output += string.Format("{0} ", die.Value);
            }
            return output;
        }

        public double getAverageofDice() {
            double total = 0;
            foreach(Die die in dice) {
                total += die.Value;
            }
            return Math.Round(total / dice.Length, 1);
        }

        public int getTotalofDice() {
            int total = 0;
            foreach (Die die in dice) {
                total += die.Value;
            }
            return total;
        }
    }
}

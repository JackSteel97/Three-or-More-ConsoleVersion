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
        private string notes;

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

        public string Notes {
            get {
                return this.notes;
            }
        }

        public HistoryEntry(int turnNumber, Player activePlayer, Die[] dice, string notes = "") {
            if(activePlayer == null || dice == null || dice.Length == 0) {
                throw new ArgumentException("No parameters can be null or zero-length");
            }
            this.turnNumber = turnNumber;
            this.activePlayer = activePlayer;
            this.dice = dice;
            this.notes = notes;
        }

        public HistoryEntry(string notes) {
            this.notes = notes;
        }

        public string getReadableFormat() {
            string output = "";
            if (turnNumber != 0) {
                output += string.Format("Turn: {0} \n", turnNumber);
            }
            if(activePlayer != null) {
                output += string.Format("Player: {0} \n", activePlayer.Name);
            }
            if (dice.Length > 0) {
                output += "Dice: ";
                foreach (Die die in dice) {
                    output += string.Format("{0} ", die.Value);
                }
            }
            if (notes.Length > 0) {
                output += string.Format("\n{0}", notes);
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

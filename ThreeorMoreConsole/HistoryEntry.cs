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

    }
}

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

            history.Add(new HistoryEntry("Game Start"));

            foreach(Player player in players) {
                this.players.Enqueue(player);
            }

            this.dice = dice;

        }
    }
}

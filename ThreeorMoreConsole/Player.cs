﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeorMoreConsole {
    class Player {
        private string name;
        private int points;

        public string Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
            
        }
        public int Points {
            get {
                return this.points;
            }
            set {
                this.points = value;
            }
        }

        public Player(string name) {
            if (name.Length == 0 || name == null) {
                throw new FormatException("Name cannot be empty or null");
            }
            this.name = name;

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreeorMoreConsole {
    class Die {
        private int value;
        private bool rolled;
        private int numberOfFaces;

        public int Value {
            get {
                if (this.rolled) {
                    return this.value;
                }else {
                    throw new InvalidOperationException();
                }
                
            }
        }
        public bool Rolled {
            get {
                return this.rolled;
            }
            set {
                this.rolled = value;
            }
        }
        public int NumberOfFaces {
            get {
                return this.numberOfFaces;
            }
            set {
                this.numberOfFaces = value;
            }
        }

        public Die(int numberOfFaces) {
            this.numberOfFaces = numberOfFaces;
            this.rolled = false;
            this.value = 1;
        }

        public int roll() {
            Random rnd = new Random();
            int rolledNum = rnd.Next(1, this.numberOfFaces);
            this.value = rolledNum;
            return rolledNum;
        }
    }
}

using AbaloneGame.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbaloneGame
{
    class AIManager
    {
        private BitSet[] Distances;
        // variables.
        private const int ValueOfGrouping = 7;
        private const int ValueOfCenterDistance = 10;
        private const int ValueOfScoredBalls = 1000;
        //Alpha Beta recursion depth
        private const int depthToSearch = 4;
        public AIManager(int player)
        {
            initializeDistances();
        }
        /**
        * program initializes and sets 5 bitsets value which help calculate each ball's distance from the center.
        */
        private void initializeDistances()
        {
            Distances = new BitSet[5];
            Distances[0] = new BitSet(109);
            Distances[1] = new BitSet(109);
            Distances[2] = new BitSet(109);
            Distances[3] = new BitSet(109);
            Distances[4] = new BitSet(109);
            //only middle
            Distances[0].set(60);
            //first circle
            Distances[1].set(48);
            Distances[1].set(49);
            Distances[1].set(61);
            Distances[1].set(59);
            Distances[1].set(71);
            Distances[1].set(72);
            //second circle
            Distances[2].set(36, 39);
            Distances[2].set(50);
            Distances[2].set(62);
            Distances[2].set(73);
            Distances[2].set(82, 85);
            Distances[2].set(70);
            Distances[2].set(58);
            Distances[2].set(47);
            //third circle
            Distances[3].set(24, 28);
            Distances[3].set(39);
            Distances[3].set(51);
            Distances[3].set(63);
            Distances[3].set(74);
            Distances[3].set(85);
            Distances[3].set(93, 97);
            Distances[3].set(81);
            Distances[3].set(69);
            Distances[3].set(57);
            Distances[3].set(46);
            Distances[3].set(35);
            //forth circle
            Distances[4].set(23);
            Distances[4].set(34);
            Distances[4].set(45);
            Distances[4].set(56);
            Distances[4].set(12, 17);
            Distances[4].set(28);
            Distances[4].set(40);
            Distances[4].set(52);
            Distances[4].set(64);
            Distances[4].set(75);
            Distances[4].set(86);
            Distances[4].set(97);
            Distances[4].set(104, 109);
            Distances[4].set(92);
            Distances[4].set(80);
            Distances[4].set(68);
            Distances[4].set(56);
        }
    }
}

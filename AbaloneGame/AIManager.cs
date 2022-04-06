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
    }
}

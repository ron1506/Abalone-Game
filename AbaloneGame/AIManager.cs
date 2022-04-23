using AbaloneGame.model;
using System;
using System.Collections;
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
        // the value given for each circle in the board.
        private const int ValueOfCenterDistance = 10;
        // the value given for a move that have a score.
        private const int ValueOfScoredBalls = 1000;
        // Alpha Beta recursion depth
        private const int depthToSearch = 2;

        /// <summary>
        /// constructor, sends to a function that initializing the distances in the board.
        /// </summary>
        /// <param name="player">the player that AI plays for</param>
        public AIManager(int player)
        {
            initializeDistances();
        }
        /// <summary>
        /// This is the program that activates main algorithm.
        /// program uses AlphaBeta and Evaluate function to find the best move of computer.
        /// </summary>
        /// <param name="board">current board to find move to.</param>
        /// <param name="currentplayer">player to find move to.</param>
        /// <returns>best move it can choose.</returns>
        public Move playTurn(Board board, int currentplayer)
        {
            //dataStruct of possible moves
            ArrayList ogBoardMoves = board.getmoves((sbyte)currentplayer); //original board's moves
            // implementing the moves to boards.
            ArrayList AllBoards = getPossibleBoards(board, ogBoardMoves);
            // starts as worst possible move ( - infinity).
            double bestValue = -1 * currentplayer * 2000000;
            double tempvalue;
            int bestindex = 0;// cnt=0;
            foreach (Board childBoard in AllBoards)
            { //goes over all the possible boards from the list.
                tempvalue = AlphaBeta(childBoard, depthToSearch, -2100000, 2100000, (sbyte)(currentplayer * -1));
                //searches for the minimum value by whites point of view. (red is AI).
                if (tempvalue < bestValue)
                {
                    bestValue = tempvalue;
                    bestindex = AllBoards.IndexOf(childBoard);
                }
            }
            return (Move)ogBoardMoves[bestindex]; //returning the best game.
        }
        /// <summary>
        /// This is the main AI recursion algorithm.
        /// uses EvaluateBoard to return the best result it can promise.
        /// if the program hasn't reached depth 1 than it calculates all possible board and calls
        /// again to the function with depth-1 and currentPlayer*-1.
        /// </summary>
        /// <param name="board"> board to calculate from.</param>
        /// <param name="depth"> current depth to search. if depth = 1 than calculates board value and return it.</param>
        /// <param name="alpha"> best board value Alpha player can promise. </param>
        /// <param name="beta"> best board value Beta player can promise. </param>
        /// <param name="currentPlayer"> player to calculate to. </param>
        /// <returns>the value of the chosen move.</returns>
        private double AlphaBeta(Board board, int depth, double alpha, double beta, sbyte currentPlayer)
        {
            //cnt++;
            // checks if needs to return the value of board.
            if (depth <= 1 || board.getScoreWhite() >= 6 || board.getScoreBlack() >= 6) // breaking point.
                return EvaluateBoard(board);
            // if 1 than try to maximize board value.
            if (currentPlayer == 1)
            {   // worst value, cannot be under -1000000.
                double value = -2000000;
                //dataStruct of possible moves
                ArrayList allBoardMoves = board.getmoves((sbyte)currentPlayer);
                ArrayList AllBoards = getPossibleBoards(board, allBoardMoves);
                foreach (Board childBoard in AllBoards)
                { // goes all over the possible boards.
                    //recursion.
                    value = Math.Max(value, AlphaBeta(childBoard, depth - 1, alpha, beta, (sbyte)-1));
                    alpha = Math.Max(value, alpha);
                    //pruning
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return value;
            }
            else
            {   // (-1) - try to minimize board value.
                // worst value, cannot be more than 1000000.
                double value = 2000000;
                //dataStruct of possible moves
                ArrayList allBoardMoves = board.getmoves((sbyte)currentPlayer);
                ArrayList AllBoards = getPossibleBoards(board, allBoardMoves);
                foreach (Board childBoard in AllBoards)
                {
                    //recursion.
                    value = Math.Min(value, AlphaBeta(childBoard, depth - 1, alpha, beta, (sbyte)1));
                    beta = Math.Min(value, alpha);
                    //pruning
                    if (alpha >= beta)
                    {
                        break;
                    }
                }
                return value;
            }
        }
        /// <summary>
        ///  program return list of all possible boards which have implemented the moves from
        /// original board and ogBoardMoves list.
        /// </summary>
        /// <param name="board">board to calculate all its child boards.</param>
        /// <param name="ogBoardMoves">list of all possible moves from originalboard - board.</param>
        /// <returns>list of all possible child boards of board.</returns>
        private ArrayList getPossibleBoards(Board board, ArrayList ogBoardMoves)
        {
            //dataStruct of boards to return
            ArrayList PossibleBoards = new ArrayList(60);
            Board tempBoard;
            int NumberOfMoves = ogBoardMoves.Count;
            //goes over each Move
            for (int i = 0; i < NumberOfMoves; i++)
            {
                tempBoard = new Board();
                tempBoard.cloneBoard(board);
                tempBoard.makeMove((Move)ogBoardMoves[i]); // check on an alternative
                PossibleBoards.Add(tempBoard);
            }
            return PossibleBoards;
        }
        /// <summary>
        /// This is the main function of the algorithm.
        /// program gets a board and calculates its entire value.
        /// the program calculates by the white player perspective.
        /// the program calculates the distance of each player balls from center,
        /// the grouping of each players balls and the current score.
        /// the program returns (value of white)-(value of black) so it calculates the entire board from
        /// white's perspective.
        /// </summary>
        /// <param name="board"> board to calculate. </param>
        /// <returns> board value from white's perspective. </returns>
        private double EvaluateBoard(Board board)
        {
            double sum = 0;
            // distances from center.
            sum += CalculateBoardDistances(board, (sbyte)1);
            sum -= CalculateBoardDistances(board, (sbyte)-1);
            //balls grouping
            sum += CalculateGrouping(board, (sbyte)1);
            sum -= CalculateGrouping(board, (sbyte)-1);
            //current score.
            sum += CalculateScore(board, (sbyte)1);
            sum -= CalculateScore(board, (sbyte)-1);
            return sum;
        }
        /// <summary>
        ///  program gets a board and player, returns value for the grouping of player.
        /// </summary>
        /// <param name="board">board to calculate</param>
        /// <param name="player">which player to calculate.</param>
        /// <returns> count of grouping * weight (value).</returns>
        private int CalculateGrouping(Board board, sbyte player)
        {
            int cnt = 0;
            BitSet a = (player == 1) ? board.getWhiteSet() : board.getBlackSet();
            //goes over each ball and count all neigbours.
            for (int i = a.nextSetBit(0); i != -1; i = a.nextSetBit(i + 1))
            { // goes all over the set bits.
                foreach(sbyte pos in board.getNeighborsOfPossition((sbyte)i))
                {
                    if (pos == -1)
                        continue;
                    if (a.get(pos)) // if the neighbor is the same color as the player
                        cnt++;
                }
            }
            int sum = CalculateBoardGroupingValue(cnt);
            return sum;
        }
        /// <summary>
        /// rogram gets the counter of grouping and return a value of the grouping.
        /// </summary>
        /// <param name="cnt"> neighbors counter.</param>
        /// <returns> cnt*ValueOfGrouping </returns>
        private int CalculateBoardGroupingValue(int cnt)
        {
            return cnt * ValueOfGrouping;
        }
        /// <summary>
        ///  program calculates the value of scored balls.
        /// </summary>
        /// <param name="b">board.</param>
        /// <param name="player">player to calculate</param>
        /// <returns> weight* amount of scored balls.</returns>
        private int CalculateScore(Board b, sbyte player)
        {
            if (player == 1)
            { // white player
                if (b.getScoreWhite() >= 6) //win
                    return 1000000; // max value, the best move.
                // otherwise calcluate the amount of the balls that were eaten times the value for each eat
                return b.getScoreWhite() * ValueOfScoredBalls;            
            }
            // in case of black as AI
            if (b.getScoreBlack() >= 6) 
                return 1000000;
            return b.getScoreBlack() * ValueOfScoredBalls;
        }
        /// <summary>
        /// program calculates the total board distances of the given player and
        /// returns the value* weight.
        /// the program uses 5 bitsets as distances mask.
        /// </summary>
        /// <param name="board">board</param>
        /// <param name="player"></param>
        /// <returns>total value of distances from center * its weight. </returns>
        private double CalculateBoardDistances(Board board, sbyte player)
        {
            BitSet a = (player == 1) ? board.getWhiteSet() : board.getBlackSet();
            double sum = 0;
            for (int i = a.nextSetBit(0); i != -1; i = a.nextSetBit(i + 1))
            { // goes on all the set bits, in the BitSet.
                // if in circle 0, (middle).
                if (Distances[0].get(i))
                {
                    sum += 0;
                    continue;
                }
                //if in circle 1.
                if (Distances[1].get(i))
                {
                    sum += 1;
                    continue;
                }
                //if in circle 2.
                if (Distances[2].get(i))
                {
                    sum += 2.8;
                    continue;
                }
                //if in circle 3.
                if (Distances[3].get(i))
                {
                    sum += 4;
                    continue;
                }
                //if in circle 4.
                if (Distances[4].get(i))
                {
                    sum += 5;
                    continue;
                }
            }
            double result = calculateValueOfSumDistances(sum); // calcluates the value of the ball's distance from the middle. 
            return result;
        }
        /// <summary>
        /// the program returns the value* -1 so the players will try to get 
        /// closer to the center and not away from it.
        /// </summary>
        /// <param name="sum">sum of distances from center.</param>
        /// <returns>sum* ValueOfCenterDistance*-1</returns>
        private double calculateValueOfSumDistances(double sum)
        {
            return sum * ValueOfCenterDistance * -1;
        }
        /// <summary>
        /// program initializes and sets 5 bitsets value which help calculate each ball's distance from the center.
        /// </summary>
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

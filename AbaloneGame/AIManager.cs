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
        private const int ValueOfCenterDistance = 10;
        private const int ValueOfScoredBalls = 1000;
        //Alpha Beta recursion depth
        private const int depthToSearch = 4;
        public AIManager(int player)
        {
            initializeDistances();
        }

        /**
        * This is the program that activates main algorithm.
        * program uses AlphaBeta and Evaluate function to find the best move of computer.
        * @param board - current board to find move to.
        * @param currentplayer - player to find move to.
        * @return - best move it can choose.
        */
        public Move playTurn(Board board, int currentplayer)
        {
            Console.WriteLine("started AI turn!!!");
            //dataStruct of possible moves
            ArrayList ogBoardMoves = board.getmoves((sbyte)currentplayer);
            Console.WriteLine("-----------found " + ogBoardMoves.Count + "board possible moves");
            // implementing the moves to boards.
            ArrayList AllBoards = getPossibleBoards(board, ogBoardMoves);
            // starts as worst possible move
            // - infinity
            double bestValue = -1 * currentplayer * 2000000;
            double tempvalue;
            int bestindex = 0;// cnt=0;
            foreach (Board childBoard in AllBoards)
            {
                tempvalue = AlphaBeta(childBoard, depthToSearch, -2100000, 2100000, (sbyte)(currentplayer * -1));
                //searches for the minimum value by blues point of view. (red is AI).
                if (tempvalue < bestValue)
                {
                    bestValue = tempvalue;
                    bestindex = AllBoards.IndexOf(childBoard);
                }
            }
            return (Move)ogBoardMoves[bestindex]; //check for alternative
        }
        /**
        * This is the main AI recursion algorithm.
        * uses EvaluateBoard to return the best result it can promise.
        * if the program hasn't reached depth 1 than it calculates all possible board and calles
        * again to the function with depth-1 and currentPlayer*-1.
        * @param board - board to calculate from.
        * @param depth - current depth to search. if depth =1 than calculates board value and return it.
        * @param alpha - best board value Alpha player can promise
        * @param beta - best board value Beta player can promise
        * @param currentPlayer - player to calculate to
        * @return
        */
        private double AlphaBeta(Board board, int depth, double alpha, double beta, sbyte currentPlayer)
        {
            //cnt++;
            //Console.WriteLine("started alpha betha");
            // checks if needs to return the value of board.
            if (depth <= 1 || board.getScoreWhite() >= 6 || board.getScoreBlack() >= 6)
                return EvaluateBoard(board);
            // if 1 than try to maximize board value.
            if (currentPlayer == 1)
            {
                // worst value, cannot be under -1000000.
                double value = -2000000;
                //dataStruct of possible moves
                ArrayList allBoardMoves = board.getmoves((sbyte)currentPlayer);
                ArrayList AllBoards = getPossibleBoards(board, allBoardMoves);
                foreach (Board childBoard in AllBoards)
                {
                    //recursion.
                    value = Math.Max(value, AlphaBeta(childBoard, depth - 1, alpha, beta, (sbyte)-1));
                    alpha = Math.Max(value, alpha);
                    //pruning
                    if (alpha >= beta)
                    {
                        //Console.WriteLine("purning accured");
                        break;
                    }
                }
                return value;
            }
            else
            {
                // try to minimize board value.
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
                        //Console.WriteLine("purning accured");
                        break;
                    }
                }
                return value;
            }
        }
        /**
        * program return list of all possible boards which have implemented the moves from
        * original board and ogBoardMoves list.
        * @param board -board to calculate all its child boards.
        * @param ogBoardMoves - list of all possible moves from originalboard - board.
        * @return list of all possible child boards of board.
        */
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
        /**
        * This is the main function of the algorithm.
        * program gets a board and calculates its entire value.
        * the program calculates by the blue player perspective.
        *
        * the program calculates the distancesof the each player balls from center,
        * the grouping of each players balls and the current score.
        * the program returns (value of blue)-(value of red) so it calculates the entire board from
        * blues perspective.
        * @param board - board to calculate
        * @return - board value from blues perspective.
        */
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
        /**
        * program gets move and player, returns value for the grouping of player.
        * @param board - board to calculate
        * @param player - which player to calculate.
        * @return count of grouping*weight (value).
        */
        private int CalculateGrouping(Board board, sbyte player)
        {
            int cnt = 0;
            BitSet a = (player == 1) ? board.getWhiteSet() : board.getBlackSet();
            //goes over each ball and count all neigbours.
            for (int i = a.nextSetBit(0); i != -1; i = a.nextSetBit(i + 1))
            {
                foreach(sbyte pos in board.getNeighborsOfPossition((sbyte)i))
                {
                    if (pos == -1)
                        continue;
                    if (a.get(pos))
                        cnt++;
                }
            }
            int sum = CalculateBoardGroupingValue(cnt);
            return sum;
        }
        /**
        * rogram gets the counter of grouping and return a value of the grouping.
        * @param cnt - neighbors counter.
        * @return cnt*ValueOfGrouping
        */
        private int CalculateBoardGroupingValue(int cnt)
        {
            return cnt * ValueOfGrouping;
        }
        /**
        * program calculates the value of scored balls.
        * @param b - board.
        * @param player - player to calculate
        * @return weight*amount of scored balls.
        */
        private int CalculateScore(Board b, sbyte player)
        {
            if (player == 1)
            {
                //win
                if (b.getScoreWhite() >= 6)
                    return 1000000;
                return b.getScoreWhite() * ValueOfScoredBalls;
            }
            if (b.getScoreBlack() >= 6)
                return 1000000;
            return b.getScoreBlack() * ValueOfScoredBalls;
        }
        /**
        * program balculates the total board distances of given player and
        * returns the value * weight.
        * the program uses 5 bitsets as distances mask.
        * @param board - board
        * @param player
        * @return total value of distances from center * its weight.
        */
        private double CalculateBoardDistances(Board board, sbyte player)
        {
            BitSet a = (player == 1) ? board.getWhiteSet() : board.getBlackSet();
            double sum = 0;
            for (int i = a.nextSetBit(0); i != -1; i = a.nextSetBit(i + 1))
            {
                //if in circle 0.
                if (Distances[0].get(i))
                {
                    sum += 0;
                    continue;
                }
                //if in circle 0.
                if (Distances[1].get(i))
                {
                    sum += 1;
                    continue;
                }
                //if in circle 0.
                if (Distances[2].get(i))
                {
                    sum += 2.8;
                    continue;
                }
                //if in circle 0.
                if (Distances[3].get(i))
                {
                    sum += 4;
                    continue;
                }
                //if in circle 0.
                if (Distances[4].get(i))
                {
                    sum += 5;
                    continue;
                }
                Console.WriteLine("couldnt find position");
            }
            double result = calculateValueOfSumDistances(sum);
            return result;
        }
        /*
        * first version
        * program gets the counter of distance to center
        * program returns the value of the
        */
        /**
        * program gets the sum of distances and rethrn the sum*its weight.
        * the program returns the value * -1 so the players will try to get closer to the center and not away from it.
        * @param sum - sum of distances from center.
        * @return - sum*ValueOfCenterDistance*-1
        */
        private double calculateValueOfSumDistances(double sum)
        {
            return sum * ValueOfCenterDistance * -1;
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

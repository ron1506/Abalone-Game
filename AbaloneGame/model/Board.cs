using System.Collections;

namespace AbaloneGame.model
{
    class Board
    {
        //data structures:
        private BitSet EdgeOfBoard;
        private BitSet WhiteSet;
        private BitSet BlackSet;
        //directions matrix
        private sbyte[] pathArr;
        //score count
        private sbyte scoreWhite;
        private sbyte scoreBlack;
        ///
        /// Constructor
        /// initializes patharr
        ///
        public Board()
        {
            pathArr = new sbyte[6];
            initpathArr();
        }
        public BitSet getEdgeOfBoard()
        {
            return EdgeOfBoard;
        }
        public void setEdgeOfBoard(BitSet edgeOfBoard)
        {
            EdgeOfBoard = edgeOfBoard;
        }
        public BitSet getWhiteSet()
        {
            return WhiteSet;
        }
        public void setWhiteSet(BitSet whiteSet)
        {
            WhiteSet = whiteSet;
        }
        public BitSet getBlackSet()
        {
            return BlackSet;
        }
        public void setBlackSet(BitSet blackSet)
        {
            BlackSet = blackSet;
        }
        public sbyte getScoreWhite()
        {
            return scoreWhite;
        }
        public void setScoreWhite(sbyte scoreWhite)
        {
            this.scoreWhite = scoreWhite;
        }
        public sbyte getScoreBlack()
        {
            return scoreBlack;
        }
        public void setScoreBlack(sbyte scoreBlack)
        {
            this.scoreBlack = scoreBlack;
        }
        ///
        ///program initializes the initpathArr
        ///
        public void initpathArr()
        {
            pathArr = new sbyte[6];
            pathArr[0] = 1;
            pathArr[1] = -12;
            pathArr[2] = -11;
            pathArr[3] = -1;
            pathArr[4] = 12;
            pathArr[5] = 11;
        }
        /// <summary>     
        /// program creats new 3 bitsets -
        /// whiteSet, BlackSet, EdgeOfBoard.
        /// </summary>
        /// <param name="BoardLayout">sets the start game layout according to the BoardLayout.</param>
        public void initializeBoard(int BoardLayout)
        {
            initpathArr();
            scoreWhite = 0;
            scoreBlack = 0;
            EdgeOfBoard = new BitSet(121);
            WhiteSet = new BitSet(121);
            BlackSet = new BitSet(121);
            ///board edges layout.
            EdgeOfBoard.set(12, 17);
            EdgeOfBoard.set(23, 29);
            EdgeOfBoard.set(34, 41);
            EdgeOfBoard.set(45, 53);
            EdgeOfBoard.set(56, 65);
            EdgeOfBoard.set(68, 76);
            EdgeOfBoard.set(80, 87);
            EdgeOfBoard.set(92, 98);
            EdgeOfBoard.set(104, 109);
            //classic layout
            if (BoardLayout == 0)
            {
                ///blueSet, player 1
                WhiteSet.set(12, 14);
                WhiteSet.set(23, 25);
                WhiteSet.set(34, 37);
                WhiteSet.set(45, 48);
                WhiteSet.set(56, 59);
                WhiteSet.set(68);
                //Redset, player -1
                BlackSet.set(52);
                BlackSet.set(62, 65);
                BlackSet.set(73, 76);
                BlackSet.set(84, 87);
                BlackSet.set(96, 98);
                BlackSet.set(107, 109);
            }
            //pro layout
            if (BoardLayout == 1)
            {
                //top left
                WhiteSet.set(39, 41);
                WhiteSet.set(50, 53);
                WhiteSet.set(62, 64);
                //top right
                BlackSet.set(83, 85);
                BlackSet.set(94, 97);
                BlackSet.set(106, 108);
                //bottom left
                BlackSet.set(13, 15);
                BlackSet.set(24, 27);
                BlackSet.set(36, 38);
                //bottom right
                WhiteSet.set(57, 59);
                WhiteSet.set(68, 71);
                WhiteSet.set(80, 82);
            }
            //snake layout
            if (BoardLayout == 2)
            {
                //start in bottom left
                WhiteSet.set(12);
                WhiteSet.set(23);
                WhiteSet.set(34);
                WhiteSet.set(45);
                WhiteSet.set(56);
                WhiteSet.set(68);
                WhiteSet.set(80);
                WhiteSet.set(92);
                WhiteSet.set(93);
                WhiteSet.set(94);
                WhiteSet.set(83);
                WhiteSet.set(71);
                WhiteSet.set(59);
                WhiteSet.set(48);
                //starts in top right
                BlackSet.set(108);
                BlackSet.set(97);
                BlackSet.set(86);
                BlackSet.set(75);
                BlackSet.set(64);
                BlackSet.set(52);
                BlackSet.set(40);
                BlackSet.set(28);
                BlackSet.set(27);
                BlackSet.set(26);
                BlackSet.set(37);
                BlackSet.set(49);
                BlackSet.set(61);
                BlackSet.set(72);
            }
            //Wall layout
            if (BoardLayout == 3)
            {
                //top left
                BlackSet.set(28);
                BlackSet.set(39);
                BlackSet.set(50, 52);
                BlackSet.set(61, 63);
                BlackSet.set(72, 74);
                BlackSet.set(83, 85);
                BlackSet.set(94, 96);
                BlackSet.set(105);
                BlackSet.set(86);
                //bottom left
                WhiteSet.set(15);
                WhiteSet.set(25, 27);
                WhiteSet.set(36, 38);
                WhiteSet.set(47, 49);
                WhiteSet.set(58, 60);
                WhiteSet.set(69, 71);
                WhiteSet.set(81);
                WhiteSet.set(92);
                WhiteSet.set(34);
            }
        }
        /// <summary>
        /// function return the value of position in board.
        /// </summary>
        /// <param name="position">position to check value for.</param>
        /// <returns> -9 if not in board, 1 if in whiteset, -1 if in blackset ,0 if not pressed(not in both).</returns>
        public sbyte GetValueInPosition(sbyte position)
        {
            //not in board
            if (!isPositionInBoard(position))
                return -9;
            if (WhiteSet.get(position))
                return 1;
            if (BlackSet.get(position))
                return -1;
            //if reached than no one is true so empty.
            return 0;
        }
        /// <summary>
        /// program sets value in given position.
        /// </summary>
        /// <param name="position">position to change.</param>
        /// <param name="value"> the value to change to.</param>
        public void SetValueInPosition(sbyte position, sbyte value)
        {
            switch (value)
            {
                case 1:
                    { //was black, now white.
                        WhiteSet.set(position, true);
                        BlackSet.set(position, false);
                        break;
                    }
                case 0:
                    { //now free.
                        WhiteSet.set(position, false);
                        BlackSet.set(position, false);
                        break;
                    }
                case -1:
                    { // was white, now black.
                        WhiteSet.set(position, false);
                        BlackSet.set(position, true);
                        break;
                    }
            }
        }
        /// <summary>
        /// program gets position and return true if in board.
        /// </summary>
        /// <param name="position">position to check</param>
        /// <returns>true if in board, false if not.</returns>
        public bool isPositionInBoard(sbyte position)
        {
            return EdgeOfBoard.get(position);
        }
        /// <summary>
        /// program check if tow positions are next to each other.
        /// !! does not check if 2 positions are in board.
        /// </summary>
        /// <param name="posA">first position</param>
        /// <param name="posB">second position</param>
        /// <returns> true if both positions are next to each other. </returns>
        public bool IsPositionsTogether(sbyte posA, sbyte posB)
        {
            sbyte givenDirection = (sbyte)(posB - posA);
            for (int i = 0; i < pathArr.Length; i++)
            {
                sbyte direction = pathArr[i];
                //checks if direction is in directions array
                if (direction == givenDirection)
                    return true;
            }
            //if reached than they are not together.
            return false;
        }
        /// <summary>
        /// program gets 2 position and return the next position in line.
        /// </summary>
        /// <param name="posA">first position</param>
        /// <param name="posB">second position</param>
        /// <returns> posC if position found, -9 if position is not in board.</returns>
        public sbyte getNextPositionInLine(sbyte posA, sbyte posB)
        {
            sbyte posC = (sbyte)(posB - posA + posB);
            if (isPositionInBoard(posC))
                return posC;
            return -9;
        }
        /// <summary>
        /// program returns the position of posSideA
        /// </summary>
        /// <param name="posA">first position</param>
        /// <param name="posB">second position, next to posA</param>
        /// <param name="posSideB">second side position, in side to posB</param>
        /// <returns> -9 if posSideA is not in board, else returns posA.</returns>
        public sbyte get4thPosInSideMove(sbyte posA, sbyte posB, sbyte posSideB)
        {
            sbyte direction = (sbyte)(posSideB - posB);
            if (!isPositionInBoard((sbyte)(posA + direction)))
                return -9;
            return (sbyte)(posA + direction);
        }
        /// <summary>
        /// program gets a valid move and updates current board from the move.
        /// </summary>
        /// <param name="move">valid move to implement.</param>
        /// <returns>1 if Winfound, 0 if not.</returns>
        public int makeMove(Move move)
        {
            //move indxex:
            sbyte[] indexesarray = move.getIndexs();
            //in all moves the first position will be the pusher's ball;
            sbyte ownV = GetValueInPosition(indexesarray[0]);
            if (move.getRowMove())
            { //if a row move
                //in all moves the first position after the push will be 0;
                SetValueInPosition(indexesarray[0], (sbyte)0);
                //send all own moves including next.
                for (int i = 1; i < move.getNumOfOwn() + 1; i++)
                { // gives each next place it's previous value, the pushing balls.
                    SetValueInPosition(indexesarray[i], ownV); 
                }
                //updates enemy balls.
                for (int i1 = 0; i1 < move.getNumToPush(); i1++)
                { // gives each next place it's previous value, the pushed balls.
                    //enemy value is ownv * -1
                    SetValueInPosition(indexesarray[move.getNumOfOwn() + 1 + i1], (sbyte)(ownV * -1));
                }
            }
            else
            {// a side move, no pushed balls.
                for (int i = 0; i < move.getNumOfOwn(); i++)
                {
                    //in a side move the original positions after the push will be 0;
                    SetValueInPosition(indexesarray[i], (sbyte)0);
                    SetValueInPosition(indexesarray[i + move.getNumOfOwn()], (sbyte)ownV);
                }
            }
            //checks if win found
            if (move.ifIsScore())
            {
                if (ownV == 1)
                { // white score
                    scoreWhite++;
                    if (scoreWhite >= 6)
                        return 1;
                }
                else
                { // black score
                    scoreBlack++;
                    if (scoreBlack >= 6)
                        return 1;
                }
            }
            //win not found
            return 0;
        }

        /////////////////////////////////////////////////Moves check/////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// program gets two points: A and B are next to each other.
        // the programs return if posC is in line after B.
        /// </summary>
        /// <param name="posA">first position</param>
        /// <param name="posB">second position (next to posA)</param>
        /// <param name="posC">third position</param>
        /// <returns> true if posA, posB and posC are in a line.</returns>
        public bool isPositionsInLine3(sbyte posA, sbyte posB, sbyte posC)
        {
            //checks if the change between x and y values is the same.
            return ((posB -posA) == (posC -posB));
        }
        /// <summary>
        /// the program switches the values of two given positions.
        /// </summary>
        /// <param name="posA">first position</param>
        /// <param name="posB">second position</param>
        public void switchPositions(sbyte posA, sbyte posB)
        {
            sbyte posAValue = GetValueInPosition(posA);
            sbyte posBValue = GetValueInPosition(posB);
            WhiteSet.set(posA, posBValue);
            BlackSet.set(posB, posAValue);
        }
        /// <summary> 
        /// program tries to create a move of push 2 balls in the direction of posC.
        /// </summary>
        /// <param name="posA"> first position (own)</param>
        /// <param name="posB"> second position(next to posA, own)</param>
        /// <param name="posC"> third position(in line) can be(valueof posA)*-1 or 0</param>
        /// <returns>move if found a valid move, null if not.</returns>
        public Move TryToPush2(sbyte posA, sbyte posB, sbyte posC)
        {
            Move mov = new Move();
            //if enpty then switch posA and posC
            sbyte VposC = GetValueInPosition(posC);
            //2 balls push.
            if (VposC == 0)
            {//empty
                //creates new Move
                mov.new2BallsMove0Push(posA, posB, posC);
                return mov;
            }
            //posC is Enemy
            //to check where is posD
            sbyte posD = getNextPositionInLine(posB, posC);
            if (posD == -9)
            {//point push, moving an enemy ball out of the board.
             //creats new Move
                mov.new2BallsMove1PushWithScore(posA, posB, posC);
                return mov;
            }
            sbyte VposD = GetValueInPosition(posD);
            //2 own and 2 enemy -> cannot push
            if (VposD != 0)
                return null;
            //if reached than 2 own 1 enemy 1 empty, can push.
            mov.new2BallsMove1PushNoScore(posA, posB, posC, posD);
            return mov;
        }
        /// <summary>
        /// program tries to create a move of push 3 balls in the direction of posD.
        /// </summary>
        /// <param name="posA">first position (own)
        /// <param name="posB">second position(next to posA , own)
        /// <param name="posC">second position(in line , own)
        /// <param name="posD">third position (in line) can be (valueof posA)*-1 or 0
        /// <return> move if found a valid move, null if not.</returns>
        public Move TryToPush3(sbyte posA, sbyte posB, sbyte posC, sbyte posD)
        {
            if (GetValueInPosition(posD) == 0)
            {//3 balls move no push.
                Move mov = new Move();
                mov.new3BallsMove0Push(posA, posB, posC, posD);
                return mov;
            }
            sbyte Vown = GetValueInPosition(posA);
            //checks if 4 balls same color.
            if (GetValueInPosition(posD) == Vown)
                return null;
            //if reached than 3 own 1 enemy.
            sbyte posE = getNextPositionInLine(posC, posD);
            if (posE == -9)
            {//3 balls move 1 push with score.
                Move mov = new Move();
                mov.new3BallsMove1PushWithScore(posA, posB, posC, posD);
                return mov;
            }
            //checks if 3 own 1 enemy 1 own.
            sbyte VposE = GetValueInPosition(posE);
            if (VposE == Vown)
                return null;
            if (VposE == 0)
            {//3 own 1 enemy push no score.
                Move mov = new Move();
                mov.new3BallsMove1PushNoScore(posA, posB, posC, posD, posE);
                return mov;
            }
            //if reached than 3 own 2 enemy.
            sbyte posF = getNextPositionInLine(posD, posE);
            if (posF == -9)
            {//3 balls move 2 push with score.
                Move mov = new Move();
                mov.new3BallsMove2PushWithScore(posA, posB, posC, posD, posE);
                return mov;
            }
            //checks if 3 own 2 enemy 1 own.
            sbyte VposF = GetValueInPosition(posF);
            if (VposF == Vown)
                return null;
            if (VposF == 0)
            {//3 own 2 enemy push no score.
                Move mov = new Move();
                mov.new3BallsMove2PushNoScore(posA, posB, posC, posD, posE, posF);
                return mov;
            }
            //3 own 3 enemy -> cannot push.
            return null;
        }
        /// <summary>
        /// program check if can do a side move of posA and posB to posSideA and posSideB
        /// </summary>
        /// <param name="posA">first position </param>
        /// <param name="posB">second position (next to posA , own) </param>
        /// <param name="posSideB">first sids position , empty. </param>
        /// <return> move if found a valid move, null if not.</returns> </param>
        public Move TryToSideMove2(sbyte posA, sbyte posB, sbyte posSideB)
        {
            ///both side poses must be 0;
            if (GetValueInPosition(posSideB) != 0)
                return null;
            //checks if possideB is next to one of given positions.
            if (!IsPositionsTogether(posB, posSideB))
                return null;
            //if reached than posSideB is Valid.
            //checks posSideA.
            sbyte posSideA = get4thPosInSideMove(posA, posB, posSideB);
            if (posSideA == -9 || GetValueInPosition(posSideA) != 0)
                return null;
            //if reached than it is a valid 2 side move.
            //checks if 2nd move position is in board
            Move mov = new Move();
            mov.new2BallsSideMove(posA, posB, posSideA, posSideB);
            return mov;
        }
        /// <summary>
        /// program check if can do a side move of posA, posB and posC to posSideA and posSideB and posSideC
        /// </summary>
        /// <param name="posA">first position</param>
        /// <param name="posB">second position (next to posA , own)</param>
        /// <param name="posB">second position (in line , own)</param>
        /// <param name="posSideC">first side position , empty. </param>
        /// <return> move if found a valid move, null if not. </returns>
        public Move TryToSideMove3(sbyte posA, sbyte posB, sbyte posC, sbyte posSideC)
        {
            ///both side poses must be 0;
            if (GetValueInPosition(posSideC) != 0)
                return null;
            //checks if possideC is next to one of given positions.
            if (!IsPositionsTogether(posC, posSideC))
                return null;
            //if reached than posSideB is Valid.
            //checks posSideB.
            sbyte posSideB = get4thPosInSideMove(posB, posC, posSideC);
            if (posSideB == -9 || GetValueInPosition(posSideB) != 0)
                return null;
            //checks posSideA.
            sbyte posSideA = get4thPosInSideMove(posA, posB, posSideB);
            if (posSideA == -9 || GetValueInPosition(posSideA) != 0)
                return null;
            //if reached than it is a valid 2 side move.
            //checks if 2nd move position is in board
            Move mov = new Move();
            mov.new3BallsSideMove(posA, posB, posC, posSideA, posSideB, posSideC);
            return mov;
        }

        //////////////////////////////////////////// AI Functions///////////////////////////////////////////////////
        /// <summary>
        /// program finds all the possible moves of Currentplayer from current board.
        /// </summary>
        /// <param name="currentPlayer">player to search moves for.</param>
        /// <return> list of all possible moves of current player. </returns>
        public ArrayList getmoves(sbyte currentPlayer)
        {
            ArrayList MoveList = new ArrayList(60);
            Move move = new Move();
            sbyte[] possiblePoisitionsArray;
            sbyte[] sideArray;
            //set own and enemy sets
            BitSet ownSet = (currentPlayer == 1) ? WhiteSet : BlackSet;
            //go over all balls in bitset (set places) as a first click.
            for (sbyte posA = (sbyte)ownSet.nextSetBit(0); posA != -1; posA = (sbyte)ownSet.nextSetBit(posA + 1))
            {
                //gets all neighbors positions of the current chosen ball.
                possiblePoisitionsArray = getNeighborsOfPossition(posA);
                /// go over each of neighbors positions.
                foreach (sbyte posB in possiblePoisitionsArray)
                { //if position is not a valid neighbor (out of the board).
                    if (posB == -1)
                        continue;
                    //checks if posB is Empty
                    sbyte VposB = GetValueInPosition(posB);
                    if (VposB == 0)
                    {
                        //add 2 points, 1 of own, 0 push, 0 score
                        move = new Move();
                        move.new1BallMove(posA, posB);
                        //adds move to list
                        MoveList.Add(move);
                    }
                    else
                    {//posB is not empty
                     //if enemy then cannot push (move already checked) then break.
                        if (VposB == currentPlayer * -1) continue;
                        //next postition in line, return -1 if not in board
                        sbyte posC = getNextPositionInLine(posA, posB);
                        if (posC != -9)
                        {//posC is inBoard.
                            if (GetValueInPosition(posC) == currentPlayer)
                            { //posC in the board
                                sbyte posD = getNextPositionInLine(posB, posC);
                                if (posD != -9)
                                { //posD is inBoard.
                                    move = new Move();
                                    move = TryToPush3(posA, posB, posC, posD); // returns a legal move if one was found, null otherwise.
                                    if (move != null)
                                        MoveList.Add(move); // adds the move to the list of possible moves.
                                }
                            }
                            else
                            {//can be 2 own push.
                                move = new Move();
                                move = TryToPush2(posA, posB, posC);
                                if (move != null)
                                    MoveList.Add(move);
                            }
                        }
                        //to check all possible side moves of 2 balls.
                        sideArray = getNeighborsOfPossition(posB);
                        /// go over each of neighbors positions from posB.
                        foreach (sbyte posSideB in sideArray)
                        {
                            //if position is not a valid neighbor.
                            if (posSideB == -1)
                                continue;
                            //doesnt check posA (already pressed) or posC (already checked in line move).
                            if (posSideB == posA || posSideB == posB)
                                continue;
                            if (GetValueInPosition(posSideB) != 0)
                                continue;
                            //gets posSideA index, if not in board then -1.
                            sbyte posSideA = get4thPosInSideMove(posA, posB, posSideB);
                            if (posSideA == -9 || GetValueInPosition(posSideA) != 0)
                                continue;
                            //if reached then it is a sideMove in positions posA, posB to posSideA and posSideB
                            //
                            move = new Move();
                            move.new2BallsSideMove(posA, posB, posSideA, posSideB);
                            MoveList.Add(move);
                            //3 SIDE MOVE
                            //checks if the next position in posC and posSideC are empty.
                            sbyte posCC = getNextPositionInLine(posA, posB);
                            if (posC != VposB)
                                continue;
                            sbyte posSideC = getNextPositionInLine(posSideA, posSideB);
                            if (posSideC == -9)
                                continue;
                            //checks if both posC and sideC are empty
                            if (GetValueInPosition(posCC) == 0 && GetValueInPosition(posSideC) == 0)
                            {//3 balls side move.
                                move = new Move();
                                move.new3BallsSideMove(posA, posB, posCC, posSideA, posSideB, posSideC);
                                MoveList.Add(move);
                            }
                        }
                    }
                }
            }
            return MoveList;
        }
        /// <summary>        
        /// program returns a byte arr of all positions next to the given position.
        /// if position is not in board than it is -1.
        /// program creates a bute array of all positions next to a given position.
        /// if a positions is not in board, its -1 in the array.
        /// </summary>
        /// <param name="position">position to creates neighbors array</param>
        /// <returns> byte array of neighbors, -1 for each neighbor that is out of board. </returns>
        public sbyte[] getNeighborsOfPossition(sbyte position)
        {
            sbyte pos;
            sbyte[] arr = new sbyte[6];
            for (byte i = 0; i < 6; i++)
            {
                //neighbor position
                pos = (sbyte)(position + pathArr[i]);
                if (isPositionInBoard(pos))
                    arr[i] = pos;
                else
                    arr[i] = -1;
            }
            return arr;
        }
        /// <summary>
        /// program gets a board instance and sets current (this) board the values of the given board.
        /// </summary>
        ///<param name="b">board to clone</param>
        public void cloneBoard(Board b)
        {
            this.WhiteSet = (BitSet)b.WhiteSet.clone();
            this.BlackSet = (BitSet)b.BlackSet.clone();
            //to find a better way for edge of board
            this.EdgeOfBoard = (BitSet)b.EdgeOfBoard.clone();
            this.scoreWhite = b.scoreWhite;
            this.scoreBlack = b.scoreBlack;
        }
    }
}


using System;

namespace AbaloneGame.model
{
    public class Move
    {
        //number of own balls to move.
        private sbyte numOfOwn;
        // number of enemy balls to push
        private sbyte numToPush;
        // is point move
        private bool isScore;
        //indexes array.
        private sbyte[] indexs;
        //isrowMove
        private bool isRowMove;
        public sbyte[] getIndexs()
        {
            return indexs;
        }

        public bool getRowMove()
        {
            return this.isRowMove;
        }

        public int getNumOfOwn()
        {
            return numOfOwn;
        }

        public int getNumToPush()
        {
            return numToPush;
        }

        public bool ifIsScore()
        {
            return isScore;
        }

        public void new1BallMove(sbyte posA, sbyte posB)
        {
            //initialize array
            indexs = new sbyte[2];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            //put other parameters to class
            numToPush = 0;
            numOfOwn = 1;
            isRowMove = false; ///maybe to change
            isScore = false;
        }

        public void new2BallsMove0Push(sbyte posA, sbyte posB, sbyte posC)
        {
            //initialize array
            indexs = new sbyte[3];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            //put other parameters to class
            numToPush = 0;
            numOfOwn = 2;
            isRowMove = true;
        }

        public void new2BallsMove1PushWithScore(sbyte posA, sbyte posB, sbyte posC)
        {
            //initialize array
            indexs = new sbyte[3];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            //put other parameters to class
            numToPush = 0;
            numOfOwn = 2;
            isRowMove = true;
            isScore = true;
        }

        public void new2BallsMove1PushNoScore(sbyte posA, sbyte posB, sbyte posC, sbyte posD)
        {
            //initialize array
            indexs = new sbyte[4];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            indexs[3] = posD;
            //put other parameters to class
            numToPush = 1;
            numOfOwn = 2;
            isRowMove = true;
            isScore = false;
        }

        public void new3BallsMove0Push(sbyte posA, sbyte posB, sbyte posC, sbyte posD)
        {
            //initialize array
            indexs = new sbyte[4];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            indexs[3] = posD;
            //put other parameters to class
            numToPush = 0;
            numOfOwn = 3;
            isRowMove = true;
            isScore = false;
        }

        public void new3BallsMove1PushWithScore(sbyte posA, sbyte posB, sbyte posC, sbyte posD)
        {
            //Console.WriteLine("Move.new3BallsMove1PushWithScore()");
            //initialize array
            indexs = new sbyte[4];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            indexs[3] = posD;
            //put other parameters to class
            numToPush = 0;
            numOfOwn = 3;
            isRowMove = true;
            isScore = true;
        }

        public void new3BallsMove1PushNoScore(sbyte posA, sbyte posB, sbyte posC, sbyte posD, sbyte posE)
        {
            //initialize array
            indexs = new sbyte[5];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            indexs[3] = posD;
            indexs[4] = posE;
            //put other parameters to class
            numToPush = 1;
            numOfOwn = 3;
            isRowMove = true;
            isScore = false;
        }

        public void new3BallsMove2PushWithScore(sbyte posA, sbyte posB, sbyte posC, sbyte posD, sbyte posE)
        {
            //initialize array
            indexs = new sbyte[5];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            indexs[3] = posD;
            indexs[4] = posE;
            //put other parameters to class
            numToPush = 1;
            numOfOwn = 3;
            isRowMove = true;
            isScore = true;
        }

        public void new3BallsMove2PushNoScore(sbyte posA, sbyte posB, sbyte posC, sbyte posD, sbyte posE, sbyte posF)
        {
            //initialize array
            indexs = new sbyte[6];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            indexs[3] = posD;
            indexs[4] = posE;
            indexs[5] = posF;
            //put other parameters to class
            numToPush = 2;
            numOfOwn = 3;
            isRowMove = true;
            isScore = false;
        }

        public void new2BallsSideMove(sbyte posA, sbyte posB, sbyte posSideA, sbyte posSideB)
        {
            //Console.WriteLine("Move.new2BallsSideMove()");
            //initialize array
            indexs = new sbyte[4];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posSideA;
            indexs[3] = posSideB;
            //put other parameters to class
            numToPush = 0;
            numOfOwn = 2;
            isRowMove = false;
            isScore = false;
        }

        public void new3BallsSideMove(sbyte posA, sbyte posB, sbyte posC, sbyte posSideA, sbyte posSideB, sbyte posSideC)
        {
            //initialize array
            indexs = new sbyte[6];
            //put positions in array
            indexs[0] = posA;
            indexs[1] = posB;
            indexs[2] = posC;
            indexs[3] = posSideA;
            indexs[4] = posSideB;
            indexs[5] = posSideC;
            //put other parameters to class
            numToPush = 0;
            numOfOwn = 3;
            isRowMove = false;
            isScore = false;
        }
    }
}
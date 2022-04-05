using AbaloneGame.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbaloneGame
{
    class GameManager
    {
        // the player who's turn is now.
        private int currentplayer;
        //board
        private Board board;
        public Board Board { get => board; }
        //player (client) turn manager
        private PlayerTurnManager Pturn;
        //AI manager
        //private AIManager AI;
        //game mode
        bool isPVP;
        //is waiting for user input.
        bool isWaitingToPlayer = false;
        /**
        * Constructor.
        * @param point - server end point
*/
        public GameManager()
        {
            this.currentplayer = 1;
            this.board = new Board();
            this.board.initializeBoard(0);
        }
        /**
        * the program get called once the client pressed the start game button.
        * the program initializes the board and sends messeges of the board to the client.
        * @param BoardLayout - the game layout.
*/
        public void StartGamePlayerVsPlayer(int BoardLayout)
        {
            //init the board values
            //and send data to client
            board.initializeBoard(BoardLayout);
            Pturn = new PlayerTurnManager(board);
            isPVP = true;
            isWaitingToPlayer = true;
        }
        /**
* program gets called when client presses "start game player vs AI"
* program initialize required parts in code.
* @param BoardLayout - the game layout.
*/
        public void StartGamePlayerVsAI(int BoardLayout)
        {
            board.initializeBoard(BoardLayout);
            Pturn = new PlayerTurnManager(board);
            isPVP = false;
            //AI = new AIManager(-1);
            isWaitingToPlayer = true;
        }
        /**
        * program switches beetween players/AI.
        * If the game mode is pvAI than its activates the ai.
*/
        private void switchPlayers()
        {
            if (isPVP == true)
            {
                currentplayer = currentplayer * -1;
            }
            //else
            //{
            //    //switch to player -> AI -> AI or AI-> player
            //    //1 is player
            //    if (currentplayer == 1)
            //    {
            //        //switch from player to AI
            //        Console.WriteLine("swithced player from " + currentplayer + " to AI turn as " + currentplayer * -1);
            //        isWaitingToPlayer = false;
            //        //gets move from AI
            //        //Move AIMove = AI.playTurn(board, currentplayer * -1);
            //        //mark move to client
            //        // implementing move in game board
            //        //int iswin = board.makeMove(AIMove);
            //    }
            //    if (iswin == 1)
            //        this.WinFound(currentplayer * -1);
            //    isWaitingToPlayer = true;
            //}
            //after turn has been committed, its player turn again.
        }
        /**
        * program recives a press index of client, sends the index to the turn
        * manager which returns a move.
        * if it is not end of turn than move = null.
        * @param index - client press index.
        */
        public int rereceivedMessage(int index)
        {
            int iswin = 0;
            if (isWaitingToPlayer)
            {
                //manages the player turn input.
                Move move = Pturn.recivedPress((sbyte)index, currentplayer);
                if (move != null)
                {
                    //implements the move inside the board.
                    iswin = board.makeMove(move);
                    if (iswin == 1)
                        return 2; // there was a move and a win.
                    else
                    {
                        switchPlayers();
                        return 1; // there was a move but no win.
                    }
                }
                return 0;
            }
            return 0;
        }
        public int getCurrentPlayer()
        {
            return currentplayer;
        }

        public string PlayerTurn()
        {
            if (currentplayer == 1)
                return "White";
            return "Black";
        }

        public bool isGameOver()
        {
            return (board.getScoreBlack() == 6 || board.getScoreWhite() == 6);
        }
    }
}

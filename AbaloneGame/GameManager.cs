using AbaloneGame.model;
using AbaloneGame.view;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        private AIManager AI;
        //game mode
        bool isPVP;
        //is waiting for user input.
        bool isWaitingToPlayer = false;
        /// <summary>
        /// constructor.
        /// </summary>
        public GameManager()
        {
            this.currentplayer = 1;
            this.board = new Board();
        }
        /// <summary>
        /// the program is being called once after the client pressed on the start player vs player game button.
        /// the program initializes the board correspondingly.
        /// </summary>
        /// <param name="BoardLayout">the game layout.</param>
        public void StartGamePlayerVsPlayer(int BoardLayout)
        {
            //init the board values
            //and send data to client
            board.initializeBoard(BoardLayout);
            Pturn = new PlayerTurnManager(board);
            isPVP = true;
            isWaitingToPlayer = true;
        }
        /// <summary>
        /// the program get called once after the client pressed on the start player vs computer game button.
        /// the program initializes the board correspondingly.
        /// </summary>
        /// <param name="BoardLayout">the game layout.</param>
        public void StartGamePlayerVsAI(int BoardLayout)
        {
            board.initializeBoard(BoardLayout);
            Pturn = new PlayerTurnManager(board);
            isPVP = false;
            AI = new AIManager(-1);
            isWaitingToPlayer = true;
        }
        /// <summary>       
        /// program switches beetween players/AI.
        /// If the game mode is Player vs AI than its activates the  AI turn.
        /// </summary>
        /// <param name="label">the label that indicates which one turn is it.</param>
        /// <returns>1 if a win was found, 0 otherwise.</returns>
        public int switchPlayers(Label label)
        {
            int iswin = 0;
            if (isPVP == true)
            {
                currentplayer = currentplayer * -1;
                return 0;
            }
            else
            {//AI
                //switch to player -> AI -> AI or AI-> player
                //1 is player
                if (currentplayer == 1)
                {
                    //switch from player to AI
                    isWaitingToPlayer = false;

                    //switch player 
                    currentplayer = currentplayer * -1;
                    label.Text = PlayerTurn() + " Player turn.";
                    label.Refresh();
                    
                    //gets move from AI
                    Move AIMove = AI.playTurn(board, currentplayer);

                    // implementing move in game board
                    iswin = board.makeMove(AIMove); 
                    if (iswin == 1)
                    {
                        return iswin;
                    }
                    currentplayer = currentplayer * -1;
                }
                isWaitingToPlayer = true;
                return iswin;
            }
            //after turn has been committed, its player turn again.
        }
        /// <summary>
        /// program recives a press index of client, sends the index to the turn
        /// manager which returns a move.
        /// if it is not end of turn than move = null.
        /// </summary>
        /// <param name="index">the index of the tile in the board that have been pressed.</param>
        /// <param name="pictureBox">the picturebox that contains the board.</param>
        /// <param name="label">the label that indicates which one turn is it.</param>
        /// <returns> 0 - if there wasn't a move.
        /// 1- if there was amove with no win.
        /// 2- if there was a move with a win.
        /// </returns>
        public int rereceivedMessage(int index, PictureBox pictureBox, Label label)
        {
            Graphics g = pictureBox.CreateGraphics();
            int iswin = 0;
            if (isWaitingToPlayer)
            {
                //manages the player turn input.
                Move move = Pturn.recivedPress((sbyte)index, currentplayer, pictureBox);
                if (move != null)
                {
                    //implements the move inside the board.
                    iswin = board.makeMove(move);
                    /*GraphicsManager.PaintBoard(g, this.Board);
                    pictureBox.Invalidate();*/
                    pictureBox.Refresh();
                    //label.Refresh();
                    if (iswin == 1)
                        return 2; // there was a move and a win.
                    else
                    {
                        if (switchPlayers(label) == 1)
                            return 2; // there was a move and there was a win
                        return 1; // there was a move but no win.
                    }
                }
                return 0;
            }
            return 0;
        }
        /// <summary>
        /// returns an int that indicates which player's turn it is.
        /// </summary>
        /// <returns> 
        /// (-1) - for black
        /// 1 - for white
        /// </returns>
        public int getCurrentPlayer()
        {
            return currentplayer;
        }
        /// <summary>
        /// returns a string that indicates which player's turn it is.
        /// </summary>
        /// <returns> 
        /// Black - for black
        /// White - for white
        /// </returns>
        public string PlayerTurn()
        {
            if (currentplayer == 1)
                return "White";
            return "Black";
        }
        /// <summary>
        /// return's true if the score of one of the players is above 6.
        /// </summary>
        /// <returns>true if game is over, false otherwise.</returns>
        public bool isGameOver()
        {
            return (board.getScoreBlack() == 6 || board.getScoreWhite() == 6);
        }
    }
}

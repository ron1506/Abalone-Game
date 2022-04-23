using AbaloneGame.model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbaloneGame.view
{
    class GraphicsManager
    {
        private const int RADIUS = 53;
        private static Dictionary<int, int[]> index_to_position = new Dictionary<int, int[]>();
        /// <summary>
        /// initializing the dictionary that contains the cordinates of every tile in the board.
        /// </summary>
        public static void init_dictionary()
        {
            try
            {
                index_to_position.Add(12, new int[2] { 170, 448 });
                index_to_position.Add(13, new int[2] { 144, 400 });
                index_to_position.Add(14, new int[2] { 115, 350 });
                index_to_position.Add(15, new int[2] { 86, 300 });
                index_to_position.Add(16, new int[2] { 57, 250 });

                index_to_position.Add(23, new int[2] { 228, 448 });
                index_to_position.Add(24, new int[2] { 201, 400 });
                index_to_position.Add(25, new int[2] { 172, 350 });
                index_to_position.Add(26, new int[2] { 143, 300 });
                index_to_position.Add(27, new int[2] { 114, 250 });
                index_to_position.Add(28, new int[2] { 88, 200 });

                index_to_position.Add(34, new int[2] { 286, 448 });
                index_to_position.Add(35, new int[2] { 258, 400 });
                index_to_position.Add(36, new int[2] { 229, 350 });
                index_to_position.Add(37, new int[2] { 200, 300 });
                index_to_position.Add(38, new int[2] { 171, 250 });
                index_to_position.Add(39, new int[2] { 142, 200 });
                index_to_position.Add(40, new int[2] { 115, 153 });

                index_to_position.Add(45, new int[2] { 343, 447 });
                index_to_position.Add(46, new int[2] { 315, 400 });
                index_to_position.Add(47, new int[2] { 286, 350 });
                index_to_position.Add(48, new int[2] { 257, 300 });
                index_to_position.Add(49, new int[2] { 228, 250 });
                index_to_position.Add(50, new int[2] { 199, 200 });
                index_to_position.Add(51, new int[2] { 173, 153 });
                index_to_position.Add(52, new int[2] { 145, 106 });

                index_to_position.Add(56, new int[2] { 397, 445 });
                index_to_position.Add(57, new int[2] { 370, 398 });
                index_to_position.Add(58, new int[2] { 343, 350 });
                index_to_position.Add(59, new int[2] { 314, 300 });
                index_to_position.Add(60, new int[2] { 285, 250 });
                index_to_position.Add(61, new int[2] { 256, 200 });
                index_to_position.Add(62, new int[2] { 229, 152 });
                index_to_position.Add(63, new int[2] { 200, 103 });
                index_to_position.Add(64, new int[2] { 171, 55 });

                index_to_position.Add(68, new int[2] { 427, 398 });
                index_to_position.Add(69, new int[2] { 400, 350 });
                index_to_position.Add(70, new int[2] { 371, 300 });
                index_to_position.Add(71, new int[2] { 342, 250 });
                index_to_position.Add(72, new int[2] { 313, 200 });
                index_to_position.Add(73, new int[2] { 287, 153 });
                index_to_position.Add(74, new int[2] { 259, 105 });
                index_to_position.Add(75, new int[2] { 229, 55 });

                index_to_position.Add(80, new int[2] { 457, 350 });
                index_to_position.Add(81, new int[2] { 428, 300 });
                index_to_position.Add(82, new int[2] { 399, 250 });
                index_to_position.Add(83, new int[2] { 370, 200 });
                index_to_position.Add(84, new int[2] { 341, 153 });
                index_to_position.Add(85, new int[2] { 312, 105 });
                index_to_position.Add(86, new int[2] { 283, 55 });

                index_to_position.Add(92, new int[2] { 485, 300 });
                index_to_position.Add(93, new int[2] { 456, 250 });
                index_to_position.Add(94, new int[2] { 427, 200 });
                index_to_position.Add(95, new int[2] { 400, 153 });
                index_to_position.Add(96, new int[2] { 370, 105 });
                index_to_position.Add(97, new int[2] { 342, 55 });

                index_to_position.Add(104, new int[2] { 513, 250 });
                index_to_position.Add(105, new int[2] { 484, 200 });
                index_to_position.Add(106, new int[2] { 455, 153 });
                index_to_position.Add(107, new int[2] { 428, 105 });
                index_to_position.Add(108, new int[2] { 400, 57 });
            }
            catch (System.ArgumentException)
            {

            }

        }
        /// <summary>
        /// the method paints the board, using graphics.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="board">the game board.</param>
        public static void PaintBoard(Graphics g, Board board)
        {
            for (int i = 0; i < 121; i++)
            { // for each bit in the 121 bits in the BitSet.
                if (board.getEdgeOfBoard().get(i))
                { //in the board
                    if (board.getBlackSet().get(i))
                    {//draw the circle black.
                        g.FillEllipse(Brushes.Black, (float)(index_to_position[i][0] - RADIUS), (float)(index_to_position[i][1] - RADIUS), RADIUS, RADIUS);
                    }
                    else if (board.getWhiteSet().get(i))
                    {//draw the circle white.
                        g.FillEllipse(Brushes.White, (float)(index_to_position[i][0] - RADIUS), (float)(index_to_position[i][1] - RADIUS), RADIUS, RADIUS);
                    }
                }
            }
        }
        /// <summary>
        /// the function draws a circle around the chosen players to move.
        /// </summary>
        /// <param name="g">graphics</param>
        /// <param name="x">the x coordinate </param>
        /// <param name="y">the y coordinate</param>
        /// <param name="currentplayer">the player who's turn is.</param>
        /// <param name="board">the game board.</param>
        /// <returns></returns>
        public static int Choose_Player(Graphics g, int x, int y, int currentplayer, Board board)
        {
            int centerX = 0, centerY = 0;
            Pen pen = new Pen(Brushes.Blue);
            pen.Width = 2.5F;
            foreach (int key in index_to_position.Keys)
            {
                if (Math.Sqrt(Math.Pow(index_to_position[key][0] - x - 25, 2) + Math.Pow(index_to_position[key][1] - y - 25, 2)) < RADIUS - 28) //in the circle
                {
                    if (currentplayer == board.GetValueInPosition((sbyte)key)) // if from the same color than circlr it with blue circle
                        g.DrawEllipse(pen, index_to_position[key][0] - RADIUS, index_to_position[key][1] - RADIUS, RADIUS, RADIUS);
                    centerX = index_to_position[key][0];
                    centerY = index_to_position[key][1];
                }
            }
            foreach (int key in index_to_position.Keys)
            {
                if (index_to_position[key][0] == centerX && index_to_position[key][1] == centerY)
                    return key;
            }
            return 0;
        }
    }
}

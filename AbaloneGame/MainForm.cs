using AbaloneGame.model;
using AbaloneGame.view;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AbaloneGame
{
    public partial class MainForm : Form
    {
        GameManager gm;

        /// <summary>
        /// Constructor, creates the MainForm.
        /// </summary>
        /// <param name="isPvP">if the mode is player vs. player.</param>
        /// <param name="layout">the board chosen layout.</param>
        public MainForm(bool isPvP, int layout)
        {
            gm = new GameManager();
            if (isPvP)
                gm.StartGamePlayerVsPlayer(layout);
            else
            {
                //AI
                gm.StartGamePlayerVsAI(layout);
            }
            GraphicsManager.init_dictionary();
            InitializeComponent();
        }
        /// <summary>
        /// defaulted method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// drawing the board.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsManager.PaintBoard(e.Graphics, gm.Board);
            label3.Text = gm.PlayerTurn() + " Player turn.";
        }
        /// <summary>
        /// OnClick method, deals if a circle is chosen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_onClick(object sender, MouseEventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            int index = GraphicsManager.Choose_Player(g, e.X, e.Y, gm.getCurrentPlayer(), gm.Board);
            int state = gm.rereceivedMessage(index, pictureBox1, label3);
            if (state == 1 || state == 2)
            {//a move was played
                pictureBox1.Invalidate();
                label1.Text = gm.Board.getScoreBlack().ToString();
                label2.Text = gm.Board.getScoreWhite().ToString();
            }
            if (gm.isGameOver())
            {
                MessageBox.Show("Game is over " + gm.PlayerTurn() + " won."); //show who won.
            }
        }
        /// <summary>
        /// back to open screen form, OnClick method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm of = new OpenForm();
            this.Hide();
            of.Show();
        }
    }
}

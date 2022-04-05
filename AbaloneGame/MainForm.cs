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
        private bool isPvP;
        private int layout;
        //PlayerTurnManager tm;
        public MainForm(bool isPvP, int layout)
        {
            gm = new GameManager();
            if (isPvP)
                gm.StartGamePlayerVsPlayer(layout);
            else
            {
                //AI
            }
            GraphicsManager.init_dictionary();
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            GraphicsManager.PaintBoard(e.Graphics, gm.Board);
            label3.Text = gm.PlayerTurn() + " Player turn.";
        }

        private void pictureBox1_onClick(object sender, MouseEventArgs e)
        {
            int index = GraphicsManager.Choose_Player(pictureBox1.CreateGraphics(), e.X, e.Y, gm.getCurrentPlayer(), gm.Board);
            int state = gm.rereceivedMessage(index);
            if (state == 1 || state == 2)
            {
                pictureBox1.Invalidate();
                label1.Text = gm.Board.getScoreBlack().ToString();
                label2.Text = gm.Board.getScoreWhite().ToString();
            }
            if (gm.isGameOver())
            {
                MessageBox.Show("Game is over " + gm.PlayerTurn() + " won.");

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm of = new OpenForm();
            this.Hide();
            of.Show();
        }
    }
}

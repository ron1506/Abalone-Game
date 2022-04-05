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
    public partial class ChooseLayoutForm : Form
    {
        private bool isPvP;
        public ChooseLayoutForm(bool isPvP)
        {
            this.isPvP = isPvP;
            InitializeComponent();
            label1.Text = "Choose the Board Layout";
            button1.Text = "Classic Layout";
            button2.Text = "Pro Layout";
            button3.Text = "Snake Layout";
            button4.Text = "Wall Layout";
            button5.Text = "Back";
        }
        /// <summary>
        /// Wall layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm(this.isPvP, 3);
            this.Hide();
            mf.Show();
        }
        /// <summary>
        /// snake layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm(this.isPvP, 2);
            this.Hide();
            mf.Show();
        }
        /// <summary>
        /// pro layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm(this.isPvP, 1);
            this.Hide();
            mf.Show();
        }

        /// <summary>
        /// classic layout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            MainForm mf = new MainForm(this.isPvP, 0);
            this.Hide();
            mf.Show();
        }
        /// <summary>
        /// return button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            OpenForm mf = new OpenForm();
            this.Hide();
            mf.Show();
        }
    }
}

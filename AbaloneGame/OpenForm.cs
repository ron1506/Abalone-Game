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
    public partial class OpenForm : Form
    {
        public OpenForm()
        {
            InitializeComponent();
            label1.Text = "Ron's Abalone Game";
            button1.Text = "Player vs. Player";
            button2.Text = "Player vs. Computer";
        }
        /// <summary>
        /// PvP Game button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            ChooseLayoutForm clf = new ChooseLayoutForm(true);
            this.Hide();
            clf.Show();
        }
        /// <summary>
        /// AI Game Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            ChooseLayoutForm clf = new ChooseLayoutForm(false);
            this.Hide();
            clf.Show();
        }
    }
}

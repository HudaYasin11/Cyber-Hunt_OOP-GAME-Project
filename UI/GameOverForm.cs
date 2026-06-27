using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberHunt.UI
{
    public partial class GameOverForm : Form
    {
        public GameOverForm()
        {
            InitializeComponent();
        }

        public GameOverForm(bool isWin, int finalScore)
        {
            InitializeComponent();

            if (isWin)
            {
                lblMessage.Text = "YOU WIN!";
                lblMessage.ForeColor = Color.Green;
                lblMessage.BackColor = Color.Black;
            }
            else
            {
                lblMessage.Text = "YOU LOSE!";
                lblMessage.ForeColor = Color.Red;
                lblMessage.BackColor = Color.Black;
            }

            lblScore.Text = "Final Score: " + finalScore;
            lblScore.ForeColor = Color.White;
            lblScore.BackColor = Color.Black;

            lblMessage.Visible = true;
            lblScore.Visible = true;

            lblMessage.BringToFront();
            lblScore.BringToFront();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Yes;
            this.Close();
        }
    }
}
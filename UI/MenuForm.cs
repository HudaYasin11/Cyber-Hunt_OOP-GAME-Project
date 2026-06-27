using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberHunt.UI
{
    public partial class MenuForm : Form
    {
        public MenuForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Level1 game = new Level1();

            game.Show();

            this.Hide();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
  "ARROW KEYS = MOVE\n\nSPACE = SHOOT\n\nDestroy enemies and survive!",
  "Instructions"
  );
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Level1 game = new Level1();

            
            game.Show();

           
            this.Hide();
        }
    }
}

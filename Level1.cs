using CyberHunt.Game;
using CyberHunt.GameObjects;
using EZInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CyberHunt
{
    public partial class Level1 : Form
    {

        private GameManager gameManager;

        public Level1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.KeyPreview = true;

            gameManager = new GameManager(this, gameTimer, enemyShootTimer);
            gameManager.Start();
        }
      
        
        private void gameTimer_Tick(object sender, EventArgs e)
        {
            gameManager.Update();
        }


        private void enemyShootTimer_Tick(object sender, EventArgs e)
        {
            gameManager.EnemyShoot();
        }
       

    }
}



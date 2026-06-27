using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberHunt.GameObjects
{
    internal class HealthPill : GameObject
    {
        private Random rand = new Random();

        public HealthPill(int formWidth, int formHeight)
        {
            Sprite = new PictureBox();
            Sprite.BackColor = Color.LimeGreen;
            Sprite.Width = 25;
            Sprite.Height = 25;
            Sprite.Location = new Point(
                rand.Next(0, formWidth - 25),
                rand.Next(0, formHeight - 25)
            );

            X = Sprite.Left;
            Y = Sprite.Top;
            IsAlive = true;
        }

        public void Respawn(int formWidth, int formHeight)
        {
            X = rand.Next(0, formWidth - Sprite.Width);
            Y = rand.Next(0, formHeight - Sprite.Height);
            Sprite.Location = new Point(X, Y);
            IsAlive = true;
            Sprite.Show();
        }

        public override void Destroy()
        {
            IsAlive = false;
            Sprite.Hide();
        }
        public override void Update(int formWidth, int formHeight)
        {
            // Optional: Add pulsing animation
            int pulse = (int)(Math.Sin(DateTime.Now.Millisecond / 300.0) * 3);
            Sprite.Width = 25 + pulse;
            Sprite.Height = 25 + pulse;
            Sprite.Left = X - (pulse / 2);
            Sprite.Top = Y - (pulse / 2);
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using CyberHunt.Enums;

namespace CyberHunt.GameObjects
{
    internal class Enemy : GameObject
    {
        private int currentLevel = 1;
        private int verticalSpeed = 5;
        private int horizontalSpeed = 8;
        private int diagonalXSpeed = 6;
        private int diagonalYSpeed = 5;
        private Random rand = new Random();

        public Enemy(Image img, int x, int y, int level)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.Width = 120;
            Sprite.Height = 70;
            Sprite.SizeMode = PictureBoxSizeMode.Zoom;
            Sprite.BackColor = Color.Transparent;
            Sprite.Location = new Point(x, y);

            X = x;
            Y = y;
            currentLevel = level;
            IsAlive = true;
        }

        public void Move(int formWidth, int formHeight)
        {
            if (currentLevel == 1) 
            {
                Y += verticalSpeed;
                if (Y > formHeight)
                {
                    Y = -Sprite.Height;
                    X = rand.Next(0, formWidth - Sprite.Width);
                }
            }
            else if (currentLevel == 2)  
            {
                X += horizontalSpeed;
                if (X > formWidth)
                {
                    X = -Sprite.Width;
                    Y = rand.Next(0, formHeight - Sprite.Height);
                }
            }
            else if (currentLevel == 3)  
            {
                X += diagonalXSpeed;
                Y += diagonalYSpeed;
                if (Y > formHeight || X > formWidth)
                {
                    Y = -Sprite.Height;
                    X = rand.Next(0, formWidth - Sprite.Width);
                }
            }
        }

        public Bullet Fire(Image bulletImg)
        {
            Bullet bullet = new Bullet(
                bulletImg,
                X + Sprite.Width / 2,
                Y + Sprite.Height,
                BulletDirection.Down
            );
            return bullet;
        }

        public void UpdateLevel(int newLevel)
        {
            currentLevel = newLevel;
        }

        public void Respawn(int formWidth, int formHeight)
        {
            Y = -Sprite.Height;
            X = rand.Next(0, formWidth - Sprite.Width);
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
            if (currentLevel == 1)
            {
                Y += verticalSpeed;
                if (Y > formHeight)
                {
                    Y = -Sprite.Height;
                    X = rand.Next(0, formWidth - Sprite.Width);
                }
            }
            else if (currentLevel == 2)
            {
                X += horizontalSpeed;
                if (X > formWidth)
                {
                    X = -Sprite.Width;
                    Y = rand.Next(0, formHeight - Sprite.Height);
                }
            }
            else if (currentLevel == 3)
            {
                X += diagonalXSpeed;
                Y += diagonalYSpeed;
                if (Y > formHeight || X > formWidth)
                {
                    Y = -Sprite.Height;
                    X = rand.Next(0, formWidth - Sprite.Width);
                }
            }
        }
    }
}
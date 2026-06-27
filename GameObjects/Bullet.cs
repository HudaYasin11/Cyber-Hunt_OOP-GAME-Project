using System.Drawing;
using System.Windows.Forms;
using CyberHunt.Enums;

namespace CyberHunt.GameObjects
{
    internal class Bullet : GameObject
    {
        private BulletDirection direction;
        private int speed = 20;

        public Bullet(Image img, int x, int y, BulletDirection direction)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.BackColor = Color.Transparent;
            Sprite.Width = img.Width;
            Sprite.Height = img.Height;
            Sprite.Location = new Point(x, y);

            this.direction = direction;
            X = x;
            Y = y;
            IsAlive = true;
        }

        public void Move()
        {
            if (direction == BulletDirection.Up)
            {
                Y -= speed;
            }
            else
            {
                Y += speed;
            }
        }

        public bool IsOffScreen(int formHeight)
        {
            return (Y < 0 || Y > formHeight);
        }

        public override void Destroy()
        {
            IsAlive = false;
            Sprite.Hide();
        }
        public override void Update(int formWidth, int formHeight)
        {
            if (direction == BulletDirection.Up)
            {
                Y -= speed;
            }
            else
            {
                Y += speed;
            }
        }
    }
}
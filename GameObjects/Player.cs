using System.Drawing;
using System.Windows.Forms;
using CyberHunt.Enums;

namespace CyberHunt.GameObjects
{
    internal class Player : GameObject
    {
        private int speed = 10;
        private int health = 100;
        private int maxHealth = 100;
        private ProgressBar healthBar;

        public int Health
        {
            get { return health; }
            set
            {
                health = value;
                if (health < 0) health = 0;
                if (health > maxHealth) health = maxHealth;
                if (healthBar != null) healthBar.Value = health;
            }
        }

        public int MaxHealth => maxHealth;
        public ProgressBar HealthBar => healthBar;

        public Player(Image img, int x, int y)
        {
            Sprite = new PictureBox();
            Sprite.Image = img;
            Sprite.Width = img.Width;
            Sprite.Height = img.Height;
            Sprite.SizeMode = PictureBoxSizeMode.Zoom;
            Sprite.BackColor = Color.Transparent;
            Sprite.Location = new Point(x, y);

            healthBar = new ProgressBar();
            healthBar.Maximum = maxHealth;
            healthBar.Value = health;
            healthBar.Width = 60;
            healthBar.Height = 15;
            healthBar.Location = new Point(x, y + img.Height);

            X = x;
            Y = y;
            IsAlive = true;
        }

        public void MoveLeft()
        {
            X -= speed;
            UpdateHealthBarPosition();
        }

        public void MoveRight()
        {
            X += speed;
            UpdateHealthBarPosition();
        }

        public void MoveUp()
        {
            Y -= speed;
            UpdateHealthBarPosition();
        }

        public void MoveDown()
        {
            Y += speed;
            UpdateHealthBarPosition();
        }

        private void UpdateHealthBarPosition()
        {
            if (healthBar != null)
                healthBar.Location = new Point(X, Y + Sprite.Height);
        }

        public void CheckBoundary(int formWidth, int formHeight)
        {
            if (X < 0) X = 0;
            if (X + Sprite.Width > formWidth) X = formWidth - Sprite.Width;
            if (Y < 0) Y = 0;
            if (Y + Sprite.Height > formHeight) Y = formHeight - Sprite.Height;
            UpdateHealthBarPosition();
        }

        public Bullet Fire(Image bulletImg)
        {
            Bullet bullet = new Bullet(
                bulletImg,
                X + Sprite.Width / 2 - 2,
                Y - bulletImg.Height,
                BulletDirection.Up
            );
            return bullet;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health <= 0)
            {
                Destroy();
            }
        }

        public void Heal(int amount)
        {
            Health += amount;
        }

        public override void Destroy()
        {
            IsAlive = false;
            Sprite.Hide();
            if (healthBar != null) healthBar.Hide();
        }

        public override void Update(int formWidth, int formHeight)
        {
            CheckBoundary(formWidth, formHeight);
        }
    }
}
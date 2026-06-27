using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CyberHunt.GameObjects;
using CyberHunt.Managers;
using CyberHunt.UI;
using CyberHunt.Interfaces;

namespace CyberHunt.Game
{
    internal class GameManager : IGameManager
    {
        private Form form;
        private Player player;
        private List<PictureBox> enemies;
        private List<PictureBox> playerBullets;
        private List<PictureBox> enemyBullets;

        private InputManager inputManager;
        private CollisionManager collisionManager;
        private LevelManager levelManager;
        private SoundManager soundManager;

        private Random rand;
        private PictureBox healthPill;
        private int pillSpawnDelay;
        private int pillSpawnInterval;

        private int score;
        private Timer gameTimer;
        private Timer enemyShootTimer;

        private PictureBox scoringPill;
        private int scoringPillSpawnDelay = 0;
        private int scoringPillSpawnInterval = 400;

        public GameManager(Form form, Timer gameTimer, Timer enemyShootTimer)
        {
            this.form = form;
            this.gameTimer = gameTimer;
            this.enemyShootTimer = enemyShootTimer;

            inputManager = new InputManager();
            collisionManager = new CollisionManager();
            levelManager = new LevelManager();
            soundManager = new SoundManager();

            enemies = new List<PictureBox>();
            playerBullets = new List<PictureBox>();
            enemyBullets = new List<PictureBox>();

            rand = new Random();
            score = 0;
            pillSpawnDelay = 0;
            pillSpawnInterval = 300;
        }

        public void Start()
        {
            LoadSounds();
            CreatePlayer();
            CreateEnemies();
            StartTimers();
        }

        private void LoadSounds()
        {
            string shootPath = Application.StartupPath + @"\Sounds\shoot.wav";
            string collisionPath = Application.StartupPath + @"\Sounds\collosion.wav";
            soundManager.LoadSounds(shootPath, collisionPath);
        }

        private void CreatePlayer()
        {
            Image playerImg = Properties.Resources.playerShip;

            int newWidth = 60;
            int newHeight = 60;

            int x = form.ClientSize.Width / 2 - (newWidth / 2);
            int y = form.ClientSize.Height - newHeight - 20;

            player = new Player(playerImg, x, y);
            player.Sprite.Width = newWidth;
            player.Sprite.Height = newHeight;
            player.Sprite.SizeMode = PictureBoxSizeMode.StretchImage;
            player.Sprite.BackColor = Color.Transparent;

            form.Controls.Add(player.Sprite);
            form.Controls.Add(player.HealthBar);  // ← ADD HEALTH BAR
        }

        // ❌ DELETE THE DUPLICATE CreatePlayer() BELOW
        // private void CreatePlayer() { ... }  ← REMOVE THIS

        private void CreateEnemies()
        {
            Image enemyImg1 = Properties.Resources.enemy1;
            Image enemyImg2 = Properties.Resources.enemy2;
            Image enemyImg3 = Properties.Resources.enemy3;

            int enemyWidth = 60;
            int enemyHeight = 60;

            // Enemy 1
            PictureBox enemy1 = new PictureBox();
            enemy1.Image = enemyImg1;
            enemy1.Width = enemyWidth;
            enemy1.Height = enemyHeight;
            enemy1.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy1.BackColor = Color.Transparent;
            enemy1.Location = new Point(50, 50);
            enemy1.Tag = "3";
            form.Controls.Add(enemy1);
            enemies.Add(enemy1);

            // Enemy 2
            PictureBox enemy2 = new PictureBox();
            enemy2.Image = enemyImg2;
            enemy2.Width = enemyWidth;
            enemy2.Height = enemyHeight;
            enemy2.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy2.BackColor = Color.Transparent;
            enemy2.Location = new Point(200, 50);
            enemy2.Tag = "3";
            form.Controls.Add(enemy2);
            enemies.Add(enemy2);

            // Enemy 3
            PictureBox enemy3 = new PictureBox();
            enemy3.Image = enemyImg3;
            enemy3.Width = enemyWidth;
            enemy3.Height = enemyHeight;
            enemy3.SizeMode = PictureBoxSizeMode.StretchImage;
            enemy3.BackColor = Color.Transparent;
            enemy3.Location = new Point(350, 50);
            enemy3.Tag = "3";
            form.Controls.Add(enemy3);
            enemies.Add(enemy3);
        }

        private void StartTimers()
        {
            gameTimer.Start();
            enemyShootTimer.Start();
        }

        public void Update()
        {
            if (inputManager.MoveLeft()) player.MoveLeft();
            if (inputManager.MoveRight()) player.MoveRight();
            if (inputManager.MoveUp()) player.MoveUp();
            if (inputManager.MoveDown()) player.MoveDown();

            player.CheckBoundary(form.ClientSize.Width, form.ClientSize.Height);

            if (inputManager.Fire())
            {
                MakeBullet();
                soundManager.PlayShootSound();
            }

            if (inputManager.Escape())
            {
                gameTimer.Stop();
                enemyShootTimer.Stop();
                MenuForm menu = new MenuForm();
                menu.Show();
                form.Close();
            }

            UpdateUI();

            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                PictureBox bullet = playerBullets[i];
                bullet.Top -= 20;

                if (bullet.Top < 0)
                {
                    form.Controls.Remove(bullet);
                    playerBullets.RemoveAt(i);
                    bullet.Dispose();
                    continue;
                }

                foreach (PictureBox enemy in enemies)
                {
                    if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        soundManager.PlayCollisionSound();
                        form.Controls.Remove(bullet);
                        playerBullets.RemoveAt(i);
                        bullet.Dispose();

                        int currentHealth = int.Parse(enemy.Tag.ToString());
                        currentHealth--;

                        if (currentHealth <= 0)
                        {
                            enemy.Top = -enemy.Height;
                            enemy.Left = rand.Next(0, form.ClientSize.Width - enemy.Width);
                            enemy.Tag = "3";

                            score += 10;
                            levelManager.EnemyKilled();
                        }
                        else
                        {
                            enemy.Tag = currentHealth.ToString();
                            enemy.BackColor = Color.Red;
                            Timer flashTimer = new Timer();
                            flashTimer.Interval = 100;
                            flashTimer.Tick += (s, e) => { enemy.BackColor = Color.Transparent; flashTimer.Stop(); };
                            flashTimer.Start();
                        }

                        UpdateUI();
                        break;
                    }
                }
            }

            foreach (PictureBox enemy in enemies)
            {
                if (player.Bounds.IntersectsWith(enemy.Bounds))
                {
                    soundManager.PlayCollisionSound();
                    enemy.Top = -enemy.Height;
                    enemy.Left = rand.Next(0, form.ClientSize.Width - enemy.Width);
                    player.TakeDamage(10);
                    UpdateUI();

                    if (player.Health <= 0)
                    {
                        GameOver(false);
                    }
                    break;
                }
            }

            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                PictureBox enemyBullet = enemyBullets[i];
                enemyBullet.Top += 15;

                if (enemyBullet.Top > form.ClientSize.Height)
                {
                    form.Controls.Remove(enemyBullet);
                    enemyBullets.RemoveAt(i);
                    enemyBullet.Dispose();
                    continue;
                }

                if (enemyBullet.Bounds.IntersectsWith(player.Bounds))
                {
                    soundManager.PlayCollisionSound();
                    form.Controls.Remove(enemyBullet);
                    enemyBullets.RemoveAt(i);
                    enemyBullet.Dispose();

                    player.TakeDamage(10);
                    UpdateUI();

                    if (player.Health <= 0)
                    {
                        GameOver(false);
                    }
                }
            }

            foreach (PictureBox enemy in enemies)
            {
                levelManager.UpdateEnemyMovement(form.ClientSize.Width, form.ClientSize.Height, enemy);
            }

            pillSpawnDelay--;
            if (pillSpawnDelay <= 0 && healthPill == null)
            {
                CreateHealthPill();
                pillSpawnDelay = pillSpawnInterval;
            }
            scoringPillSpawnDelay--;
            if (scoringPillSpawnDelay <= 0 && scoringPill == null)
            {
                CreateScoringPill();
                scoringPillSpawnDelay = scoringPillSpawnInterval;
            }

            if (healthPill != null && healthPill.Bounds.IntersectsWith(player.Bounds))
            {
                player.Heal(20);
                form.Controls.Remove(healthPill);
                healthPill.Dispose();
                healthPill = null;
                soundManager.PlayCollisionSound();
                UpdateUI();
            }
            if (scoringPill != null && scoringPill.Bounds.IntersectsWith(player.Bounds))
            {
                score += 50;
                form.Controls.Remove(scoringPill);
                scoringPill.Dispose();
                scoringPill = null;
                UpdateUI();
            }
            if (levelManager.IsLevelComplete())
            {
                if (levelManager.CurrentLevel == 3)
                {
                    GameOver(true);
                    return;
                }

                levelManager.AdvanceToNextLevel();
                MessageBox.Show($"Level {levelManager.CurrentLevel} Started!\nKill {levelManager.KillsNeeded} enemies to advance");
                ResetEnemiesForNewLevel();
                UpdateUI();
                return;
            }
        }

        private void MakeBullet()
        {
            PictureBox bullet = new PictureBox();
            bullet.Image = Properties.Resources.bullet;
            bullet.Width = Properties.Resources.bullet.Width;
            bullet.Height = Properties.Resources.bullet.Height;
            bullet.BackColor = Color.Transparent;
            bullet.Left = player.X + (player.Sprite.Width / 2) - (bullet.Width / 2);
            bullet.Top = player.Y;
            bullet.Tag = "bullet";
            form.Controls.Add(bullet);
            bullet.BringToFront();
            playerBullets.Add(bullet);
        }

        public void MakeEnemyBullet(PictureBox enemy)
        {
            PictureBox enemyBullet = new PictureBox();
            enemyBullet.Image = Properties.Resources.enemyBullet;
            enemyBullet.Width = Properties.Resources.enemyBullet.Width;
            enemyBullet.Height = Properties.Resources.enemyBullet.Height;
            enemyBullet.BackColor = Color.Transparent;
            enemyBullet.Left = enemy.Left + (enemy.Width / 2) - (enemyBullet.Width / 2);
            enemyBullet.Top = enemy.Bottom;
            enemyBullet.Tag = "enemyBullet";
            form.Controls.Add(enemyBullet);
            enemyBullet.BringToFront();
            enemyBullets.Add(enemyBullet);
        }

        private void CreateHealthPill()
        {
            healthPill = new PictureBox();
            healthPill.BackColor = Color.LimeGreen;
            healthPill.Width = 25;
            healthPill.Height = 25;
            healthPill.SizeMode = PictureBoxSizeMode.Zoom;
            healthPill.Left = rand.Next(0, form.ClientSize.Width - healthPill.Width);
            healthPill.Top = rand.Next(0, form.ClientSize.Height - healthPill.Height);
            healthPill.Tag = "healthPill";
            form.Controls.Add(healthPill);
            healthPill.BringToFront();
        }

        private void CreateScoringPill()
        {
            scoringPill = new PictureBox();
            scoringPill.BackColor = Color.Yellow;
            scoringPill.Width = 20;
            scoringPill.Height = 20;
            scoringPill.Left = rand.Next(0, form.ClientSize.Width - 20);
            scoringPill.Top = rand.Next(0, form.ClientSize.Height - 20);
            scoringPill.Tag = "scoringPill";
            form.Controls.Add(scoringPill);
        }

        private void ResetEnemiesForNewLevel()
        {
            foreach (PictureBox enemy in enemies)
            {
                enemy.Top = -enemy.Height;
                enemy.Left = rand.Next(0, form.ClientSize.Width - enemy.Width);
                enemy.Tag = "3";
            }
        }

        private void UpdateUI()
        {
            foreach (Control c in form.Controls)
            {
                if (c.Name == "txtScore")
                {
                    c.Text = "Score: " + score;
                }
                if (c.Name == "txtHealth")
                {
                    c.Text = "Health: " + player.Health;
                }
                if (c.Name == "txtLevel")
                {
                    c.Text = levelManager.GetLevelDisplayText();
                }
            }
        }

        public void EnemyShoot()
        {
            if (enemies.Count > 0)
            {
                int index = rand.Next(enemies.Count);
                MakeEnemyBullet(enemies[index]);
            }
        }

        private void GameOver(bool isWin)
        {
            gameTimer.Stop();
            enemyShootTimer.Stop();

            if (player.HealthBar != null)
                player.HealthBar.Hide();

            GameOverForm gameOver = new GameOverForm(isWin, score);
            DialogResult result = gameOver.ShowDialog();

            if (result == DialogResult.Yes)
            {
                Application.Restart();
            }
            else
            {
                MenuForm menu = new MenuForm();
                menu.Show();
                form.Close();
            }
        }
    }
}
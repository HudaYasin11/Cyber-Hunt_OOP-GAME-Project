using CyberHunt.GameObjects;
using System.Collections.Generic;
using System.Media;
using System.Windows.Forms;

namespace CyberHunt.Managers
{
    internal class CollisionManager
    {
       
        public void CheckBulletEnemyCollision(
            List<PictureBox> playerBullets,
            List<PictureBox> enemies,
            ref int score,
            ref int enemiesKilled,
            ref int currentLevel,
            ref bool levelUp,
            SoundPlayer collisionSound,
            Form form)
        {
            for (int i = playerBullets.Count - 1; i >= 0; i--)
            {
                PictureBox bullet = playerBullets[i];

                foreach (PictureBox enemy in enemies)
                {
                    if (bullet.Bounds.IntersectsWith(enemy.Bounds))
                    {
                        collisionSound.Play();
                        form.Controls.Remove(bullet);
                        playerBullets.RemoveAt(i);
                        bullet.Dispose();

                        enemy.Top = 0;
                        enemy.Left = new System.Random().Next(0, form.Width - enemy.Width);

                       
                        score += 10;
                        enemiesKilled++;

                        break;
                    }
                }
            }
        }

        public bool CheckBulletPlayerCollision(
            List<PictureBox> enemyBullets,
            Player player,
            SoundPlayer collisionSound,
            Form form)
        {
            for (int i = enemyBullets.Count - 1; i >= 0; i--)
            {
                PictureBox bullet = enemyBullets[i];

                if (bullet.Bounds.IntersectsWith(player.Bounds))
                {
                    collisionSound.Play();
                    form.Controls.Remove(bullet);
                    enemyBullets.RemoveAt(i);
                    bullet.Dispose();

                    player.TakeDamage(10);
                    return true;
                }
            }
            return false; 
        }

        public bool CheckEnemyPlayerCollision(
            List<PictureBox> enemies,
            Player player,
            SoundPlayer collisionSound,
            Form form)
        {
            foreach (PictureBox enemy in enemies)
            {
                if (enemy.Bounds.IntersectsWith(player.Bounds))
                {
                    collisionSound.Play();
                    enemy.Top = -enemy.Height;
                    enemy.Left = new System.Random().Next(0, form.Width - enemy.Width);

                    player.TakeDamage(10);
                    return true;
                }
            }
            return false;
        }
    }
}
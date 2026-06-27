using System;
using System.Windows.Forms;

namespace CyberHunt.Managers
{
    internal class LevelManager
    {
        private int currentLevel;
        private int enemiesKilledInCurrentLevel;
        private int[] killsNeeded;
        private Random rand;
        private bool levelCompletedHandled;
        public LevelManager()
        {
            currentLevel = 1;
            enemiesKilledInCurrentLevel = 0;
            killsNeeded = new int[] { 0, 8, 10, 15 }; 
            rand = new Random();
            levelCompletedHandled = false;
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
        }

        public int EnemiesKilledInCurrentLevel
        {
            get { return enemiesKilledInCurrentLevel; }
        }

        public int KillsNeeded
        {
            get { return killsNeeded[currentLevel]; }
        }

        public int KillsForLevel(int level)
        {
            if (level <= 3)
            {
                return killsNeeded[level];
            }
            return 0;
        }

        public void EnemyKilled()
        {
            enemiesKilledInCurrentLevel++;
        }

        public bool IsLevelComplete()
        {
            if (levelCompletedHandled)
                return false;

            bool complete = enemiesKilledInCurrentLevel >= killsNeeded[currentLevel];

            if (complete)
            {
                levelCompletedHandled = true; 
            }

            return complete;
        }

        public void AdvanceToNextLevel()
        {
            if (currentLevel < 3)
            {
                currentLevel++;
                enemiesKilledInCurrentLevel = 0;
                levelCompletedHandled = false; 
            }
        }

        public bool IsGameComplete()
        {
            return currentLevel > 3;
        }

        public void ResetLevel()
        {

            currentLevel = 1;
            enemiesKilledInCurrentLevel = 0;
            levelCompletedHandled = false; 
        }

        public string GetLevelDisplayText()
        {
            return $"Level: {currentLevel} ({enemiesKilledInCurrentLevel}/{killsNeeded[currentLevel]})";
        }

        public void UpdateEnemyMovement(int formWidth, int formHeight, PictureBox enemy)
        {
            if (currentLevel == 1)
            {
                enemy.Top += 5;
                if (enemy.Top > formHeight)
                {
                    enemy.Top = -enemy.Height;
                    enemy.Left = rand.Next(0, formWidth - enemy.Width);
                }
            }
            else if (currentLevel == 2)
            {
                enemy.Left += 8;
                if (enemy.Left > formWidth)
                {
                    enemy.Left = -enemy.Width;
                    enemy.Top = rand.Next(0, formHeight - enemy.Height);
                }
            }
            else if (currentLevel == 3)
            {
                enemy.Left += 6;
                enemy.Top += 5;
                if (enemy.Top > formHeight || enemy.Left > formWidth)
                {
                    enemy.Top = -enemy.Height;
                    enemy.Left = rand.Next(0, formWidth - enemy.Width);
                }
            }
        }

        public int GetEnemySpeed()
        {
            if (currentLevel == 1)
            {
                return 5;
            }
            else if (currentLevel == 2)
            {
                return 8;
            }
            else
            {
                return 6;
            }
        }
    }
}
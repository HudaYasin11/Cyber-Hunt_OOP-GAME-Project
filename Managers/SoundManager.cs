using System.Media;

namespace CyberHunt.Managers
{
    internal class SoundManager
    {
        private SoundPlayer shootSound;
        private SoundPlayer collisionSound;
        private bool soundEnabled;

        public SoundManager()
        {
            soundEnabled = true;
        }

        public void LoadSounds(string shootPath, string collisionPath)
        {
            shootSound = new SoundPlayer(shootPath);
            collisionSound = new SoundPlayer(collisionPath);

            shootSound.Load();
            collisionSound.Load();
        }

        public void PlayShootSound()
        {
            if (soundEnabled && shootSound != null)
            {
                shootSound.Play();
            }
        }

        public void PlayCollisionSound()
        {
            if (soundEnabled && collisionSound != null)
            {
                collisionSound.Play();
            }
        }

        public void EnableSound()
        {
            soundEnabled = true;
        }

        public void DisableSound()
        {
            soundEnabled = false;
        }

        public void ToggleSound()
        {
            soundEnabled = !soundEnabled;
        }

        public bool IsSoundEnabled()
        {
            return soundEnabled;
        }
    }
}
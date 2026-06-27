using EZInput;

namespace CyberHunt.Managers
{
    internal class InputManager
    {
        public bool MoveLeft()
        {
            return Keyboard.IsKeyPressed(Key.LeftArrow);
        }

        public bool MoveRight()
        {
            return Keyboard.IsKeyPressed(Key.RightArrow);
        }

        public bool MoveUp()
        {
            return Keyboard.IsKeyPressed(Key.UpArrow);
        }

        public bool MoveDown()
        {
            return Keyboard.IsKeyPressed(Key.DownArrow);
        }

        public bool Fire()
        {
            return Keyboard.IsKeyPressed(Key.Space);
        }

        public bool Escape()
        {
            return Keyboard.IsKeyPressed(Key.Escape);
        }
    }
}
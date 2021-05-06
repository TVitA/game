using OpenTK.Input;

namespace JUnity.Input
{
    /// <summary>
    /// Input manager class
    /// </summary>
    public static class InputManager
    {
        /// <summary>
        /// Current keyboard state
        /// </summary>
        public static KeyboardState KeyboardState { get; private set; }

        /// <summary>
        /// Is button was pressed on last frame
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if pressed</returns>
        public static bool IsKeyJustPressed(Key key)
        {
            if (KeyboardState.IsKeyDown(key) && !lastState.IsKeyDown(key))
            {
                return true;
            }
            return false;
        }

        internal static void Update()
        {
            lastState = KeyboardState;
            KeyboardState = Keyboard.GetState();
        }

        private static KeyboardState lastState;
    }
}

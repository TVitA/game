using System;

using OpenTK.Input;

namespace GameEngine.Input
{
    /// <summary>
    /// Input manager class.
    /// </summary>
    public static class InputManager : Object
    {
        /// <summary>
        /// Current keyboard state.
        /// </summary>
        private static KeyboardState keyboardState;

        /// <summary>
        /// Last state of keyboard.
        /// </summary>
        private static KeyboardState lastState;

        /// <summary>
        /// Returns keyboard state.
        /// </summary>
        public static KeyboardState KeyboardState => keyboardState;

        /// <summary>
        /// Is button was pressed on last frame.
        /// </summary>
        /// <param name="key">Key to check.</param>
        /// <returns>True if key is pressed.</returns>
        public static Boolean IsKeyJustPressed(Key key)
        {
            if (KeyboardState.IsKeyDown(key) && !lastState.IsKeyDown(key))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Update state of keyboard.
        /// </summary>
        internal static void Update()
        {
            lastState = keyboardState;

            keyboardState = Keyboard.GetState();
        }
    }
}

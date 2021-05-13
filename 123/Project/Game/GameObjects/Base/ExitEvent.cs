using System;

using GameEngine;
using GameEngine.Basic;
using GameEngine.Input;

namespace Game.GameObjects
{
    /// <summary>
    /// Exit event listener class.
    /// </summary>
    public class ExitEvent : GameObject
    {
        /// <summary>
        /// Fixed update.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        public override void FixedUpdate(Double deltaTime)
        {
            if (InputManager.IsKeyJustPressed(OpenTK.Input.Key.Escape))
            {
                Engine.Stop();
            }
        }
    }
}

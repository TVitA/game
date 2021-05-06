using JUnity;
using JUnity.Basic;
using JUnity.Input;

namespace Tankists.GameObjects.Base
{
    /// <summary>
    /// Exit event listener class
    /// </summary>
    public class ExitEvent : GameObject
    {
        /// <summary>
        /// Fixed update
        /// </summary>
        /// <param name="deltaTime">Time between frames</param>
        public override void FixedUpdate(double deltaTime)
        {
            if (InputManager.IsKeyJustPressed(OpenTK.Input.Key.Escape))
            {
                Engine.Stop();
            }
        }
    }
}

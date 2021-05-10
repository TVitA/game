using System;

using OpenTK;

using GameEngine.Basic;

namespace GameEngine.Utilities
{
    /// <summary>
    /// TransformationHelper class.
    /// </summary>
    public static class TransformationHelper : Object
    {
        /// <summary>
        /// Rotate game object around point.
        /// </summary>
        /// <param name="gameObject">Game object.</param>
        /// <param name="point">Point.</param>
        /// <param name="angle">Angle.</param>
        public static void RotateAroundPoint(GameObject gameObject, Vector2 point, float angle)
        {
            var position = gameObject.Position;

            position.X = (Single)((gameObject.Position.X - point.X) * Math.Cos(MathHelper.DegreesToRadians(angle))
                - (gameObject.Position.Y - point.Y) * Math.Sin(MathHelper.DegreesToRadians(angle)));

            position.Y = (Single)((gameObject.Position.X - point.X) * Math.Sin(MathHelper.DegreesToRadians(angle))
                + (gameObject.Position.Y - point.Y) * Math.Cos(MathHelper.DegreesToRadians(angle)));
            
            gameObject.Position = position + point;
            
            gameObject.Rotation = angle;
        }
    }
}

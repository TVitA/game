using JUnity.Basic;
using OpenTK;
using System;

namespace JUnity.Utilities
{
    /// <summary>
    /// Helper class
    /// </summary>
    public static class TransformationHelper
    {
        /// <summary>
        /// Rotate obj around point
        /// </summary>
        /// <param name="obj">Object</param>
        /// <param name="point">Point</param>
        /// <param name="angle">Angle</param>
        public static void RotateAroundPoint(GameObject obj, Vector2 point, float angle)
        {
            var tmp = obj.position;
            tmp.X = (float)((obj.position.X - point.X) * Math.Cos(MathHelper.DegreesToRadians(angle)) - (obj.position.Y - point.Y) * Math.Sin(MathHelper.DegreesToRadians(angle)));
            tmp.Y = (float)((obj.position.X - point.X) * Math.Sin(MathHelper.DegreesToRadians(angle)) + (obj.position.Y - point.Y) * Math.Cos(MathHelper.DegreesToRadians(angle)));
            obj.position =  tmp + point;
            obj.Rotation = angle;
        }
    }
}

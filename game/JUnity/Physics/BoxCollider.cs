using JUnity.Physics.BaseColliderClasses;
using OpenTK;

namespace JUnity.Physics
{
    /// <summary>
    /// Box collider class
    /// </summary>
    public sealed class BoxCollider : PolygonCollider
    {
        /// <summary>
        /// Box collider ctor
        /// </summary>
        /// <param name="lowerLeft">Lower left point</param>
        /// <param name="upperRight">Upper right point</param>
        public BoxCollider(Vector2 lowerLeft, Vector2 upperRight)
            : base(GetPoints(lowerLeft, upperRight))
        { }

        private static Vector2[] GetPoints(Vector2 lowerLeft, Vector2 upperRight)
        {
            return new[] { new Vector2(lowerLeft.X, upperRight.Y), upperRight, new Vector2(upperRight.X, lowerLeft.Y), lowerLeft };
        }
    }
}

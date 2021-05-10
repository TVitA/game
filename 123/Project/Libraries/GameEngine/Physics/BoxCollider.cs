using OpenTK;

using GameEngine.Physics.BaseColliderClasses;

namespace GameEngine.Physics
{
    /// <summary>
    /// Box collider class.
    /// </summary>
    public sealed class BoxCollider : PolygonCollider
    {
        /// <summary>
        /// Box collider constructor.
        /// </summary>
        /// <param name="lowerLeft">Lower left point.</param>
        /// <param name="upperRight">Upper right point.</param>
        public BoxCollider(Vector2 lowerLeft, Vector2 upperRight)
            : base(GetPoints(lowerLeft, upperRight))
        { }

        /// <summary>
        /// Forms array of points of box collider.
        /// </summary>
        /// <param name="lowerLeft">Lower left point.</param>
        /// <param name="upperRight">Upper right point.</param>
        /// <returns>Array of points of box collider.</returns>
        private static Vector2[] GetPoints(Vector2 lowerLeft, Vector2 upperRight)
        {
            return new[]
            {
                new Vector2(lowerLeft.X, upperRight.Y),
                upperRight,
                new Vector2(upperRight.X, lowerLeft.Y),
                lowerLeft,
            };
        }
    }
}

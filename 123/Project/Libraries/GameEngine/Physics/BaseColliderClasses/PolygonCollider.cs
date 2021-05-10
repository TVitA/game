using System;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace GameEngine.Physics.BaseColliderClasses
{
    /// <summary>
    /// Polygon collider base class.
    /// </summary>
    public class PolygonCollider : Collider
    {
        /// <summary>
        /// PolygonCollider points.
        /// </summary>
        private Vector2[] points;

        /// <summary>
        /// PolygonCollider constructor.
        /// </summary>
        /// <param name="points">Array of points that defined field of polygon collider.</param>
        public PolygonCollider(Vector2[] points)
        {
            this.points = points;
        }

        /// <summary>
        /// Returns array of points.
        /// </summary>
        protected Vector2[] Points => points;

        /// <summary>
        /// Rotate function.
        /// </summary>
        /// <param name="deltaAngle">Rotation.</param>
        protected override void Rotate(Single deltaAngle)
        {
            for (var i = 0; i < points.Length; ++i)
            {
                var point = points[i];
                
                point.X = (Single)(points[i].X * Math.Cos(MathHelper.DegreesToRadians(deltaAngle))
                    - points[i].Y * Math.Sin(MathHelper.DegreesToRadians(deltaAngle)));

                point.Y = (Single)(points[i].X * Math.Sin(MathHelper.DegreesToRadians(deltaAngle))
                    + points[i].Y * Math.Cos(MathHelper.DegreesToRadians(deltaAngle)));
                
                points[i] = point;
            }
        }

        /// <summary>
        /// Get normalized vector.
        /// </summary>
        /// <param name="start">Start index.</param>
        /// <returns>Normalized vector.</returns>
        private Vector2 GetNormal(Int32 start)
        {
            var end = (start + 1 == points.Length) ? 0 : start + 1;

            var dx = points[end].X - points[start].X;
            var dy = points[end].Y - points[start].Y;

            var vector = new Vector2(-dy, dx);

            vector.Normalize();

            return vector;
        }

        /// <summary>
        /// Get supported support.
        /// </summary>
        /// <param name="direction">Direction.</param>
        /// <returns>Supported vector.</returns>
        private Vector2 GetSupport(Vector2 direction)
        {
            var bestProjection = Single.MinValue;

            var bestVertex = Vector2.Zero;

            for (var i = 0; i < points.Length; ++i)
            {
                var projection = Vector2.Dot(points[i] + Rigidbody.Owner.Position, direction);

                if (projection > bestProjection)
                {
                    bestProjection = projection;

                    bestVertex = points[i] + Rigidbody.Owner.Position;
                }
            }

            return bestVertex;
        }

        /// <summary>
        /// Get axis least penetration.
        /// </summary>
        /// <param name="polygonCollider">Polygon collider.</param>
        /// <param name="resolveDirection">Resolve direction.</param>
        /// <returns>Axis least penetration.</returns>
        private Single FindAxisLeastPenetration(PolygonCollider polygonCollider, out Vector2 resolveDirection)
        {
            var bestDistance = Single.MinValue;

            var bestIndex = -1;

            for (var i = 0; i < points.Length; ++i)
            {
                var normalizedVector = GetNormal(i);

                var supportVector = polygonCollider.GetSupport(-normalizedVector);

                var dot = Vector2.Dot(normalizedVector, supportVector - points[i] - Rigidbody.Owner.Position);

                if (dot > bestDistance)
                {
                    bestDistance = dot;

                    bestIndex = i;
                }
            }

            resolveDirection = GetNormal(bestIndex);

            return bestDistance;
        }

        /// <summary>
        /// Draw collider.
        /// </summary>
        internal override void Draw()
        {
            GL.Disable(EnableCap.Blend);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            GL.Begin(PrimitiveType.LineLoop);

            if (IsTrigger)
            {
                GL.Color3(Color.DeepPink);
            }
            else
            {
                GL.Color3(Color.LightGreen);
            }

            foreach (Vector2 point in points)
            {
                GL.Vertex2(point + Rigidbody.Owner.Position);
            }

            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
        }

        /// <summary>
        /// Resolve collision method.
        /// </summary>
        /// <param name="collider">Other collider.</param>
        protected override void ResolveCollision(Collider collider)
        {
            if (collider == null)
            {
                throw new ArgumentNullException();
            }

            var polygonCollider = collider as PolygonCollider;

            if (polygonCollider == null)
            {
                throw new ApplicationException("Colliders detecting collision error.");
            }

            var one = FindAxisLeastPenetration(polygonCollider, out var dir1);

            var two = polygonCollider.FindAxisLeastPenetration(this, out var dir2);

            if (one > 0.0f || two > 0.0f)
            {
                return;
            }

            if (IsTrigger || polygonCollider.IsTrigger)
            {
                Rigidbody.TriggerNotify(polygonCollider);

                polygonCollider.Rigidbody.TriggerNotify(this);
            }
            else
            {
                if (!IsStatic)
                {
                    Rigidbody.Owner.Position += -dir2 * Math.Max(one, two);

                    var x = Rigidbody.Velocity.X;
                    var y = Rigidbody.Velocity.Y;

                    if (dir1.X != 0)
                    {
                        x = 0.0f;
                    }

                    if (dir1.Y != 0)
                    {
                        y = 0.0f;
                    }

                    Rigidbody.Velocity = new Vector2(x, y);
                }

                if (!polygonCollider.IsStatic)
                {
                    polygonCollider.Rigidbody.Owner.Position += -dir1 * Math.Max(one, two);

                    var x = polygonCollider.Rigidbody.Velocity.X;
                    var y = polygonCollider.Rigidbody.Velocity.Y;

                    if (dir2.X != 0)
                    {
                        x = 0.0f;
                    }

                    if (dir2.Y != 0)
                    {
                        y = 0.0f;
                    }

                    polygonCollider.Rigidbody.Velocity = new Vector2(x, y);
                }
            }
        }

        /// <summary>
        /// Generate axis aligned bounding box for this collider.
        /// </summary>
        /// <returns>AxisAlignedBoundingBox object.</returns>
        private protected override AxisAlignedBoundingBox GenerateAABB()
        {
            var min = new Vector2(Single.MaxValue, Single.MaxValue);
            var max = new Vector2(Single.MinValue, Single.MinValue);

            for (var i = 0; i < points.Length; ++i)
            {
                if (points[i].X < min.X)
                {
                    min.X = points[i].X;
                }

                if (points[i].Y < min.Y)
                {
                    min.Y = points[i].Y;
                }

                if (points[i].X > max.X)
                {
                    max.X = points[i].X;
                }

                if (points[i].Y > max.Y)
                {
                    max.Y = points[i].Y;
                }
            }

            return new AxisAlignedBoundingBox(this, min, max);
        }
    }
}

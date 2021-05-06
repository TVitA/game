using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Drawing;

namespace JUnity.Physics.BaseColliderClasses
{
    /// <summary>
    /// Polygon collider base class
    /// </summary>
    public class PolygonCollider : Collider
    {
        /// <summary>
        /// Collider ctor
        /// </summary>
        /// <param name="points"></param>
        public PolygonCollider(Vector2[] points)
        {
            this.points = points;
        }

        /// <summary>
        /// Collider points
        /// </summary>
        protected Vector2[] points;

        /// <summary>
        /// Rotate function
        /// </summary>
        /// <param name="angle"></param>
        protected override void Rotate(float angle)
        {
            for (int i = 0; i < points.Length; i++)
            {
                var tmp = points[i];
                tmp.X = (float)(points[i].X * Math.Cos(MathHelper.DegreesToRadians(angle)) - points[i].Y * Math.Sin(MathHelper.DegreesToRadians(angle)));
                tmp.Y = (float)(points[i].X * Math.Sin(MathHelper.DegreesToRadians(angle)) + points[i].Y * Math.Cos(MathHelper.DegreesToRadians(angle)));
                points[i] = tmp;
            }
        }

        private Vector2 GetNormal(int start)
        {
            int end = start + 1 == points.Length ? 0 : start + 1;
            var dx = points[end].X - points[start].X;
            var dy = points[end].Y - points[start].Y;

            var answ = new Vector2(-dy, dx);
            answ.Normalize();
            return answ;
        }

        private Vector2 GetSupport(Vector2 direction)
        {
            float bestProjection = float.MinValue;
            Vector2 bestVertex = Vector2.Zero;

            for (int i = 0; i < points.Length; i++)
            {
                var projection = Vector2.Dot(points[i] + Rigidbody.owner.position, direction);
                if (projection > bestProjection)
                {
                    bestProjection = projection;
                    bestVertex = points[i] + Rigidbody.owner.position;
                }
            }

            return bestVertex;
        }

        private float FindAxisLeastPenetration(PolygonCollider b, out Vector2 resolveDirection)
        {
            float bestDistance = float.MinValue;
            int bestIndex = -1;

            for (int i = 0; i < points.Length; i++)
            {
                Vector2 n = GetNormal(i);
                Vector2 support = b.GetSupport(-n);
                float d = Vector2.Dot(n, support - points[i] - Rigidbody.owner.position);

                if (d > bestDistance)
                {
                    bestDistance = d;
                    bestIndex = i;
                }
            }

            resolveDirection = GetNormal(bestIndex);
            return bestDistance;
        }

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

            foreach (var point in points)
            {
                GL.Vertex2(point + Rigidbody.owner.position);
            }

            GL.End();
            GL.PopMatrix();
            GL.Enable(EnableCap.Blend);
        }

        /// <summary>
        /// Resolve collision method
        /// </summary>
        protected override void ResolveCollision(Collider other)
        {
            switch (other)
            {
                case PolygonCollider polygonCollider:
                    {
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
                                Rigidbody.owner.position += -dir2 * Math.Max(one, two);
                                if (dir1.X != 0)
                                {
                                    Rigidbody.velocity.X = 0.0f;
                                }
                                if (dir1.Y != 0)
                                {
                                    Rigidbody.velocity.Y = 0.0f;
                                }
                            }

                            if (!polygonCollider.IsStatic)
                            {
                                polygonCollider.Rigidbody.owner.position += -dir1 * Math.Max(one, two);
                                if (dir2.X != 0)
                                {
                                    polygonCollider.Rigidbody.velocity.X = 0.0f;
                                }
                                if (dir2.Y != 0)
                                {
                                    polygonCollider.Rigidbody.velocity.Y = 0.0f;
                                }
                            }
                        }
                    }
                    break;
                default:
                    throw new ApplicationException("Colliders detecting collision error");
            }
        }

        private protected override AxisAlignedBoundingBox GenerateAABB()
        {
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);
            for (int i = 0; i < points.Length; i++)
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

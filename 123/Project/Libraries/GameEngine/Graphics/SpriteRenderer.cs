using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using GameEngine.Basic;
using GameEngine.Utilities;

namespace GameEngine.Graphics
{
    /// <summary>
    /// Sprite renderer component
    /// </summary>
    public sealed class SpriteRenderer : GameComponent, IUniqueComponent
    {
        /// <summary>
        /// Render order list.
        /// </summary>
        private static readonly List<RenderOrder> renderOrders = new List<RenderOrder>();

        /// <summary>
        /// Is disposed.
        /// </summary>
        private System.Boolean isDisposed;

        /// <summary>
        /// Sprite collection.
        /// </summary>
        private SpriteCollection sprites;

        static SpriteRenderer()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        internal SpriteRenderer(GameObject owner)
            : base(owner)
        {
            isDisposed = false;

            sprites = new SpriteCollection();
        }

        /// <summary>
        /// Returns render order list.
        /// </summary>
        internal static List<RenderOrder> RenderOrders => renderOrders;

        /// <summary>
        /// Returns sprite collection.
        /// </summary>
        public SpriteCollection Sprites => sprites;

        /// <summary>
        /// Call every frame.
        /// </summary>
        /// <param name="deltaTime">Time between frames.</param>
        internal override void CallComponent(Double deltaTime)
        {
            foreach (Sprite sprite in Sprites)
            {
                renderOrders.Add(new RenderOrder(sprite, Owner));
            }
        }

        /// <summary>
        /// Render scene.
        /// </summary>
        internal static void RenderScene()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            renderOrders.Sort();

            for (var i = 0; i < renderOrders.Count; ++i)
            {
                RenderSprite(renderOrders[i]);
            }

            renderOrders.Clear();
        }


        /// <summary>
        /// Render sprite.
        /// </summary>
        /// <param name="renderOrder">Render order.</param>
        private static void RenderSprite(RenderOrder renderOrder)
        {
            var points = new Vector2[4]
            {
                new Vector2(0.0f, 1.0f),
                new Vector2(1.0f, 1.0f),
                new Vector2(1.0f, 0.0f),
                new Vector2(0.0f, 0.0f)
            };

            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();

            var x = renderOrder.owner.Position.X + (renderOrder.sprite.RotationPoint.X + renderOrder.sprite.Offset.X) * renderOrder.sprite.Scale.X;
            var y = renderOrder.owner.Position.Y + (renderOrder.sprite.RotationPoint.Y + renderOrder.sprite.Offset.Y) * renderOrder.sprite.Scale.Y;

            GL.Translate(x, y, 0.0);
            GL.Rotate(renderOrder.sprite.Rotation + renderOrder.owner.Rotation, 0.0f, 0.0f, 1.0f);
            GL.Translate(-x, -y, 0.0);

            x = renderOrder.owner.Position.X + renderOrder.sprite.Offset.X * renderOrder.sprite.Scale.X;
            y = renderOrder.owner.Position.Y + renderOrder.sprite.Offset.Y * renderOrder.sprite.Scale.Y;

            GL.Translate(x, y, 0.0);
            GL.Rotate(renderOrder.sprite.TextureRotation, 0.0f, 0.0f, 1.0f);
            GL.Translate(-x, -y, 0.0);

            GL.BindTexture(TextureTarget.Texture2D, renderOrder.sprite.Texture.Id);
            GL.Begin(PrimitiveType.Quads);
            GL.Color3(renderOrder.sprite.Color);

            for (int i = 0; i < 4; i++)
            {
                if (renderOrder.sprite.DrawingRectangle == null)
                {
                    GL.TexCoord2(points[i]);
                }
                else
                {
                    GL.TexCoord2(
                        (renderOrder.sprite.DrawingRectangle.Value.X + points[i].X * renderOrder.sprite.DrawingRectangle.Value.Width)
                        / renderOrder.sprite.Texture.Width,
                        (renderOrder.sprite.DrawingRectangle.Value.Y + points[i].Y * renderOrder.sprite.DrawingRectangle.Value.Height)
                        / renderOrder.sprite.Texture.Height);
                }

                if (renderOrder.sprite.FlipX)
                {
                    points[i].X = points[i].X == 1.0f ? 0.0f : 1.0f;
                }

                if (renderOrder.sprite.FlipY)
                {
                    points[i].Y = points[i].Y == 1.0f ? 0.0f : 1.0f;
                }

                points[i].X = renderOrder.sprite.Width * (points[i].X - 0.5f);
                points[i].Y = renderOrder.sprite.Height * (points[i].Y - 0.5f);

                points[i] += renderOrder.sprite.Offset;
                points[i] *= renderOrder.sprite.Scale;
                points[i] += renderOrder.owner.Position;

                GL.Vertex2(points[i]);
            }

            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        /// <param name="disposing">Is disposing.</param>
        private protected override void Dispose(System.Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Sprites.Dispose();
                }

                isDisposed = true;
            }
        }
    }
}

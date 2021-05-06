using JUnity.Basic;
using JUnity.Utilities;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;

namespace JUnity.Graphics
{
    /// <summary>
    /// Sprite renderer component
    /// </summary>
    public sealed class SpriteRenderer : GameComponent, IUniqueComponent
    {
        static SpriteRenderer()
        {
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
        }

        internal SpriteRenderer(GameObject @object)
            : base(@object)
        {
            Sprites = new SpriteCollection();
        }

        /// <summary>
        /// Sprites list
        /// </summary>
        public SpriteCollection Sprites { get; set; }

        internal override void CallComponent(double deltaTime)
        {
            foreach (var sprite in Sprites)
            {
                renderOrders.Add(new RenderOrder(sprite, owner));
            }
        }

        internal static void RenderScene()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            renderOrders.Sort();
            for (int i = 0; i < renderOrders.Count; i++)
            {
                RenderSprite(renderOrders[i]);
            }

            renderOrders.Clear();
        }

        internal static readonly List<RenderOrder> renderOrders = new List<RenderOrder>();

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

            var X = renderOrder.owner.position.X + (renderOrder.sprite.rotationPoint.X + renderOrder.sprite.offset.X) * renderOrder.sprite.scale.X;
            var Y = renderOrder.owner.position.Y + (renderOrder.sprite.rotationPoint.Y + renderOrder.sprite.offset.Y) * renderOrder.sprite.scale.Y;

            GL.Translate(X, Y, 0.0);
            GL.Rotate(renderOrder.sprite.Rotation + renderOrder.owner.Rotation, 0.0f, 0.0f, 1.0f);
            GL.Translate(-X, -Y, 0.0);

            X = renderOrder.owner.position.X + renderOrder.sprite.offset.X * renderOrder.sprite.scale.X;
            Y = renderOrder.owner.position.Y + renderOrder.sprite.offset.Y * renderOrder.sprite.scale.Y;

            GL.Translate(X, Y, 0.0);
            GL.Rotate(renderOrder.sprite.TextureRotation, 0.0f, 0.0f, 1.0f);
            GL.Translate(-X, -Y, 0.0);

            GL.BindTexture(TextureTarget.Texture2D, renderOrder.sprite.Texture2D.Id);
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
                        (renderOrder.sprite.DrawingRectangle.Value.X + points[i].X * renderOrder.sprite.DrawingRectangle.Value.Width) / renderOrder.sprite.Texture2D.Width,
                        (renderOrder.sprite.DrawingRectangle.Value.Y + points[i].Y * renderOrder.sprite.DrawingRectangle.Value.Height) / renderOrder.sprite.Texture2D.Height);
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

                points[i] += renderOrder.sprite.offset;
                points[i] *= renderOrder.sprite.scale;
                points[i] += renderOrder.owner.position;
                GL.Vertex2(points[i]);
            }

            GL.End();
            GL.PopMatrix();
        }

        bool isDisposed = false;

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
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

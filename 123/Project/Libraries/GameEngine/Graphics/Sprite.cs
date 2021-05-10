using System;
using System.Drawing;

using OpenTK;

namespace GameEngine.Graphics
{
    /// <summary>
    /// Sprite container class
    /// </summary>
    public class Sprite : IDisposable
    {
        /// <summary>
        /// Is disposed sprite.
        /// </summary>
        private Boolean isDisposed;

        /// <summary>
        /// Sprite z order.
        /// </summary>
        private Single zOrder;

        /// <summary>
        /// Sprite rotation.
        /// </summary>
        private Single rotation;

        /// <summary>
        /// Texture rotation.
        /// </summary>
        private Single textureRotation;

        /// <summary>
        /// Texture color.
        /// </summary>
        private Color color;

        /// <summary>
        /// Is sprite should be flipped by x axis.
        /// </summary>
        private Boolean flipX;
        /// <summary>
        /// Is sprite should be flipped by y axis.
        /// </summary>
        private Boolean flipY;

        /// <summary>
        /// Sprite offset.
        /// </summary>
        private Vector2 offset;
        /// <summary>
        /// Sprite scale.
        /// </summary>
        private Vector2 scale;
        /// <summary>
        /// Sprite rotation point.
        /// </summary>
        private Vector2 rotationPoint;

        /// <summary>
        /// Width of sprite.
        /// </summary>
        private Int32 width;
        /// <summary>
        /// Height of sprite.
        /// </summary>
        private Int32 height;

        /// <summary>
        /// Texture.
        /// </summary>
        private Texture2D texture;

        /// <summary>
        /// Image source rect.
        /// </summary>
        private Rectangle? drawingRectangle;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="texture">Texture.</param>
        internal Sprite(Texture2D texture)
        {
            isDisposed = false;

            this.texture = texture;

            color = Color.White;
            scale = Vector2.One;
            Width = texture.Width;
            Height = texture.Height;
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="texture">Texture.</param>
        /// <param name="rect">Rect.</param>
        internal Sprite(Texture2D texture, Rectangle rect)
           : this(texture)
        {
            drawingRectangle = rect;

            Width = rect.Width;
            Height = rect.Height;
        }

        /// <summary>
        /// Creates new sprite.
        /// </summary>
        /// <param name="filename">Image filename.</param>
        public Sprite(String filename)
            : this(Utilities.ResourceLoader.LoadTexture2D(filename))
        { }

        /// <summary>
        /// Creates new sprite.
        /// </summary>
        /// <param name="filename">Image filename.</param>
        /// <param name="rect">Source rect.</param>
        public Sprite(String filename, Rectangle rect)
            : this(Utilities.ResourceLoader.LoadTexture2D(filename), rect)
        { }

        /// <summary>
        /// Returns zOrder.
        /// </summary>
        public Single ZOrder
        {
            get => zOrder;

            set => zOrder = value;
        }

        /// <summary>
        /// Returns rotation.
        /// </summary>
        public Single Rotation
        {
            get => rotation;

            set => rotation = value;
        }

        /// <summary>
        /// Returns textureRotation.
        /// </summary>
        public Single TextureRotation
        {
            get => textureRotation;

            set => textureRotation = value;
        }

        /// <summary>
        /// Returns color.
        /// </summary>
        public Color Color
        {
            get => color;

            set => color = value;
        }

        /// <summary>
        /// Returns flipX.
        /// </summary>
        public Boolean FlipX
        {
            get => flipX;

            set => flipX = value;
        }

        /// <summary>
        /// Returns flipY.
        /// </summary>
        public Boolean FlipY
        {
            get => flipY;

            set => flipY = value;
        }

        /// <summary>
        /// Returns offset.
        /// </summary>
        public Vector2 Offset
        {
            get => offset;

            set => offset = value;
        }

        /// <summary>
        /// Returns scale.
        /// </summary>
        public Vector2 Scale
        {
            get => scale;

            set => scale = value;
        }

        /// <summary>
        /// Returns rotationPoint.
        /// </summary>
        public Vector2 RotationPoint
        {
            get => rotationPoint;

            set => rotationPoint = value;
        }

        /// <summary>
        /// Returns texture.
        /// </summary>
        internal Texture2D Texture => texture;

        /// <summary>
        /// Image source rect.
        /// </summary>
        public Rectangle? DrawingRectangle => drawingRectangle;

        /// <summary>
        /// Sprite width.
        /// </summary>
        public Int32 Width
        {
            get => width;

            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException("Width", "Width cannot be less then 0");
                }

                width = value;
            }
        }

        /// <summary>
        /// Sprite height.
        /// </summary>
        public Int32 Height
        {
            get => height;

            set
            {
                if (value <= 0.0)
                {
                    throw new ArgumentOutOfRangeException("Width", "Width cannot be less then 0");
                }

                height = value;
            }
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// IDisposable implementation.
        /// </summary>
        public void Dispose(Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    texture.Dispose();
                }

                isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor.
        /// </summary>
        ~Sprite()
        {
            Dispose(false);
        }
    }
}

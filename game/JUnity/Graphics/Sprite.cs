using OpenTK;
using System;
using System.Drawing;

namespace JUnity.Graphics
{
    /// <summary>
    /// Sprite container class
    /// </summary>
    public class Sprite : IDisposable
    {
        /// <summary>
        /// Creates new sprite
        /// </summary>
        /// <param name="filename">Image filename</param>
        public Sprite(string filename)
            : this(Utilities.ResourceLoader.LoadTexture2D(filename))
        { }

        /// <summary>
        /// Creates new sprite
        /// </summary>
        /// <param name="filename">Image filename</param>
        /// <param name="rect">Source rect</param>
        public Sprite(string filename, Rectangle rect)
            : this(Utilities.ResourceLoader.LoadTexture2D(filename), rect)
        { }

        internal Sprite(Texture2D texture)
        {
            Texture2D = texture;
            Color = Color.White;
            scale = Vector2.One;
            Width = texture.Width;
            Height = texture.Height;
        }

        internal Sprite(Texture2D texture, Rectangle rect)
            : this(texture)
        {
            DrawingRectangle = rect;
            Width = rect.Width;
            Height = rect.Height;
        }

        internal Texture2D Texture2D { get; set; }

        /// <summary>
        /// Image source rect
        /// </summary>
        public Rectangle? DrawingRectangle { get; set; }

        /// <summary>
        /// Sprite width
        /// </summary>
        public int Width
        {
            get => width;
            set
            {
                if (value >= 0.0)
                {
                    width = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Width", "Width cannot be less then 0");
                }
            }
        }

        /// <summary>
        /// Sprite height
        /// </summary>
        public int Height
        {
            get => height;
            set
            {
                if (value >= 0.0)
                {
                    height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Height", "Height cannot be less then 0");
                }
            }
        }

        /// <summary>
        /// Sprite z order
        /// </summary>
        public float ZOrder { get; set; }

        /// <summary>
        /// Sprite rotation
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Texture rotation
        /// </summary>
        public float TextureRotation { get; set; }

        /// <summary>
        /// Texture color
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Is sprite should be flipped by x axe
        /// </summary>
        public bool FlipX { get; set; }

        /// <summary>
        /// Is sprite should be flipped by y axe
        /// </summary>
        public bool FlipY { get; set; }

        /// <summary>
        /// Sprite offset
        /// </summary>
        public Vector2 offset;

        /// <summary>
        /// Sprite scale
        /// </summary>
        public Vector2 scale;

        /// <summary>
        /// Sprite rotation point
        /// </summary>
        public Vector2 rotationPoint;

        private bool isDisposed = false;
        private int width;
        private int height;

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// IDisposable implementation
        /// </summary>
        public void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    Texture2D.Dispose();
                }

                isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Sprite()
        {
            Dispose(false);
        }
    }
}

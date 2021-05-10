using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

using OpenTK;
using OpenTK.Graphics.OpenGL;

using GameEngine.Graphics;

namespace GameEngine.Utilities
{
    /// <summary>
    /// Sprite collection
    /// </summary>
    public sealed class SpriteCollection : Object, ICollection<Sprite>, IDisposable
    {
        /// <summary>
        /// On sprite added event
        /// </summary>
        public event EventHandler<SpriteEventArgs> OnSpriteAdded;

        /// <summary>
        /// On sprite removed event
        /// </summary>
        public event EventHandler<SpriteEventArgs> OnSpriteRemoved;

        /// <summary>
        /// On collection changed event
        /// </summary>
        public event EventHandler OnCollectionChanged;

        /// <summary>
        /// List of sprites.
        /// </summary>
        private readonly List<Sprite> sprites = new List<Sprite>();

        /// <summary>
        /// Is disposed.
        /// </summary>
        private System.Boolean isDisposed;

        /// <summary>
        /// Count of sprites in collection
        /// </summary>
        public Int32 Count => sprites.Count;

        /// <summary>
        /// Is collection readonly
        /// </summary>
        public System.Boolean IsReadOnly => false;

        /// <summary>
        /// Sprite collection ctor.
        /// </summary>
        public SpriteCollection()
            : base()
        {
            isDisposed = false;
        }

        /// <summary>
        /// Add sprite by filename.
        /// </summary>
        /// <param name="name">Image filename.</param>
        /// <param name="drawingRect">Source rect.</param>
        /// <returns></returns>
        public Sprite AddByName(String name, Rectangle? drawingRect = null)
        {
            Sprite sprite = null;

            if (drawingRect != null)
            {
                sprite = new Sprite(ResourceLoader.LoadTexture2D(name), drawingRect.Value);
            }
            else
            {
                sprite = new Sprite(ResourceLoader.LoadTexture2D(name));
            }

            Add(sprite);

            return sprite;
        }

        /// <summary>
        /// Add sprite by filename.
        /// </summary>
        /// <param name="name">Image filename.</param>
        /// <param name="textureMinFilter">Texture filtering.</param>
        /// <param name="textureMagFilter">Texture filtering.</param>
        /// <param name="drawingRect">Source rect.</param>
        /// <returns>Added sprite.</returns>
        public Sprite AddByName(String name, TextureMinFilter textureMinFilter, TextureMagFilter textureMagFilter, Rectangle? drawingRect = null)
        {
            Sprite sprite = null;

            if (drawingRect != null)
            {
                sprite = new Sprite(ResourceLoader.LoadTexture2D(name, textureMinFilter, textureMagFilter), drawingRect.Value);
            }
            else
            {
                sprite = new Sprite(ResourceLoader.LoadTexture2D(name, textureMinFilter, textureMagFilter));
            }

            Add(sprite);

            return sprite;
        }

        /// <summary>
        /// Try to get image by filename
        /// </summary>
        /// <param name="name">Image filename</param>
        /// <param name="sprite">Sprite</param>
        /// <returns>True if found</returns>
        public System.Boolean TryGetByName(String name, out Sprite sprite)
        {
            var findedSprite = sprites.Find(spr => spr.Texture.Name == name);

            if (findedSprite != null)
            {
                sprite = findedSprite;

                return true;
            }

            sprite = default;

            return false;
        }

        /// <summary>
        /// Collection indexator
        /// </summary>
        /// <param name="index">Sprite index</param>
        /// <returns>Sprite</returns>
        public Sprite this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                return sprites[index];
            }

            set
            {
                if (index < 0 || index >= Count)
                {
                    throw new ArgumentOutOfRangeException();
                }

                sprites[index] = value;
            }
        }

        /// <summary>
        /// Collection indexator
        /// </summary>
        /// <param name="name">Sprite name</param>
        /// <returns>Sprite</returns>
        public Sprite this[String name]
        {
            get => sprites.Find(x => x.Texture.Name == name);

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }

                var pos = sprites.FindIndex(spr => spr.Texture.Name == name);

                if (pos == -1)
                {
                    throw new ArgumentOutOfRangeException();
                }

                sprites[pos] = value;
            }
        }

        /// <summary>
        /// Remove sprite by name.
        /// </summary>
        /// <param name="name">Sprite name.</param>
        /// <returns>True if removed.</returns>
        public System.Boolean RemoveByName(String name)
        {
            var sprite = sprites.Find(spr => spr.Texture.Name == name);

            if (sprite != null)
            {
                return Remove(sprite);
            }

            return false;
        }

        /// <summary>
        /// Set size to all sprites
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public void SetSizeToAll(Int32 width, Int32 height)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Width = width;

                sprite.Height = height;
            }
        }

        /// <summary>
        /// Set z order to all sprites
        /// </summary>
        /// <param name="zOrder">Z order</param>
        public void SetZOrderToAll(Single zOrder)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.ZOrder = zOrder;
            }
        }

        /// <summary>
        /// Set rotation to all
        /// </summary>
        /// <param name="rotation">Rotation</param>
        public void SetRotationToAll(Single rotation)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Rotation = rotation;
            }
        }

        /// <summary>
        /// Set texture rotation to all
        /// </summary>
        /// <param name="rotation">Rotation</param>
        public void SetTextureRotationToAll(Single rotation)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.TextureRotation = rotation;
            }
        }

        /// <summary>
        /// Set color to all
        /// </summary>
        /// <param name="color">Color to set</param>
        public void SetColorToAll(Color color)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Color = color;
            }
        }

        /// <summary>
        /// Set flip to all
        /// </summary>
        /// <param name="flipX">Flip by x</param>
        /// <param name="flipY">Flip by y</param>
        public void SetFlipToAll(System.Boolean flipX, System.Boolean flipY)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.FlipX = flipX;
                sprite.FlipY = flipY;
            }
        }

        /// <summary>
        /// Set offset to all
        /// </summary>
        /// <param name="offsetX">X offset</param>
        /// <param name="offsetY">Y offset</param>
        public void SetOffsetToAll(Single offsetX, Single offsetY)
        {
            SetOffsetToAll(new Vector2(offsetX, offsetY));
        }

        /// <summary>
        /// Set offset to all
        /// </summary>
        /// <param name="offset">Offset</param>
        public void SetOffsetToAll(Vector2 offset)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.Offset = offset;
            }
        }

        /// <summary>
        /// Set scale to all
        /// </summary>
        /// <param name="scaleX">X scale</param>
        /// <param name="scaleY">Y scale</param>
        public void SetScaleToAll(Single scaleX, Single scaleY)
        {
            var scale = new Vector2(scaleX, scaleY);

            foreach (Sprite sprite in sprites)
            {
                sprite.Scale = scale;
            }
        }

        /// <summary>
        /// Set scale to all
        /// </summary>
        /// <param name="scale">Scale</param>
        public void SetScaleToAll(Single scale)
        {
            SetScaleToAll(scale, scale);
        }

        /// <summary>
        /// Set rotation point to all
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void SetRotationPointToAll(Single x, Single y)
        {
            SetRotationPointToAll(new Vector2(x, y));
        }

        /// <summary>
        /// Set rotation point to all
        /// </summary>
        /// <param name="rotation">Rotation point</param>
        public void SetRotationPointToAll(Vector2 rotation)
        {
            foreach (Sprite sprite in sprites)
            {
                sprite.RotationPoint = rotation;
            }
        }

        /// <summary>
        /// Add sprite
        /// </summary>
        /// <param name="sprite">Sprite to add</param>
        public void Add(Sprite sprite)
        {
            sprites.Add(sprite);

            OnSpriteAdded?.Invoke(this, new SpriteEventArgs(sprite));
            OnCollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Add range to collection
        /// </summary>
        /// <param name="spritesRange">Range to add</param>
        public void AddRange(IEnumerable<Sprite> spritesRange)
        {
            sprites.AddRange(spritesRange);

            OnCollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Clear collection
        /// </summary>
        public void Clear()
        {
            sprites.Clear();

            OnCollectionChanged?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Is collection contains item
        /// </summary>
        /// <param name="sprite">Item to check</param>
        /// <returns>True if contains</returns>
        public System.Boolean Contains(Sprite sprite)
        {
            return sprites.Contains(sprite);
        }

        /// <summary>
        /// Copy sprites to array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="arrayIndex">Index to start copy</param>
        public void CopyTo(Sprite[] array, Int32 arrayIndex)
        {
            sprites.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Returns enumerator
        /// </summary>
        /// <returns>Enumerator</returns>
        public IEnumerator<Sprite> GetEnumerator()
        {
            return sprites.GetEnumerator();
        }

        /// <summary>
        /// Remove sprite
        /// </summary>
        /// <param name="sprite">Sprite to remove</param>
        /// <returns></returns>
        public System.Boolean Remove(Sprite sprite)
        {
            if (sprites.Remove(sprite))
            {
                OnSpriteRemoved?.Invoke(this, new SpriteEventArgs(sprite));
                OnCollectionChanged?.Invoke(this, EventArgs.Empty);

                return true;
            }

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sprites.GetEnumerator();
        }
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
        public void Dispose(System.Boolean disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    foreach (Sprite sprite in sprites)
                    {
                        sprite.Dispose();
                    }
                }

                isDisposed = true;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~SpriteCollection()
        {
            Dispose(false);
        }
    }
}

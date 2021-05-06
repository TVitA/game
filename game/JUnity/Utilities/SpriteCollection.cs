using JUnity.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace JUnity.Utilities
{
    /// <summary>
    /// Sprite collection
    /// </summary>
    public sealed class SpriteCollection : ICollection<Sprite>, IDisposable
    {
        private readonly List<Sprite> sprites = new List<Sprite>();

        /// <summary>
        /// Count of sprites in collection
        /// </summary>
        public int Count => sprites.Count;

        /// <summary>
        /// Is collection readonly
        /// </summary>
        public bool IsReadOnly => false;

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
        /// Add sprite by filename
        /// </summary>
        /// <param name="name">Image filename</param>
        /// <param name="drawingRect">Source rect</param>
        /// <returns></returns>
        public Sprite AddByName(string name, Rectangle? drawingRect = null)
        {
            Sprite tmp = null;
            if (drawingRect != null)
            {
                tmp = new Sprite(ResourceLoader.LoadTexture2D(name), drawingRect.Value);
            }
            else
            {
                tmp = new Sprite(ResourceLoader.LoadTexture2D(name));
            }
            
            Add(tmp);
            return tmp;
        }

        /// <summary>
        /// Add sprite by filename
        /// </summary>
        /// <param name="name">Image filename</param>
        /// <param name="textureMinFilter">Texture filtering</param>
        /// <param name="textureMagFilter">Texture filtering</param>
        /// <param name="drawingRect">Source rect</param>
        /// <returns>Added sprite</returns>
        public Sprite AddByName(string name, TextureMinFilter textureMinFilter, TextureMagFilter textureMagFilter, Rectangle? drawingRect = null)
        {
            Sprite tmp = null;
            if (drawingRect != null)
            {
                tmp = new Sprite(ResourceLoader.LoadTexture2D(name, textureMinFilter, textureMagFilter), drawingRect.Value);
            }
            else
            {
                tmp = new Sprite(ResourceLoader.LoadTexture2D(name, textureMinFilter, textureMagFilter));
            }

            Add(tmp);
            return tmp;
        }

        /// <summary>
        /// Try to get image by filename
        /// </summary>
        /// <param name="name">Image filename</param>
        /// <param name="sprite">Sprite</param>
        /// <returns>True if found</returns>
        public bool TryGetByName(string name, out Sprite sprite)
        {
            var tmp = sprites.Find(x => x.Texture2D.Name == name);
            if (tmp != null)
            {
                sprite = tmp;
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
        public Sprite this[int index]
        {
            get => sprites[index];
            set => sprites[index] = value;
        }

        /// <summary>
        /// Collection indexator
        /// </summary>
        /// <param name="name">Sprite name</param>
        /// <returns>Sprite</returns>
        public Sprite this[string name]
        {
            get => sprites.Find(x => x.Texture2D.Name == name);
            set
            {
                var pos = sprites.FindIndex(x => x.Texture2D.Name == name);
                sprites[pos] = value;
            }
        }

        /// <summary>
        /// Remove sprite by name
        /// </summary>
        /// <param name="name">Sprite name</param>
        /// <returns>True if removed</returns>
        public bool RemoveByName(string name)
        {
            var tmp = sprites.Find(x => x.Texture2D.Name == name);
            if (tmp != null)
            {
                return Remove(tmp);
            }
            return false;
        }

        /// <summary>
        /// Set size to all sprites
        /// </summary>
        /// <param name="width">Width</param>
        /// <param name="height">Height</param>
        public void SetSizeToAll(int width, int height)
        {
            foreach (var sprite in sprites)
            {
                sprite.Width = width;
                sprite.Height = height;
            }
        }

        /// <summary>
        /// Set z order to all sprites
        /// </summary>
        /// <param name="zOrder">Z order</param>
        public void SetZOrderToAll(float zOrder)
        {
            foreach (var sprite in sprites)
            {
                sprite.ZOrder = zOrder;
            }
        }

        /// <summary>
        /// Set rotation to all
        /// </summary>
        /// <param name="rotation">Rotation</param>
        public void SetRotationToAll(float rotation)
        {
            foreach (var sprite in sprites)
            {
                sprite.Rotation = rotation;
            }
        }

        /// <summary>
        /// Set texture rotation to all
        /// </summary>
        /// <param name="rotation">Rotation</param>
        public void SetTextureRotationToAll(float rotation)
        {
            foreach (var sprite in sprites)
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
            foreach (var sprite in sprites)
            {
                sprite.Color = color;
            }
        }

        /// <summary>
        /// Set flip to all
        /// </summary>
        /// <param name="flipX">Flip by x</param>
        /// <param name="flipY">Flip by y</param>
        public void SetFlipToAll(bool flipX, bool flipY)
        {
            foreach (var sprite in sprites)
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
        public void SetOffsetToAll(float offsetX, float offsetY)
        {
            foreach (var sprite in sprites)
            {
                sprite.offset.X = offsetX;
                sprite.offset.Y = offsetY;
            }
        }

        /// <summary>
        /// Set offset to all
        /// </summary>
        /// <param name="offset">Offset</param>
        public void SetOffsetToAll(Vector2 offset)
        {
            foreach (var sprite in sprites)
            {
                sprite.offset = offset;
            }
        }

        /// <summary>
        /// Set scale to all
        /// </summary>
        /// <param name="scaleX">X scale</param>
        /// <param name="scaleY">Y scale</param>
        public void SetScaleToAll(float scaleX, float scaleY)
        {
            foreach (var sprite in sprites)
            {
                sprite.scale.X = scaleX;
                sprite.scale.Y = scaleY;
            }
        }

        /// <summary>
        /// Set scale to all
        /// </summary>
        /// <param name="scale">Scale</param>
        public void SetScaleToAll(float scale)
        {
            SetScaleToAll(scale, scale);
        }

        /// <summary>
        /// Set rotation point to all
        /// </summary>
        /// <param name="x">X</param>
        /// <param name="y">Y</param>
        public void SetRotationPointToAll(float x, float y)
        {
            foreach (var sprite in sprites)
            {
                sprite.rotationPoint.X = x;
                sprite.rotationPoint.Y = y;
            }
        }

        /// <summary>
        /// Set rotation point to all
        /// </summary>
        /// <param name="rotation">Rotation point</param>
        public void SetRotationPointToAll(Vector2 rotation)
        {
            foreach (var sprite in sprites)
            {
                sprite.rotationPoint = rotation;
            }
        }

        /// <summary>
        /// Add sprite
        /// </summary>
        /// <param name="item">Sprite to add</param>
        public void Add(Sprite item)
        {
            sprites.Add(item);
            OnSpriteAdded?.Invoke(this, new SpriteEventArgs(item));
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
        /// <param name="item">Item to check</param>
        /// <returns>True if contains</returns>
        public bool Contains(Sprite item)
        {
            return sprites.Contains(item);
        }

        /// <summary>
        /// Copy sprites to array
        /// </summary>
        /// <param name="array">Array</param>
        /// <param name="arrayIndex">Index to start copy</param>
        public void CopyTo(Sprite[] array, int arrayIndex)
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
        /// <param name="item">Sprite to remove</param>
        /// <returns></returns>
        public bool Remove(Sprite item)
        {
            if (sprites.Remove(item))
            {
                OnSpriteRemoved?.Invoke(this, new SpriteEventArgs(item));
                OnCollectionChanged?.Invoke(this, EventArgs.Empty);
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return sprites.GetEnumerator();
        }

        private bool isDisposed = false;

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
                    foreach (var sprite in sprites)
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

    /// <summary>
    /// Contains info aboud added/removed sprite
    /// </summary>
    public sealed class SpriteEventArgs : EventArgs
    {
        /// <summary>
        /// Creates new event args
        /// </summary>
        /// <param name="sprite">Added or removed sprite</param>
        public SpriteEventArgs(Sprite sprite)
        {
            Sprite = sprite;
        }

        /// <summary>
        /// Added / removed sprite
        /// </summary>
        public Sprite Sprite { get; }
    }
}

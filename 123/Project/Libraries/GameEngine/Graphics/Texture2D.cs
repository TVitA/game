using System;
using System.Collections.Generic;

using OpenTK.Graphics.OpenGL;

namespace GameEngine.Graphics
{
    /// <summary>
    /// Texture 2D class.
    /// </summary>
    internal class Texture2D : Object, IDisposable
    {
        /// <summary>
        /// Static list of all textures.
        /// </summary>
        private static Dictionary<String, Texture2D> textures = new Dictionary<String, Texture2D>();

        /// <summary>
        /// Checker of using dispose method.
        /// </summary>
        private System.Boolean isDisposed;

        /// <summary>
        /// Counter of sprites that use this texture.
        /// </summary>
        private Int32 usage;

        /// <summary>
        /// ID of texture in OpenGL.
        /// </summary>
        private Int32 id;
        /// <summary>
        /// Width of texture.
        /// </summary>
        private Int32 width;
        /// <summary>
        /// Height of width.
        /// </summary>
        private Int32 height;
        /// <summary>
        /// Name of texture.
        /// </summary>
        private String name;

        /// <summary>
        /// Texture 2D constructor.
        /// </summary>
        /// <param name="id">ID of texture in OpenGL.</param>
        /// <param name="width">Width of texture.</param>
        /// <param name="height">Height of width.</param>
        /// <param name="name">Name of texture.</param>
        internal Texture2D(Int32 id, Int32 width, Int32 height, String name)
            : base()
        {
            if (name == null)
            {
                throw new ArgumentNullException();
            }

            isDisposed = false;

            this.id = id;
            this.width = width;
            this.height = height;
            this.name = name;

            usage = 1;

            try
            {
                textures.Add(name, this);
            }
            catch (ArgumentException)
            { }
        }

        /// <summary>
        /// Returns ID of texture.
        /// </summary>
        public Int32 Id => id;

        /// <summary>
        /// Returns width of texture.
        /// </summary>
        public Int32 Width => width;

        /// <summary>
        /// Returns height of texture.
        /// </summary>
        public Int32 Height => height;

        /// <summary>
        /// Returns name of texture.
        /// </summary>
        public String Name => name;

        /// <summary>
        /// Finding texture in texture list.
        /// </summary>
        /// <param name="filename">Name of texture.</param>
        /// <returns>Texture from texture list or null if it not found.</returns>
        internal static Texture2D TryGetByName(String filename)
        {
            if (filename == null)
            {
                throw new ArgumentNullException();
            }

            Texture2D texture = null;

            if (textures.TryGetValue(filename, out texture))
            {
                texture.usage++;
            }

            return texture;
        }

        /// <summary>
        /// Clear list of textures.
        /// </summary>
        internal static void Reset()
        {
            textures.Clear();
        }

        /// <summary>
        /// Releasing resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Releasing resources.
        /// </summary>
        /// <param name="isDisposing">Releasing managed resources.</param>
        protected void Dispose(System.Boolean isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    if (--usage == 0)
                    {
                        GL.DeleteTexture(id);

                        textures.Remove(name);

                        isDisposed = true;
                    }
                }
            }
        }
        
        /// <summary>
        /// Destructer.
        /// </summary>
        ~Texture2D()
        {
            Dispose(false);
        }
    }
}

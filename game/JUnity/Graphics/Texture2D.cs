using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;

namespace JUnity.Graphics
{
    internal class Texture2D : IDisposable
    {
        public int Id { get; }

        public int Width { get; }

        public int Height { get; }

        public string Name { get; }

        internal static void Reset()
        {
            textures.Clear();
        }

        internal static Texture2D TryGetByName(string filename)
        {
            if (textures.TryGetValue(filename, out var tmp))
            {
                tmp.usage++;
                return tmp;
            }
            return null;
        }

        internal Texture2D(int id, int width, int height, string name)
        {
            Id = id;
            Width = width;
            Height = height;
            Name = name;
            usage++;
            try
            {
                textures.Add(name, this);
            }
            catch (ArgumentException)
            { }
        }

        private static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

        private int usage;

        bool isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool isDisposing)
        {
            if (!isDisposed)
            {
                if (isDisposing)
                {
                    if (--usage <= 0)
                    {
                        GL.DeleteTexture(Id);
                        textures.Remove(Name);
                    }
                }

                isDisposed = true;
            }
        }

        ~Texture2D()
        {
            Dispose(false);
        }
    }
}

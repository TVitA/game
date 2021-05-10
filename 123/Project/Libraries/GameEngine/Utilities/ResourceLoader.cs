using System;
using System.IO;
using System.Drawing;

using OpenTK.Graphics.OpenGL;

using GameEngine.Graphics;

namespace GameEngine.Utilities
{
    /// <summary>
    /// Resource loader class.
    /// </summary>
    internal static class ResourceLoader : Object
    {
        /// <summary>
        /// Create texture or returns it.
        /// </summary>
        /// <param name="path">Path to sprite.</param>
        /// <param name="textureMinFilter">Texture min filter.</param>
        /// <param name="textureMagFilter">Texture mag filter.</param>
        /// <returns></returns>
        public static Texture2D LoadTexture2D(String path,
            TextureMinFilter textureMinFilter = TextureMinFilter.Linear,
            TextureMagFilter textureMagFilter = TextureMagFilter.Linear)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to load sprite", path);
            }

            var texture = Texture2D.TryGetByName(path);

            if (texture != null)
            {
                return texture;
            }

            Bitmap bmp = new Bitmap(path);

            bmp.RotateFlip(RotateFlipType.Rotate180FlipX);

            var data = bmp.LockBits(
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb
            );

            int id = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, id);

            GL.TexImage2D(
                TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba,
                data.Width, data.Height, 0, PixelFormat.Bgra,
                PixelType.UnsignedByte, data.Scan0
            );

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (Int32)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (Int32)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (Int32)textureMinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (Int32)textureMagFilter);

            return new Texture2D(id, bmp.Width, bmp.Height, path);
        }
    }
}

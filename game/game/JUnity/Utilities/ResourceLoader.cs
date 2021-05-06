using JUnity.Graphics;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OpenTK.Graphics.OpenGL;

namespace JUnity.Utilities
{
    internal sealed class ResourceLoader
    {
        public static Texture2D LoadTexture2D(string path, TextureMinFilter textureMinFilter = TextureMinFilter.Linear, TextureMagFilter textureMagFilter = TextureMagFilter.Linear)
        {
            var tmp = Texture2D.TryGetByName(path);
            if (tmp != null)
            {
                return tmp;
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException("Unable to load sprite", path);
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
                TextureTarget.Texture2D,
                0, PixelInternalFormat.Rgba,
                data.Width,
                data.Height,
                0,
                PixelFormat.Bgra,
                PixelType.UnsignedByte,
                data.Scan0
            );

            bmp.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.ClampToEdge);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)textureMinFilter);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)textureMagFilter);

            return new Texture2D(id, bmp.Width, bmp.Height, path);
        }
    }
}

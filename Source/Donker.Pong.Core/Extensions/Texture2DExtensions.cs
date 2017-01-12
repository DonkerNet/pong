using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Donker.Pong.Common.Extensions
{
    /// <summary>
    /// Contains extension methods for the <see cref="Texture2D"/> class.
    /// </summary>
    public static class Texture2DExtensions
    {
        /// <summary>
        /// Sets all the pixels of a 2D texture with a single color.
        /// </summary>
        /// <param name="texture2D">The texture to set the color for.</param>
        /// <param name="color">The color to set.</param>
        public static void FillColor(this Texture2D texture2D, Color color)
        {
            Color[] colors = new Color[texture2D.Width * texture2D.Height];

            if (colors.Length == 1)
            {
                colors[0] = color;
            }
            else
            {
                for (int x = 0; x < texture2D.Width; ++x)
                {
                    for (int y = 0; y < texture2D.Height; ++y)
                    {
                        colors[x + y * texture2D.Width] = color;
                    }
                }
            }

            texture2D.SetData(colors);
        }
    }
}
using System;
using Android.Graphics;

namespace Styles
{
    public static class ColorExtensions
    {
        /// <summary>
        /// Convert a ColorRGB to a Native Color instace.
        /// </summary>
        /// <returns>UIColor/returns>
        /// <param name="color">Color.</param>
        public static Color ToNative(this IColorSpace color)
        {
            var rgb = color.ToRgb();
            return new Color((int)(rgb.R * 255), (int)(rgb.G * 255), (int)(rgb.B * 255), (int)(rgb.A * 255));
        }

        public static Color[] ToNative(this IRgb[] colors)
        {
            var nativeColors = new Color[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                nativeColors[i] = colors[i].ToNative();
            }
            return nativeColors;
        }

        public static int[] ToInt(this IRgb[] colors)
        {
            var nativeColors = new int[colors.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                nativeColors[i] = colors[i].ToNative().ToArgb();
            }
            return nativeColors;
        }

        /// <summary>
        /// Converts a UIColor value to a ColorRGB object.
        /// </summary>
        /// <returns>ColorRGB</returns>
        /// <param name="color">Target UIColor</param>
        public static ColorRGB ToColorRGB(this Color color)
        {
            return ColorRGB.FromRGBA(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Converts a Color value to a string hex value
        /// </summary>
        /// <returns>A string hex value</returns>
        /// <param name="color">Target UIColor</param>
        public static string ToHex(this Color color)
        {
            return string.Format("#{0:X2}{1:X2}{2:X2}", color.R, color.G, color.B);
        }


        /// <summary>
        /// Creates a Color from a int hex value
        /// </summary>
        /// <returns>Color</returns>
        /// <param name="color">Extension Color reference</param>
        /// <param name="hexValue">Hex value as an int</param>
        public static Color FromHex(this Color color, int hexValue)
        {
            return new Color(hexValue);
        }

        /// <summary>
        /// Creates a UIColor from a string hex value
        /// </summary>
        /// <returns>UIColor</returns>
        /// <param name="color">Extension UIColor reference</param>
        /// <param name="hexValue">Hex value as an int</param>
        /// <param name="alpha">Alpha value of the color</param>
        public static Color FromHex(this Color color, string hexValue, float alpha = 1f)
        {
            //return Color.ParseColor (hexValue);

            int red, green, blue, iAlpha;
            var colorString = hexValue.Replace("#", "");
            iAlpha = (int)Math.Round(255f / alpha);

            if (alpha > 1.0f)
            {
                iAlpha = 255;
            }
            else if (alpha < 0.0f)
            {
                iAlpha = 0;
            }

            switch (colorString.Length)
            {
                case 3: // #RGB
                    {
                        red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16);
                        green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16);
                        blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16);
                        return new Color(red, green, blue, iAlpha);
                    }
                case 6: // #RRGGBB
                    {
                        red = Convert.ToInt32(colorString.Substring(0, 2), 16);
                        green = Convert.ToInt32(colorString.Substring(2, 2), 16);
                        blue = Convert.ToInt32(colorString.Substring(4, 2), 16);
                        return new Color(red, green, blue, iAlpha);
                    }
                case 8: // #AARRGGBB
                    {
                        iAlpha = Convert.ToInt32(colorString.Substring(0, 2), 16);
                        red = Convert.ToInt32(colorString.Substring(2, 2), 16);
                        green = Convert.ToInt32(colorString.Substring(4, 2), 16);
                        blue = Convert.ToInt32(colorString.Substring(6, 2), 16);
                        return new Color(red, green, blue, iAlpha);
                    }

                default:
                    throw new ArgumentOutOfRangeException(string.Format("Invalid color value {0} is invalid. It should be a hex value of the form #RBG, #RRGGBB", hexValue));
            }
        }
    }
}


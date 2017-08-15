using System;
using System.Collections.Generic;
using Styles;

namespace Styles
{
    public static class BitmapUtils
    {
        // TODO implement methods
        // colorWithAverageColorFromImage

        // TODO rename and perhaps return a ColorScheme
        public static ColorSet GetColorSet(this Bitmap bitmap)
        {
            var result = new ColorSet();

            var randomColorsThreshold = (bitmap.Height * 0.01);
            var blackColor = ColorRGB.Black;
            var whiteColor = ColorRGB.White;

            var leftEdgeColors = new List<ColorRGB>();
            var imageColors = bitmap.GetColorArray();

            // Work out the edge colors
            var count = 0;
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    // A lot of albums have white or black edges from crops, so ignore the first few pixels
                    if (5 <= x && x <= 10)
                    {
                        leftEdgeColors.Add(imageColors[count]);
                    }
                    count++;
                }
            }

            // TODO check which collection I should be looping
            // Get background color
            var sortedColors = new List<ColorCount>();
            for (int i = 0; i < leftEdgeColors.Count; i++)
            {
                var colorCount = leftEdgeColors.FindAll((ColorRGB obj) => { return obj == leftEdgeColors[i]; }).Count;
                if (randomColorsThreshold < colorCount)
                {
                    sortedColors.Add(new ColorCount(leftEdgeColors[i], colorCount));
                }
            }

            sortedColors.Sort(SortColors);

            ColorCount proposedEdgeColor;
            if (sortedColors.Count > 0)
            {
                proposedEdgeColor = sortedColors[0];
            }
            else
            {
                proposedEdgeColor = new ColorCount(blackColor, 1);
            }

            if (proposedEdgeColor.Color.IsBlackOrWhite() && sortedColors.Count > 0)
            {
                for (int i = 0; i < sortedColors.Count; i++)
                {
                    var nextProposedEdgeColor = sortedColors[i];
                    if ((float)nextProposedEdgeColor.Count / (float)proposedEdgeColor.Count > 0.3f)
                    {
                        if (!nextProposedEdgeColor.Color.IsBlackOrWhite())
                        {
                            proposedEdgeColor = nextProposedEdgeColor;
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            result.BackgroundColor = proposedEdgeColor.Color;

            // Get foreground colors
            sortedColors.Clear();
            var findDarkTextColor = !result.BackgroundColor.IsDarkColor();

            foreach (var color in imageColors)
            {
                var colorMin = color.ColorWithMinimumSaturation(0.15f);
                if (colorMin.IsDarkColor() == findDarkTextColor)
                {
                    var colorCount = leftEdgeColors.FindAll((ColorRGB obj) => { return obj == colorMin; }).Count;
                    sortedColors.Add(new ColorCount(colorMin, colorCount));
                }
            }

            sortedColors.Sort(SortColors);
            foreach (var countedColor in sortedColors)
            {
                var color = countedColor.Color;

                // Set the primary color
                if (result.PrimaryColor == ColorRGB.Empty)
                {
                    if (color.IsContrastingColor(result.BackgroundColor))
                    {
                        result.PrimaryColor = color;
                    }
                }

                // Set the Secondary Color
                else if (result.SecondaryColor == ColorRGB.Empty)
                {
                    if (!result.PrimaryColor.IsDistinct(color) || !color.IsContrastingColor(result.BackgroundColor))
                    {
                        continue;
                    }

                    result.SecondaryColor = color;
                }

                // Set the Detail Color
                else if (result.DetailColor == ColorRGB.Empty)
                {
                    if (!result.SecondaryColor.IsDistinct(color) || !result.PrimaryColor.IsDistinct(color) || !color.IsContrastingColor(result.BackgroundColor))
                    {
                        continue;
                    }

                    result.DetailColor = color;
                    break;
                }
            }

            var isDarkBackground = result.BackgroundColor.IsDarkColor();
            if (result.PrimaryColor == ColorRGB.Empty)
                result.PrimaryColor = isDarkBackground ? whiteColor : blackColor;

            if (result.SecondaryColor == ColorRGB.Empty)
                result.SecondaryColor = isDarkBackground ? whiteColor : blackColor;

            if (result.DetailColor == ColorRGB.Empty)
                result.DetailColor = isDarkBackground ? whiteColor : blackColor;

            return result;
        }

        private static int SortColors(ColorCount x, ColorCount y)
        {
            if (x.Count < y.Count)
                return -1;
            if (x.Count == y.Count)
                return 0;
            return 1;
        }
    }
}


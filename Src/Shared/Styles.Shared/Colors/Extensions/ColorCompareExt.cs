using System;
namespace Styles
{
	public static class ColorCompareExt
	{
		public static bool IsDarkColor(this IColorSpace color)
		{
			var rgb = color.ToRgb();
			return (0.2126 * rgb.R + 0.7152 * rgb.G + 0.0722 * rgb.B) < 0.5;
		}

		public static bool IsBlackOrWhite(this IColorSpace color)
		{
			var rgb = color.ToRgb();
			return (rgb.R > 0.91 && rgb.G > 0.91 && rgb.B > 0.91) || (rgb.R < 0.09 && rgb.G < 0.09 && rgb.B < 0.09);
		}

		public static bool IsDistinct(this IColorSpace color, IColorSpace target)
		{
			var rgb = color.ToRgb();
			var comparison = target.ToRgb();
			var threshold = 0.25;

			if (Math.Abs(rgb.R - comparison.R) > threshold || Math.Abs(rgb.G - comparison.G) > threshold || Math.Abs(rgb.B - comparison.B) > threshold)
			{
				if (Math.Abs(rgb.R - rgb.G) < 0.03 && Math.Abs(rgb.R - rgb.B) < 0.03)
				{
					if (Math.Abs(comparison.R - comparison.G) < 0.03 && Math.Abs(comparison.R - comparison.B) < 0.03)
					{
						return false;
					}
				}
				return true;
			}

			return false;
		}

		public static bool IsContrastingColor(this IColorSpace color, IColorSpace target)
		{
			var rgb = color.ToRgb();
			var comparison = target.ToRgb();

			var bgLum = 0.2126 * rgb.R + 0.7152 * rgb.G + 0.0722 * rgb.B;
			var fgLum = 0.2126 * comparison.R + 0.7152 * comparison.G + 0.0722 * comparison.B;

			var bgGreater = bgLum > fgLum;
			var nom = bgGreater ? bgLum : fgLum;

			var denom = bgGreater ? fgLum : bgLum;
			var contrast = (nom + 0.05) / (denom + 0.05);

			return (1.6 < contrast);
		}
	}
}

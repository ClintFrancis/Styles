using System;
namespace Styles
{
	public static class ColorValueExt
	{
		public static string ToHex(this IColorSpace color)
		{
			var rgb = color.ToRgb();
			if (rgb.A == 1) return string.Format("#{0:X2}{1:X2}{2:X2}", (int)(rgb.R * 255), (int)(rgb.G * 255), (int)(rgb.B * 255));

			return string.Format("#{0:X2}{1:X2}{2:X2}{3:X2}", (int)rgb.A * 255, (int)(rgb.R * 255), (int)(rgb.G * 255), (int)(rgb.B * 255));
		}

		public static uint ToValueRGB(this IColorSpace color)
		{
			var rgb = (ColorRGB)color.ToRgb();
			return (uint)(rgb.Red << 16 | rgb.Green << 8 | rgb.Blue);
		}

		public static uint ToValueARGB(this IColorSpace color)
		{
			var rgb = (ColorRGB)color.ToRgb();
			return (uint)(rgb.Alpha << 24 | rgb.Red << 16 | rgb.Green << 8 | rgb.Blue);
		}

		public static uint ToValueRGBA(this IColorSpace color)
		{
			var rgb = (ColorRGB)color.ToRgb();
			return (uint)(rgb.Red << 24 | rgb.Green << 16 | rgb.Blue << 8 | rgb.Alpha);
		}
	}
}

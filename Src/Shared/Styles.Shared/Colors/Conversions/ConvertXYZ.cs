using System;
namespace Styles
{
	internal static class ConvertXYZ
	{
		internal static ColorXYZ WhiteReference = new ColorXYZ(95.047, 100, 108.883);
		internal static double Epsilon = 0.008856;
		internal static double Kappa = 903.3;

		internal static IXyz ToColorSpace (IRgb color)
		{
			var r = PivotRgb(color.R);
			var g = PivotRgb(color.G);
			var b = PivotRgb(color.B);

			var x = r * 0.4124 + g * 0.3576 + b * 0.1805;
			var y = r * 0.2126 + g * 0.7152 + b * 0.0722;
			var z = r * 0.0193 + g * 0.1192 + b * 0.9505;

			return new ColorXYZ(x, y, z);
		}

		internal static IRgb ToColor (IXyz item)
		{
			var x = item.X / 100;
			var y = item.Y / 100;
			var z = item.Z / 100;

			var r = x * 3.2406 + y * -1.5372 + z * -0.4986;
			var g = x * -0.9689 + y * 1.8758 + z * 0.0415;
			var b = x * 0.0557 + y * -0.204 + z * 1.057;

			r = r > 0.0031308 ? 1.055 * Math.Pow(r, 0.416666666666667) - 0.055 : 12.92 * r;
			g = g > 0.0031308 ? 1.055 * Math.Pow(g, 0.416666666666667) - 0.055 : 12.92 * g;
			b = b > 0.0031308 ? 1.055 * Math.Pow(b, 0.416666666666667) - 0.055 : 12.92 * b;

			return new ColorRGB(ToRgb(r), ToRgb(g), ToRgb(b));
		}

		static double ToRgb(double n)
		{
			var result = 255 * n;
			if (result < 0)
				return 0;
			if (result > 255)
				return 255;
			return result/255;
		}

		static double CubicRoot(double n)
		{
			return Math.Pow(n, 0.333333333333333);
		}

		static double PivotRgb(double n)
		{
			return (n > 0.04045 ? Math.Pow((n + 0.055) / 1.055, 2.4) : n / 12.92) * 100;
		}
	}
}


using System;
namespace Styles
{
	internal static class ConvertLAB
	{
		internal static ILab ToColorSpace (IRgb color)
		{
			var xyz = ColorXYZ.FromColor(color);

			var white = ConvertXYZ.WhiteReference;
			var x = PivotXyz(xyz.X / white.X);
			var y = PivotXyz(xyz.Y / white.Y);
			var z = PivotXyz(xyz.Z / white.Z);

			var l = Math.Max(0, 116 * y - 16);
			var a = 500 * (x - y);
			var b = 200 * (y - z);

			return new ColorLAB(l, a, b);
		}

		internal static IRgb ToColor (ILab item)
		{
			var y = (item.L + 16) / 116;
			var x = item.A / 500 + y;
			var z = y - item.B / 200;

			var white = ConvertXYZ.WhiteReference;
			var x3 = x * x * x;
			var z3 = z * z * z;

			var xyz = ColorXYZ.Empty;
			xyz.X = white.X * (x3 > 0.008856 ? x3 : (x - 0.137931034482759) / 7.787);
			xyz.Y = white.Y * (item.L > (7.9996248) ? System.Math.Pow(((item.L + 16) / 116), 3) : item.L / 903.3);
			xyz.Z = white.Z * (z3 > 0.008856 ? z3 : (z - 0.137931034482759) / 7.787);

			return xyz.ToRgb();
		}

		static double PivotXyz(double n)
		{
			return n > ConvertXYZ.Epsilon ? CubicRoot(n) : (ConvertXYZ.Kappa * n + 16) / 116;
		}

		static double CubicRoot(double n)
		{
			return Math.Pow(n, 1.0 / 3.0);
		}
	}
}


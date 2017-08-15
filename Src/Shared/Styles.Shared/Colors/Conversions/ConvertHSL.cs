using System;
namespace Styles
{
	internal static class ConvertHSL
	{
		internal static IHsl ToColorSpace (IRgb rgb)
		{
			var max = Math.Max(rgb.R, Math.Max(rgb.G, rgb.B));
			var min = Math.Min(rgb.R, Math.Min(rgb.G, rgb.B));
			double h = 0, s = 0, l = (max + min) / 2d;
			if (max == min)
			{
				h = s = 0;
			}
			else
			{
				var d = max - min;
				s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
				if (rgb.R == max)
				{
					h = (rgb.G - rgb.B) / d + (rgb.G < rgb.B ? 6 : 0);
				}
				else if (rgb.G == max)
				{
					h = (rgb.B - rgb.R) / d + 2;
				}
				else if (rgb.B == max)
				{
					h = (rgb.R - rgb.G) / d + 4;
				}
				h = h / 6;
			}
			return new ColorHSL(h, s, l, rgb.A);
		}

		public static ColorRGB ToColor(IHsl hsl)
		{
			if (hsl.S == 0)
			{
				return new ColorRGB(hsl.L, hsl.L, hsl.L, hsl.A);
			}
			else
			{
				var q = (hsl.L < 0.5) ? (hsl.L * (1 + hsl.S)) : (hsl.L + hsl.S - (hsl.L * hsl.S));
				var p = (2 * hsl.L) - q;
				var Hk = hsl.H;
				var T = new double[3];
				T[0] = Hk + (0.333333333333333d);
				T[1] = Hk;
				T[2] = Hk - (0.333333333333333d);
				for (var i = 0; i < 3; i++)
				{
					if (T[i] < 0)
						T[i] += 1;
					if (T[i] > 1)
						T[i] -= 1;
					if ((T[i] * 6) < 1)
					{
						T[i] = p + ((q - p) * 6 * T[i]);
					}
					else if ((T[i] * 2) < 1)
					{
						T[i] = q;
					}
					else if ((T[i] * 3) < 2)
					{
						T[i] = p + (q - p) * ((0.666666666666667d) - T[i]) * 6;
					}
					else
						T[i] = p;
				}
				return new ColorRGB(T[0], T[1], T[2], hsl.A);
			}
		}
	}
}


using System;
using Styles;

namespace SharedColorTests
{
	public class ColorTestUtils
	{
		public struct HSL : IHsl
		{
			public double H
			{
				get; set;
			}

			public double L
			{
				get; set;
			}

			public double S
			{
				get; set;
			}

			public HSL(double h, double s, double l){
				H = h;
				S = s;
				L = l;
				CheckValues();
			}

			public void Initialize(IRgb color)
			{
				var result = RGBtoHSL(color);
				this.H = result.H;
				this.S = result.S;
				this.L = result.L;
				CheckValues();
			}

			public IRgb ToRgb()
			{
				return HSLtoRGB(this);
			}

			override public string ToString(){
				return "hsl(" + Math.Round(this.H * 360, 1) + ", " + Math.Round(this.S * 100, 1) + "%, " + Math.Round(this.L * 100, 1) + "%)";
			}

			void CheckValues(){
				this.H = this.H % 1;
				if (this.H < 0)
					this.H += 1;
				this.S = Math.Max(0, Math.Min(1, this.S));
				this.L = Math.Max(0, Math.Min(1, this.L));
				//this.alpha = Math.Max(0, Math.Min(1, this.alpha));
			}
		}

		public static IHsl RGBtoHSL(IRgb color){
			var max = Math.Max(color.R, Math.Max(color.G, color.B));
			var min = Math.Min(color.R, Math.Min(color.G, color.B));
			double h = 0d, s = 0d, l = (max + min) / 2d;
			if (max == min)
			{
				h = s = 0;
			}
			else
			{
				var d = max - min;
				s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
				if (color.R == max)
				{
					h = (color.G - color.B) / d + (color.G < color.B ? 6 : 0);
				}
				else if (color.G == max)
				{
					h = (color.B - color.R) / d + 2;
				}
				else if (color.B == max)
				{
					h = (color.R - color.G) / d + 4;
				}
				h = h / 6;
			}
			return new HSL(h, s, l);
		}

		public static IRgb HSLtoRGB(IHsl hsl)
		{
			if (hsl.S == 0)
			{
				return new ColorRGB(hsl.L, hsl.L, hsl.L );
			}
			else
			{
				var q = (hsl.L < 0.5) ? (hsl.L * (1 + hsl.S)) : (hsl.L + hsl.S - (hsl.L * hsl.S));
				var p = (2 * hsl.L) - q;
				var Hk = hsl.H;
				var T = new double[3];
				T[0] = Hk + (0.333333333333333);
				T[1] = Hk;
				T[2] = Hk - (0.333333333333333);
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
						T[i] = p + (q - p) * ((0.666666666666667) - T[i]) * 6;
					}
					else
						T[i] = p;
				}
				return new ColorRGB(T[0], T[1], T[2]);
			}
		}
	}
}

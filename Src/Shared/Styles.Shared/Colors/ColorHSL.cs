using System;
namespace Styles
{
	public struct ColorHSL : IHsl
	{
		public static readonly ColorHSL Empty = new ColorHSL();

		double hue;
		double saturation;
		double luminance;
		double alpha;

		#region Operators
		public static bool operator ==(ColorHSL item1, ColorHSL item2)
		{
			return (
				item1.H == item2.H
				&& item1.S == item2.S
				&& item1.L == item2.L
				);
		}

		public static bool operator !=(ColorHSL item1, ColorHSL item2)
		{
			return (
				item1.H != item2.H
				|| item1.S != item2.S
				|| item1.L != item2.L
				);
		}

		#endregion

		#region Accessors

		public double H
		{
			get { return hue;}
			set {  
				hue = value % 1;
				if (hue < 0) hue += 1;
			}
		}

		public double Hue{
			get { return Math.Round(hue * 360, 1); }
			set
			{
				value = value % 360;
				if (value < 0)
					value += 360;

				hue = value / 360;
			}
		}

		public double S
		{
			get { return saturation; }
			set { saturation = Math.Max(0, Math.Min(1, value)); }
		}

		public double Saturation
		{
			get { return Math.Round(saturation * 100, 2); }
			set { saturation = Math.Max(0, Math.Min(100, value)); }
		}

		public double L
		{
			get { return luminance; }
			set { luminance = Math.Max(0, Math.Min(1, value)); }
		}

		public double Luminance
		{
			get { return Math.Round(luminance * 100, 2); }
			set { luminance = Math.Max(0, Math.Min(100, value)); }
		}

		public double A
		{
			get { return alpha; }
			set { alpha = Math.Max(0, Math.Min(1, value)); }
		}

		#endregion
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Styles.Core.ColorHSL"/> struct.
		/// </summary>
		/// <param name="h">Hue value, from 0 to 360</param>
		/// <param name="s">Saturation, from 0 to 1</param>
		/// <param name="l">Luminance, from 0 to 1</param> 
		public ColorHSL(double h, double s, double l, double a = 1)
		{
			hue = saturation = luminance = alpha = 0;

			this.H = h;
			this.S = s;
			this.L = l;
			this.A = a;
		}

		public override bool Equals(Object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return (this == (ColorHSL)obj);
		}

		public override int GetHashCode()
		{
			return H.GetHashCode() ^ S.GetHashCode() ^ L.GetHashCode();
		}

		public override string ToString()
		{
			if (this.alpha == 1)
				return "hsl(" + Math.Round(this.H * 360, 1) + ", " + Math.Round(this.S * 100, 1) + "%, " + Math.Round(this.L * 100, 1) + "%)";
            else
                return "hsla(" + Math.Round(this.H * 360, 1) + ", " + Math.Round(this.S * 100, 1) + "%, " + Math.Round(this.L * 100, 1) + "%, " + Math.Round(this.alpha, 3) + ")";
		}

		#region IColorSpace implementation
		public void Initialize(IRgb color)
		{
			var hsl = ConvertHSL.ToColorSpace(color);
			this.H = hsl.H;
			this.S = hsl.S;
			this.L = hsl.L;
			// TODO Alpha
			alpha = 1;
		}

		public IRgb ToRgb()
		{
			return ConvertHSL.ToColor(this);
		}
		#endregion

		public static IHsl FromColor(IRgb color)
		{
			return ConvertHSL.ToColorSpace(color);
		}

		public static IRgb ToColor(double hue, double saturation, double luminosity)
		{
			return new ColorHSL(hue, saturation, luminosity).ToRgb();
		}

		public static ColorHSL FromHSL(int h, int s, int l)
		{
			return FromHSL((byte)h, (byte)s, (byte)l);
		}

		public static ColorHSL FromHSL(byte h, byte s, byte l)
		{
			return new ColorHSL()
			{
				Hue = h,
				Saturation = s,
				Luminance = l
			};
		}
	}
}


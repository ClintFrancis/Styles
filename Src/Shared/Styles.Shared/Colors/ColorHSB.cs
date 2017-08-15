using System;
namespace Styles
{
	public struct ColorHSB : IHsb
	{
		public static readonly ColorHSB Empty = new ColorHSB();

		double hue;
		double saturation;
		double brightness;
		double alpha;

		#region Operators
		public static bool operator ==(ColorHSB item1, ColorHSB item2)
		{
			return (
				item1.H == item2.H
				&& item1.S == item2.S
				&& item1.B == item2.B
				);
		}

		public static bool operator !=(ColorHSB item1, ColorHSB item2)
		{
			return (
				item1.H != item2.H
				|| item1.S != item2.S
				|| item1.B != item2.B
				);
		}
		#endregion

		#region Accessors
		public double H
		{
			get { return hue;}
			set {hue = Math.Max(0, Math.Min(1, value));}
		}

		public double Hue
		{
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

		public double B
		{
			get { return brightness; }
			set { brightness = Math.Max(0, Math.Min(1, value)); }
		}

		public double Brightness
		{
			get { return Math.Round(brightness * 100, 2); }
			set { brightness = Math.Max(0, Math.Min(100, value)); }
		}

		public double A
		{
			get { return alpha; }
			set { alpha = Math.Max(0, Math.Min(1, value)); }
		}
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Styles.Core.ColorHSB"/> struct.
		/// </summary>
		/// <param name="h">Hue value, from 0 to 1</param>
		/// <param name="s">Saturation, from 0 to 1</param>
		/// <param name="b">Brightness, from 0 to 1</param>
		public ColorHSB(double h, double s, double b)
		{
			hue = saturation = brightness = 0;
			alpha = 1;

			H = h;
			S = s;
			B = b;
		}

		public override bool Equals(Object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return (this == (ColorHSB)obj);
		}

		public override int GetHashCode()
		{
			return H.GetHashCode() ^ S.GetHashCode() ^ B.GetHashCode();
		}

		public override string ToString()
		{
			if (alpha == 1)
				return "hsv(" + Math.Round(hue * 360, 1) + ", " + Math.Round(saturation * 100, 1) + "%, " + Math.Round(brightness * 100, 1) + "%)";
			return "hsva(" + Math.Round(hue * 360, 1) + ", " + Math.Round(saturation * 100, 1) + "%, " + Math.Round(brightness * 100, 1) + "%, " + Math.Round(this.alpha, 3) + ")";
		}

		#region IColorSpace implementation
		public void Initialize(IRgb color)
		{
			var hsl = ConvertHSB.ToColorSpace(color);
			this.H = hsl.H;
			this.S = hsl.S;
			this.B = hsl.B;
			alpha = 1;
		}

		public IRgb ToRgb()
		{
			return ConvertHSB.ToColor(this);
		}
		#endregion

		public static IHsb FromColor(IRgb color)
		{
			return ConvertHSB.ToColorSpace(color);
		}

		public static IRgb ToColor(double hue, double saturation, double brightness)
		{
			return new ColorHSB(hue, saturation, brightness).ToRgb();
		}

		public static ColorHSB FromHSB(int h, int s, int b)
		{
			return FromHSB((byte)h, (byte)s, (byte)b);
		}

		public static ColorHSB FromHSB(byte h, byte s, byte b)
		{
			return new ColorHSB()
			{
				Hue = h,
				Saturation = s,
				Brightness = b
			};
		}
	}
}


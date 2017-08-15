using System;
namespace Styles
{
	public partial struct ColorRGB : IRgb
	{
		public static readonly ColorRGB Empty = new ColorRGB(0, 0, 0, 0);
		public static readonly ColorRGB White = new ColorRGB(1, 1, 1, 1);
		public static readonly ColorRGB Black = new ColorRGB(0, 0, 0, 1);

		#region Operators
		public static bool operator ==(ColorRGB item1, ColorRGB item2)
		{
			return (
				item1.R == item2.R
				&& item1.G == item2.G
				&& item1.B == item2.B
				);
		}

		public static bool operator !=(ColorRGB item1, ColorRGB item2)
		{
			return (
				item1.R != item2.R
				|| item1.G != item2.G
				|| item1.B != item2.B
				);
		}

		#endregion

		#region Accessors

		double red;
		double green;
		double blue;
		double alpha;
		
		public double R
		{
			get { return red;}
			set { red = Math.Max(0, Math.Min(1, value)); }
		}

		public byte Red
		{
			get{ return (byte)Math.Round(this.R * 255); }
			set{ this.R = value / 255; }
		}

		public double G
		{
			get { return green; }
			set { green = Math.Max(0, Math.Min(1, value)); }
		}

		public byte Green
		{
			get { return (byte)Math.Round(this.G * 255); }
			set { this.G = value / 255; }
		}

		public double B
		{
			get { return blue; }
			set { blue = Math.Max(0, Math.Min(1, value)); }
		}

		public byte Blue
		{
			get { return (byte)Math.Round(this.B * 255); }
			set { this.B = value / 255; }
		}

		public double A
		{
			get { return alpha; }
			set { alpha = Math.Max(0, Math.Min(1, value)); }
		}

		public byte Alpha
		{
			get { return (byte)Math.Round(this.A * 255); }
			set { this.A = value / 255; }
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Styles.Core.ColorRGB"/> struct.
		/// </summary>
		/// <param name="r">Red, from 0 to 1</param>
		/// <param name="g">Green, from 0 to 1</param>
		/// <param name="b">Blue, from 0 to 1</param>
		/// <param name="a">Alpha, from 0 to 1</param>
		public ColorRGB(double r, double g, double b, double a = 1)
		{
			red = green = blue = alpha = 0;

			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		public override bool Equals(Object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return (this == (ColorRGB)obj);
		}

		public override int GetHashCode()
		{
			return R.GetHashCode() ^ G.GetHashCode() ^ B.GetHashCode();
		}

		public override string ToString()
		{
			if (alpha == 1) return "rgb(" + this.Red + ", " + this.Green + ", " + this.Blue + ")";

			return "rgba(" + this.Red + ", " + this.Green + ", " + this.Blue + ", " + Math.Round(this.alpha, 3) + ")";
		}

		#region IColorSpace implementation
		public void Initialize(IRgb color)
		{
			R = color.R;
			G = color.G;
			B = color.B;
		}

		public IRgb ToRgb()
		{
			return this;
		}
		#endregion

		public static ColorRGB FromHex(string hexString)
		{
			var colorString = hexString.Replace("#", "").ToUpper();
			int alpha, red, green, blue;

			switch (colorString.Length)
			{
				case 3: // #RGB
					{
						red = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(0, 1)), 16);
						green = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(1, 1)), 16);
						blue = Convert.ToInt32(string.Format("{0}{0}", colorString.Substring(2, 1)), 16);
						return new ColorRGB(red / 255d, green / 255d, blue / 255d);
					}
				case 6: // #RRGGBB
					{
						red = Convert.ToInt32(colorString.Substring(0, 2), 16);
						green = Convert.ToInt32(colorString.Substring(2, 2), 16);
						blue = Convert.ToInt32(colorString.Substring(4, 2), 16);
						return new ColorRGB(red / 255d, green / 255d, blue / 255d);
					}
				case 8: // #AARRGGBB
					{
						alpha = Convert.ToInt32(colorString.Substring(0, 2), 16);
						red = Convert.ToInt32(colorString.Substring(2, 2), 16);
						green = Convert.ToInt32(colorString.Substring(4, 2), 16);
						blue = Convert.ToInt32(colorString.Substring(6, 2), 16);
						return new ColorRGB(red / 255d, green / 255d, blue / 255d, alpha / 255d);
					}
				default:
					throw new ArgumentOutOfRangeException(string.Format("Invalid color value {0} is invalid. It should be a hex value of the form #RBG, #RRGGBB", hexString));
			}
		}

		public static ColorRGB FromRGB(uint value)
		{
			return new ColorRGB(
				((value >> 16) & 0xFF) / 255d, 
				((value >> 8) & 0xFF) / 255d, 
				(value & 0xFF) / 255d
			);
		}

		public static ColorRGB FromRGB(int r, int g, int b)
		{
			return FromRGB((byte)r, (byte)g, (byte)b);
		}

		public static ColorRGB FromRGB(byte r, byte g, byte b)
		{
			return new ColorRGB(r / 255d, g / 255d, b / 255d);
		}

		public static ColorRGB FromRGBA(int r, int g, int b, double a)
		{
			return FromRGBA((byte)r, (byte)g, (byte)b, a);
		}

		public static ColorRGB FromRGBA(byte r, byte g, byte b, double a)
		{
			return new ColorRGB(r / 255d, g / 255d, b / 255d, a);
		}

		public static ColorRGB FromARGB(uint value)
		{
			return new ColorRGB(
				((value >> 16) & 0xFF) / 255d, 
				((value >> 8) & 0xFF) / 255d, 
				(value & 0xFF) / 255d,
				((value >> 24) & 0xFF) / 255d
			);
		}
	}
}


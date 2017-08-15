using System;
namespace Styles
{
	public struct ColorLAB : ILab
	{
		public static readonly ColorLAB Empty = new ColorLAB();

		double _l;
		double _a;
		double _b;

		#region Operators
		public static bool operator ==(ColorLAB item1, ColorLAB item2)
		{
			return (
				item1.L == item2.L
				&& item1.A == item2.A
				&& item1.B == item2.B
				);
		}

		public static bool operator !=(ColorLAB item1, ColorLAB item2)
		{
			return (
				item1.L != item2.L
				|| item1.A != item2.A
				|| item1.B != item2.B
				);
		}

		#endregion

		public double L
		{
			get { return _l; }
			set { _l = Math.Max(0, Math.Min(100, value)); }
		}

		public double A
		{
			get { return _a; }
			set { _a = Math.Max(-128, Math.Min(128, value)); }
		}

		public double B
		{
			get { return _b; }
			set { _b = Math.Max(-128, Math.Min(128, value)); }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Styles.Core.ColorLAB"/> struct.
		/// </summary>
		/// <param name="l">L, from 0 to 100</param>
		/// <param name="a">A, from -128 to 128</param>
		/// <param name="b">B, from -128 to 128</param>
		public ColorLAB(double l, double a, double b)
		{
			_l = _a = _b = 0;

			this.L = l;
			this.A = a;
			this.B = b;
		}

		public override bool Equals(Object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return (this == (ColorLAB)obj);
		}

		public override int GetHashCode()
		{
			return L.GetHashCode() ^ A.GetHashCode() ^ B.GetHashCode();
		}

		#region IColorSpace implementation
		public void Initialize(IRgb color)
		{
			var lab = ConvertLAB.ToColorSpace(color);
			this.L = lab.L;
			this.A = lab.A;
			this.B = lab.B;
		}

		public IRgb ToRgb()
		{
			return ConvertLAB.ToColor(this);
		}

		public override string ToString()
		{
			return "lab(" + Math.Round(L, 2) + ", " + Math.Round(A, 2) + ", " + Math.Round(B, 2) + ")";
		}
		#endregion

		public static ILab FromColor(IRgb color)
		{
			return ConvertLAB.ToColorSpace(color);
		}

		public static IRgb ToColor(double l, double a, double b)
		{
			return new ColorLAB(l, a, b).ToRgb();
		}

		public static ColorLAB FromLAB(int l, int a, int b)
		{
			return new ColorLAB()
			{
				L = l,
				A = a,
				B = b
			};
		}
	}
}


using System;
namespace Styles
{
	public struct ColorXYZ : IXyz
	{
		public static readonly ColorXYZ Empty = new ColorXYZ();

		double _x;
		double _y;
		double _z;

		#region Operators
		public static bool operator ==(ColorXYZ item1, ColorXYZ item2)
		{
			return (
				item1.X == item2.X
				&& item1.Y == item2.Y
				&& item1.Z == item2.Z
				);
		}

		public static bool operator !=(ColorXYZ item1, ColorXYZ item2)
		{
			return (
				item1.X != item2.X
				|| item1.Y != item2.Y
				|| item1.Z != item2.Z
				);
		}

		#endregion

		#region Accessors
		/// <summary>
		/// Gets or sets X component.
		/// </summary>
		public double X
		{
			get { return _x;}
			set { _x = Math.Max(0, Math.Min(100, value)); }
		}

		/// <summary>
		/// Gets or sets Y component.
		/// </summary>
		public double Y
		{
			get { return _y; }
			set { _y = Math.Max(0, Math.Min(100, value)); }
		}

		/// <summary>
		/// Gets or sets Z component.
		/// </summary>
		public double Z
		{
			get { return _z; }
			set { _z = Math.Max(0, Math.Min(100, value)); }
		}

		#endregion
		/// <summary>
		/// Initializes a new instance of the <see cref="T:Styles.Core.ColorXYZ"/> struct.
		/// </summary>
		/// <param name="x">X from 0 to 100</param>
		/// <param name="y">Y from 0 to 100</param>
		/// <param name="z">Z from 0 to 100</param>
		// TODO fix the vairable ranges
		public ColorXYZ(double x, double y, double z)
		{
			_x = _y = _z = 0;

			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		public override bool Equals(Object obj)
		{
			if (obj == null || GetType() != obj.GetType()) return false;

			return (this == (ColorXYZ)obj);
		}

		public override int GetHashCode()
		{
			return X.GetHashCode() ^ Y.GetHashCode() ^ Z.GetHashCode();
		}

		public override string ToString()
		{
			return "xyz(" + Math.Round(X, 2) + ", " + Math.Round(Y, 2) + ", " + Math.Round(Z, 2) + ")";
		}

		#region IColorSpace implementation
		public void Initialize(IRgb color)
		{
			var xyz = ConvertXYZ.ToColorSpace(color);
			this.X = xyz.X;
			this.Y = xyz.Y;
			this.Z = xyz.Z;
		}

		public IRgb ToRgb()
		{
			return ConvertXYZ.ToColor(this);
		}
		#endregion

		public static IXyz FromColor(IRgb color)
		{
			return ConvertXYZ.ToColorSpace(color);
		}

		public static IRgb ToColor(double x, double y, double z)
		{
			return new ColorXYZ(x, y, z).ToRgb();
		}
	}
}


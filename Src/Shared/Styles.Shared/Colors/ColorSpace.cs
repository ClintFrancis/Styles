using System;
namespace Styles
{
	public static class ColorSpaceExtensions
	{
		/// <summary>
		/// Convert any IColorSpace to any other IColorSpace
		/// </summary>
		/// <typeparam name="T">Must implement IColorSpace, new()</typeparam>
		/// <returns></returns>
		public static T To<T>(this IColorSpace colorSpace) where T : IColorSpace, new()
		{
			if (typeof(T) == colorSpace.GetType())
			{
				return (T)colorSpace;
			}

			var newColorSpace = new T();
			newColorSpace.Initialize(colorSpace.ToRgb());

			return newColorSpace;
		}

		/// <summary>
		/// Convienience method for comparing any IColorSpace
		/// </summary>
		/// <param name="compareToValue"></param>
		/// <param name="comparer"></param>
		/// <returns>Single number representing the difference between two colors</returns>
		public static double Compare(this IColorSpace space, IColorSpace compareToValue, IColorSpaceComparison comparer)
		{
			return comparer.Compare(space, compareToValue);
		}
	}

	public interface IColorSpace
	{
		/// <summary>
		/// Initialize settings from an Rgb object
		/// </summary>
		/// <param name="color"></param>
		void Initialize(IRgb color);

		/// <summary>
		/// Convert the color space to Rgb, you should probably using the "To" method instead. Need to figure out a way to "hide" or otherwise remove this method from the public interface.
		/// </summary>
		/// <returns></returns>
		IRgb ToRgb();
	}

	public interface IColorSpaceComparison
	{
		double Compare(IColorSpace a, IColorSpace b);
	}

	public interface IRgb : IColorSpace
	{
		double R { get; set; }
		double G { get; set; }
		double B { get; set; }
		double A { get; set; }

		//byte Red { get; set; }
		//byte Green { get; set; }
		//byte Blue { get; set; }
	}

	public interface IXyz : IColorSpace
	{
		double X { get; set; }
		double Y { get; set; }
		double Z { get; set; }
	}

	public interface IHsl : IColorSpace
	{
		double H { get; set; }
		double S { get; set; }
		double L { get; set; }
		double A { get; set; }
	}

	public interface ILab : IColorSpace
	{
		double L { get; set; }
		double A { get; set; }
		double B { get; set; }
	}

	public interface ILch : IColorSpace
	{
		double L { get; set; }
		double C { get; set; }
		double H { get; set; }
	}

	public interface ILuv : IColorSpace
	{
		double L { get; set; }
		double U { get; set; }
		double V { get; set; }
	}

	public interface IYxy : IColorSpace
	{
		double Y1 { get; set; }
		double X { get; set; }
		double Y2 { get; set; }
	}

	public interface IHsb : IColorSpace
	{
		double H { get; set; }
		double S { get; set; }
		double B { get; set; }
		double A { get; set; }
	}

	public interface IHunterLab : IColorSpace
	{
		double L { get; set; }
		double A { get; set; }
		double B { get; set; }
	}
}


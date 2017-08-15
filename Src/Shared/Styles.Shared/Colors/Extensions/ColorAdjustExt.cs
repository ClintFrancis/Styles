using System;
namespace Styles
{
	public static class ColorAdjustExt
	{
		public static ColorRGB ColorWithMinimumSaturation(this IColorSpace color, double minSaturation)
		{
			var hsb = ColorHSB.FromColor(color.ToRgb());

			if (hsb.S < minSaturation)
			{
				hsb.S = minSaturation;
			}
			return (ColorRGB)hsb.ToRgb();
		}

		public static ColorRGB Mix(this IColorSpace color, ColorRGB mix, double amount = 0.5)
		{
			var rgb = color.ToRgb();
			var nomalizedWeight = (amount > 1) ? 1 : ((amount < 0) ? 0 : amount);

			var red = rgb.R + nomalizedWeight * (mix.R - rgb.R);
			var green = rgb.G + nomalizedWeight * (mix.G - rgb.G);
			var blue = rgb.B + nomalizedWeight * (mix.B - rgb.B);
			var alpha = 1;//rgb.A + nomalizedWeight * (mix.A - rgb.A);

			return new ColorRGB(red, green, blue, alpha);
		}

		public static ColorRGB Tinted(this IColorSpace color, double amount = 0.2)
		{
			return color.Mix(new ColorRGB(1, 1, 1, 1), amount);
		}

		public static ColorRGB Shaded(this IColorSpace color, double amount = 0.2)
		{
			return color.Mix(new ColorRGB(0, 0, 0, 1), amount);
		}

		public static ColorRGB AdjustHue(this IColorSpace color, double amount)
		{
			var rgb = color.ToRgb();
			var hsl = (ColorHSL)ColorHSL.FromColor(rgb);
			hsl.Hue += amount;

			return (ColorRGB)hsl.ToRgb();
		}

		public static ColorRGB WithAlpha(this IColorSpace color, double amount)
		{
			var rgb = (ColorRGB)color.ToRgb();
			rgb.A = amount;
			return rgb;
		}

		public static ColorRGB Complementary(this IColorSpace color)
		{
			return color.AdjustHue(180);
		}

		public static ColorRGB Lightened(this IColorSpace color, double amount = 0.2)
		{
			var hsl = ColorHSL.FromColor(color.ToRgb());
			hsl.L += amount;

			return (ColorRGB)hsl.ToRgb();
		}

		public static ColorRGB Darkened(this IColorSpace color, double amount = 0.2)
		{
			return color.Lightened(-amount);
		}

		public static ColorRGB Saturated(this IColorSpace color, double amount = 0.2)
		{
			var hsl = ColorHSL.FromColor(color.ToRgb());
			hsl.S += amount;

			return (ColorRGB)hsl.ToRgb();
		}

		public static ColorRGB Desaturated(this IColorSpace color, double amount = 0.2)
		{
			return color.Saturated(amount * -1);
		}

		public static ColorRGB GrayScale(this IColorSpace color)
		{
			return color.Desaturated(1);
		}

		public static ColorRGB Inverted(this IColorSpace color)
		{
			var rgb = color.ToRgb();
			return new ColorRGB(
				1 - rgb.R,
				1 - rgb.G,
				1 - rgb.B,
				1
			);
		}

		#region Generic Methods

		public static T ColorWithMinimumSaturation<T>(this IColorSpace color, double minSaturation) where T : IColorSpace, new()
		{
			return color.ColorWithMinimumSaturation(minSaturation).To<T>();
		}

		public static T Mix<T>(this IColorSpace color, ColorRGB mix, float amount = 0.5f) where T : IColorSpace, new()
		{
			return color.Mix(mix, amount).To<T>();
		}

		public static T Tinted<T>(this IColorSpace color, float amount = 0.2f) where T : IColorSpace, new()
		{
			return color.Mix(new ColorRGB(1, 1, 1, 1), amount).To<T>();
		}

		public static T Shaded<T>(this IColorSpace color, float amount = 0.2f) where T : IColorSpace, new()
		{
			return color.Mix(new ColorRGB(0, 0, 0, 1), amount).To<T>();
		}

		public static T AdjustHue<T>(this IColorSpace color, int amount) where T : IColorSpace, new()
		{
			return color.AdjustHue(amount).To<T>();
		}

		public static T Complementary<T>(this IColorSpace color) where T : IColorSpace, new()
		{
			return color.AdjustHue(180).To<T>();
		}

		public static T Lightened<T>(this IColorSpace color, double amount = 0.2) where T : IColorSpace, new()
		{
			return color.Lightened(amount).To<T>();
		}

		public static T Darkened<T>(this IColorSpace color, double amount = 0.2) where T : IColorSpace, new()
		{
			return color.Lightened(-amount).To<T>();
		}

		public static T Saturated<T>(this IColorSpace color, double amount = 0.2) where T : IColorSpace, new()
		{
			return color.Saturated(amount).To<T>();
		}

		public static T Desaturated<T>(this IColorSpace color, double amount = 0.2) where T : IColorSpace, new()
		{
			return color.Desaturated(amount).To<T>();
		}

		public static T GrayScale<T>(this IColorSpace color) where T : IColorSpace, new()
		{
			return color.Desaturated(1).To<T>();
		}

		public static T Inverted<T>(this IColorSpace color) where T : IColorSpace, new()
		{
			return color.Inverted().To<T>();
		}
		#endregion
	}
}

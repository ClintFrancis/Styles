using System;
namespace Styles.Color
{
	public sealed class SquareColorScheme : ColorScheme
	{
		const string SecondaryColorID = "secondary";
		const string TertiaryColorID = "tertiary";
		const string QuaternaryColorID = "quaternary";

		public IRgb SecondaryColor { get { return Colors[SecondaryColorID]; } }
		public IRgb TertiaryColor { get { return Colors[TertiaryColorID]; } }
		public IRgb QuaternaryColor { get { return Colors[QuaternaryColorID]; } }

		SquareColorScheme(Swatch [] colors)
		{
			Type = ColorSchemeType.Square;
            SetColors(colors);
		}

		public override void SetColors(Swatch[] colors)
		{
			var length = colors.Length;
			if (length != 4)
				throw new Exception("Invalid range of colors supplied, square color arrays must contain 4 colors");

			base.SetColors(colors);
		}

		public static SquareColorScheme FromColor(ColorRGB color)
		{
			var hsl = ColorHSL.FromColor(color);

			var divisor = .25d;
			hsl.H -= divisor;

			var swatches = new Swatch[]{
				new Swatch(QuaternaryColorID, ColorHSL.Empty),
				new Swatch(PrimaryColorID, ColorHSL.Empty),
				new Swatch(SecondaryColorID, ColorHSL.Empty),
				new Swatch(TertiaryColorID, ColorHSL.Empty)
			};

			for (var i = 0; i < swatches.Length; i++)
			{
				swatches[i].Color = hsl.ToRgb();
				hsl.H += divisor;
			}

			return new SquareColorScheme(swatches);
		}
	}
}

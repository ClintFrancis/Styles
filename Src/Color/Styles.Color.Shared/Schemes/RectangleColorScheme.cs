using System;
namespace Styles.Color
{
	public sealed class RectangleColorScheme : ColorScheme
	{
		const string SecondaryColorID = "secondary";
		const string TertiaryColorID = "tertiary";
		const string QuaternaryColorID = "quaternary";

		public IRgb SecondaryColor { get { return Colors[SecondaryColorID]; } }
		public IRgb TertiaryColor { get { return Colors[TertiaryColorID]; } }
		public IRgb QuaternaryColor { get { return Colors[QuaternaryColorID]; } }

		RectangleColorScheme(Swatch [] colors)
		{
			Type = ColorSchemeType.Square;
            SetColors(colors);
		}

		public override void SetColors(Swatch[] colors)
		{
			var length = colors.Length;
			if (length != 4)
				throw new Exception("Invalid range of colors supplied, rectangular color arrays must contain 4 colors");

			base.SetColors(colors);
		}

		public static RectangleColorScheme FromColor(ColorRGB color)
		{
			var swatches = new Swatch[]{
				new Swatch(QuaternaryColorID, ColorHSL.Empty),
				new Swatch(PrimaryColorID, ColorHSL.Empty),
				new Swatch(SecondaryColorID, ColorHSL.Empty),
				new Swatch(TertiaryColorID, ColorHSL.Empty)
			};

			var primary = ColorHSL.FromColor(color);
			var h = primary.H;
			var s = primary.S;
			var l = primary.L;

			// quaternary
			var quaternary = new ColorHSL(h, s, l);
			quaternary.H -= 0.166666666666667;
			swatches[0].Color = quaternary;

			// Primary
			swatches[1].Color = primary;

			// secondary
			swatches[2].Color = quaternary.Complementary();

			// tertiary
			swatches[3].Color = primary.Complementary();

			return new RectangleColorScheme(swatches);
		}
	}
}

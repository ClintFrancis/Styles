using System;
namespace Styles.Color
{
	public sealed class SplitComplementaryColorScheme : ColorScheme
	{
		const string SecondaryColorID = "secondary";
		const string TertiaryColorID = "tertiary";

		public IRgb SecondaryColor { get { return Colors[SecondaryColorID]; } }
		public IRgb TertiaryColor { get { return Colors[TertiaryColorID]; } }

		SplitComplementaryColorScheme (Swatch [] colors)
		{
			Type = ColorSchemeType.SplitComplementary;
			SetColors (colors);
		}

		public override void SetColors (Swatch [] colors)
		{
			var length = colors.Length;
			if (length != 3)
				throw new Exception ("Invalid range of colors supplied, split complementary color arrays must contain 3 colors");

			base.SetColors(colors);
		}

		public static SplitComplementaryColorScheme FromColor (ColorRGB color)
		{
			var swatches = new Swatch[]{
				new Swatch(PrimaryColorID, ColorHSL.Empty),
				new Swatch(SecondaryColorID, ColorHSL.Empty),
				new Swatch(TertiaryColorID, ColorHSL.Empty)
			};

			var primary = ColorHSL.FromColor (color);
			var h = primary.H;
			var s = primary.S;
			var l = primary.L;

			swatches[0].Color = primary;

			var secondary = new ColorHSL(h, s, l);
			secondary.H += 0.416666666666667;
			swatches[1].Color = secondary;

			var tertiary = new ColorHSL(h, s, l);
			tertiary.H = h + 0.5 + 0.0833333333333333;
			swatches[2].Color = tertiary;

			return new SplitComplementaryColorScheme (swatches);
		}


	}
}



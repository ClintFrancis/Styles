using System;
namespace Styles.Color
{
	public sealed class ComplementaryColorScheme : ColorScheme
	{
		const string ComplimentaryColorID = "complimentary";

		public IRgb ComplimentaryColor { get { return Colors[ComplimentaryColorID]; } }

		// no public constructor
		ComplementaryColorScheme (Swatch[] colors)
		{
			Type = ColorSchemeType.Complementary;
			SetColors (colors);
		}

		public override void SetColors (Swatch[] colors)
		{
			if (colors.Length != 2)
				throw new Exception ("Invalid range of colors supplied, complementary color arrays must only contain 2 colors");

			base.SetColors(colors);
		}

		public static ComplementaryColorScheme FromColor (ColorRGB color)
		{
			var primary = new Swatch(PrimaryColorID, color);
			var compilmentary = new Swatch(ComplimentaryColorID, color.AdjustHue(180));

			return new ComplementaryColorScheme (new Swatch [] { primary, compilmentary });
		}

	}
}


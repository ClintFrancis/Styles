using System;
namespace Styles.Color
{
	public sealed class MonochromaticColorScheme : ColorScheme
	{
		const string DarkColorID = "dark";
		const string DarkenedColorID = "darkened";
		const string LightColorID = "light";
		const string LightenedColorID = "lightened";

		public IRgb DarkColor { get { return Colors[DarkColorID]; } }
		public IRgb DarkenedColor { get { return Colors[DarkenedColorID]; } }
		public IRgb LightColor { get { return Colors[LightColorID]; } }
		public IRgb LightenedColor { get { return Colors[LightenedColorID]; } }

		MonochromaticColorScheme (Swatch [] colors)
		{
			Type = ColorSchemeType.Monochromatic;
			SetColors (colors);
		}

		public override void SetColors (Swatch [] colors)
		{
			var length = colors.Length;
			if (length != 5)
				throw new Exception ("Invalid range of colors supplied, monochromatic color arrays must be 5 colors in length");

			base.SetColors(colors);
		}

		public static MonochromaticColorScheme FromColor (ColorRGB color)
		{
			//if (flatten) {
			//	var labColors = ColorScheme.GenerateColors (24, 0, .66, .81);
			//	color = color.NearestFlatColor (labColors);
			//}

			var lab = (ColorLAB)ColorLAB.FromColor (color);

			var dark 		= new Swatch(DarkColorID, ColorLAB.ToColor (lab.L - 20, lab.A, lab.B));
			var darkened 	= new Swatch(DarkenedColorID, ColorLAB.ToColor (lab.L - 10, lab.A, lab.B));
			var primary 	= new Swatch(PrimaryColorID, color);
			var lightened 	= new Swatch(LightenedColorID, ColorLAB.ToColor (lab.L + 10, lab.A, lab.B));
			var light 		= new Swatch(LightColorID, ColorLAB.ToColor (lab.L + 20, lab.A, lab.B));

			return new MonochromaticColorScheme (new Swatch [] { dark, darkened, primary, lightened, light });
		}
	}
}


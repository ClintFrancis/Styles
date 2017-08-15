using System;
namespace Styles.Color
{
	public sealed class TriadicColorScheme : ColorScheme
	{
		const string SecondaryColorID = "secondary";
		const string TertiaryColorID = "tertiary";

		public IRgb SecondaryColor { get { return Colors[SecondaryColorID]; } }
		public IRgb TertiaryColor { get { return Colors[TertiaryColorID]; } }

		TriadicColorScheme (Swatch [] colors)
		{
			Type = ColorSchemeType.Triadic;
			SetColors (colors);
		}

		public override void SetColors (Swatch [] colors)
		{
			var length = colors.Length;
			if (length != 3)
				throw new Exception ("Invalid range of colors supplied, triadic color arrays must be contain 3 colors");

			base.SetColors(colors);
		}

		public static TriadicColorScheme FromColor (ColorRGB color)
		{
			var hsl = ColorHSL.FromColor (color);

			var divisor = 1d / 3d;
			hsl.H -= divisor;

			var swatches = new Swatch[]{
				new Swatch(TertiaryColorID, ColorHSL.Empty),
				new Swatch(PrimaryColorID, ColorHSL.Empty),
				new Swatch(SecondaryColorID, ColorHSL.Empty)
			};

            for (var i = 0; i< swatches.Length; i++){
				swatches[i].Color = hsl.ToRgb();
				hsl.H += divisor;     
			}

			return new TriadicColorScheme (swatches);
		}
	}
}


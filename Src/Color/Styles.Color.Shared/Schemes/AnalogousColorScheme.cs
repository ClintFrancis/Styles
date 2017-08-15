using System;
namespace Styles.Color
{
	public sealed class AnalogousColorScheme : ColorScheme
	{
		const string SecondaryColorID = "secondary";
		const string TertiaryColorID = "tertiary";

		public IRgb SecondaryColor { get { return Colors[SecondaryColorID]; } }
		public IRgb TertiaryColor { get { return Colors[TertiaryColorID]; } }

		AnalogousColorScheme(Swatch[] colors)
		{
			Type = ColorSchemeType.Analogous;
			SetColors(colors);
		}

		public override void SetColors (Swatch[] colors)
		{
			var length = colors.Length;
			if (length != 3)
				throw new Exception ("Invalid range of colors supplied, analogous color arrays must contain 3 colors");

			base.SetColors(colors);
		}

		public static AnalogousColorScheme FromColor (ColorRGB color)
		{
			var swatches = new Swatch[]{
				new Swatch(TertiaryColorID, ColorHSL.Empty),
				new Swatch(PrimaryColorID, ColorHSL.Empty),
				new Swatch(SecondaryColorID, ColorHSL.Empty)
			};

			var primary = ColorHSL.FromColor(color);
			var h = primary.H;
			var s = primary.S;
			var l = primary.L;

			// Teriary
			var tertiary = new ColorHSL(h, s, l);
			tertiary.H -= 0.0833333333333333;
			swatches[0].Color = tertiary;

			// Primary
			swatches[1].Color = primary;

			// Secondary
			var secondary = new ColorHSL(h, s, l);
			secondary.H += 0.0833333333333333;
			swatches[2].Color = secondary;

			return new AnalogousColorScheme(swatches);
		}
	}
}


using System;
namespace Styles.Color
{
	public struct Swatch
	{
		public IColorSpace Color { get; set; }
		public string Name { get; set; }

		public Swatch(string name, IColorSpace color)
		{
			Name = name;
			Color = color;
		}
	}

	public static class ColorSwatches
	{
		// Light colors
		public static readonly ColorRGB FlatBlack 		= ColorRGB.FromRGB (43, 43, 43); 
		public static readonly ColorRGB FlatBlue 		= ColorRGB.FromRGB (80, 102, 161);
		public static readonly ColorRGB FlatBrown 		= ColorRGB.FromRGB (94, 69, 52);
		public static readonly ColorRGB FlatCoffee 		= ColorRGB.FromRGB (163, 134, 113);
		public static readonly ColorRGB FlatForestGreen = ColorRGB.FromRGB(52, 94, 65);
		public static readonly ColorRGB FlatGray 		= ColorRGB.FromRGB(149, 165, 166);
		public static readonly ColorRGB FlatGreen 		= ColorRGB.FromRGB(47, 204, 112);
		public static readonly ColorRGB FlatLime 		= ColorRGB.FromRGB (166, 199, 60); 
		public static readonly ColorRGB FlatMagenta 	= ColorRGB.FromRGB (155, 89, 181);
		public static readonly ColorRGB FlatMaroon 		= ColorRGB.FromRGB (120, 48, 42);
		public static readonly ColorRGB FlatMint 		= ColorRGB.FromRGB (26, 189, 156);
		public static readonly ColorRGB FlatNavyBlue 	= ColorRGB.FromRGB (52, 73, 94);
		public static readonly ColorRGB FlatOrange 		= ColorRGB.FromRGB (229, 125, 34);
		public static readonly ColorRGB FlatPink 		= ColorRGB.FromRGB (245, 125, 197);
		public static readonly ColorRGB FlatPlum 		= ColorRGB.FromRGB (94, 52, 94);
		public static readonly ColorRGB FlatPowderBlue 	= ColorRGB.FromRGB (184, 202, 242);
		public static readonly ColorRGB FlatPurple 		= ColorRGB.FromRGB (116, 94, 196);
		public static readonly ColorRGB FlatRed 		= ColorRGB.FromRGB (232, 78, 60);
		public static readonly ColorRGB FlatSand 		= ColorRGB.FromRGB (240, 222, 180);
		public static readonly ColorRGB FlatSkyBlue 	= ColorRGB.FromRGB (53, 153, 219);
		public static readonly ColorRGB FlatTeal 		= ColorRGB.FromRGB (59, 112, 130);
		public static readonly ColorRGB FlatWatermelon 	= ColorRGB.FromRGB (240, 113, 121);
		public static readonly ColorRGB FlatWhite 		= ColorRGB.FromRGB (237, 241, 242);
		public static readonly ColorRGB FlatYellow 		= ColorRGB.FromRGB (255, 205, 3);

		// Dark colors
		public static readonly ColorRGB FlatBlackDark 		= ColorRGB.FromRGB(38, 38, 38);		
		public static readonly ColorRGB FlatBlueDark 		= ColorRGB.FromRGB(57, 77, 130);	
		public static readonly ColorRGB FlatBrownDark 		= ColorRGB.FromRGB(79, 58, 43);		
		public static readonly ColorRGB FlatCoffeeDark 		= ColorRGB.FromRGB(143, 114, 94);	
		public static readonly ColorRGB FlatForestGreenDark = ColorRGB.FromRGB(44, 79, 53);		
		public static readonly ColorRGB FlatGrayDark 		= ColorRGB.FromRGB(126, 139, 140);	
		public static readonly ColorRGB FlatGreenDark 		= ColorRGB.FromRGB(38, 173, 95);	
		public static readonly ColorRGB FlatLimeDark 		= ColorRGB.FromRGB(143, 176, 33);	
		public static readonly ColorRGB FlatMagentaDark 	= ColorRGB.FromRGB(142, 68, 173);	
		public static readonly ColorRGB FlatMaroonDark 		= ColorRGB.FromRGB(102, 37, 33);	
		public static readonly ColorRGB FlatMintDark 		= ColorRGB.FromRGB(22, 161, 133);	
		public static readonly ColorRGB FlatNavyBlueDark 	= ColorRGB.FromRGB(43, 61, 79);		
		public static readonly ColorRGB FlatOrangeDark 		= ColorRGB.FromRGB(212, 85, 0);		
		public static readonly ColorRGB FlatPinkDark 		= ColorRGB.FromRGB(212, 91, 157);	
		public static readonly ColorRGB FlatPlumDark 		= ColorRGB.FromRGB(79, 43, 79);		
		public static readonly ColorRGB FlatPowderBlueDark 	= ColorRGB.FromRGB(154, 172, 214);	
		public static readonly ColorRGB FlatPurpleDark 		= ColorRGB.FromRGB(106, 83, 189);	
		public static readonly ColorRGB FlatRedDark 		= ColorRGB.FromRGB(191, 57, 42);	
		public static readonly ColorRGB FlatSandDark 		= ColorRGB.FromRGB (214, 195, 150);
		public static readonly ColorRGB FlatSkyBlueDark 	= ColorRGB.FromRGB(41, 128, 186);
		public static readonly ColorRGB FlatTealDark 		= ColorRGB.FromRGB(53, 98, 115);
		public static readonly ColorRGB FlatWatermelonDark 	= ColorRGB.FromRGB(217, 85, 89);
		public static readonly ColorRGB FlatWhiteDark 		= ColorRGB.FromRGB(189, 195, 199);
		public static readonly ColorRGB FlatYellowDark 		= ColorRGB.FromRGB(255, 170, 0);

		public static ColorRGB [] FlatColors = new [] { FlatBlack, FlatBlackDark, FlatBlue, FlatBlueDark, FlatBrown, FlatBrownDark, FlatCoffee, FlatCoffeeDark, FlatForestGreen, FlatForestGreenDark, FlatGray, FlatGrayDark, FlatGreen, FlatGreenDark, FlatLime, FlatLimeDark, FlatMagenta, FlatMagentaDark, FlatMaroon, FlatMaroonDark, FlatMint, FlatMintDark, FlatNavyBlue, FlatNavyBlueDark, FlatOrange, FlatOrangeDark, FlatPink, FlatPinkDark, FlatPlum, FlatPlumDark, FlatPowderBlue, FlatPowderBlueDark, FlatPurple, FlatPurpleDark, FlatRed, FlatRedDark, FlatSand, FlatSandDark, FlatSkyBlue, FlatSkyBlueDark, FlatTeal, FlatTealDark, FlatWatermelon, FlatWatermelonDark, FlatWhite, FlatWhiteDark, FlatYellow, FlatYellowDark };


		// Secondard Colors TDB
		public static readonly ColorRGB Red 		= ColorRGB.FromRGB(241, 69, 61);
		public static readonly ColorRGB Pink 		= ColorRGB.FromRGB(230, 37, 101);
		public static readonly ColorRGB Purple 		= ColorRGB.FromRGB(155, 47, 174);
		public static readonly ColorRGB DeepPurple 	= ColorRGB.FromRGB(103, 63, 180);
		public static readonly ColorRGB Indigo 		= ColorRGB.FromRGB(64, 84, 178);
		public static readonly ColorRGB Blue 		= ColorRGB.FromRGB(43, 152, 240);
		public static readonly ColorRGB LightBlue 	= ColorRGB.FromRGB(30, 170, 241);
		public static readonly ColorRGB Cyan 		= ColorRGB.FromRGB(31, 188, 210);
		public static readonly ColorRGB Teal 		= ColorRGB.FromRGB(21, 149, 136);
		public static readonly ColorRGB Green 		= ColorRGB.FromRGB(80, 174, 85);
		public static readonly ColorRGB LightGreen 	= ColorRGB.FromRGB(140, 193, 82);
		public static readonly ColorRGB Lime 		= ColorRGB.FromRGB(205, 218, 73);
		public static readonly ColorRGB Yellow 		= ColorRGB.FromRGB(254, 233, 78);
		public static readonly ColorRGB Amber 		= ColorRGB.FromRGB(253, 192, 47);
		public static readonly ColorRGB Orange 		= ColorRGB.FromRGB(253, 151, 39);
		public static readonly ColorRGB DeepOrange 	= ColorRGB.FromRGB (252, 88, 48);
		public static readonly ColorRGB Brown 		= ColorRGB.FromRGB(120, 85, 73);
		public static readonly ColorRGB Grey 		= ColorRGB.FromRGB(158, 158, 158);
		public static readonly ColorRGB BlueGrey 	= ColorRGB.FromRGB(97, 125, 138);

		public static ColorRGB [] FlatColors2 = new ColorRGB []{
			   Red.Lightened(),          Red.Darkened(),
			   Pink.Lightened(),         Pink.Darkened(),
			   Purple.Lightened(),       Purple.Darkened(),
			   DeepPurple.Lightened(),   DeepPurple.Darkened(),
			   Indigo.Lightened(),       Indigo.Darkened(),
			   Blue.Lightened(),         Blue.Darkened(),
			   LightBlue.Lightened(),    LightBlue.Darkened(),
			   Cyan.Lightened(),         Cyan.Darkened(),
			   Teal.Lightened(),         Teal.Darkened(),
			   Green.Lightened(),        Green.Darkened(),
			   LightGreen.Lightened(),   LightGreen.Darkened(),
			   Lime.Lightened(),         Lime.Darkened(),
			   Yellow.Lightened(),       Yellow.Darkened(),
			   Amber.Lightened(),        Amber.Darkened(),
			   Orange.Lightened(),       Orange.Darkened(),
			   DeepOrange.Lightened(),   DeepOrange.Darkened(),
			   Brown.Lightened(),        Brown.Darkened(),
			   Grey.Lightened(),         Grey.Darkened(),
			   BlueGrey.Lightened(),     BlueGrey.Darkened()
		};
	}
}


using System;
using NUnit.Framework;
using Styles;
using Styles.Color;

namespace ColorTests
{
	[TestFixture()]
	public class ColorSchemeTests
	{
		[Test()]
		public void RectangularColorSchemeTest()
		{
			var rgb = ColorRGB.FromRGB(240, 76, 59);
			var rectangleScheme = RectangleColorScheme.FromColor(rgb);

			// color 1
			var primary = (ColorRGB)rectangleScheme.PrimaryColor;
			var secondary = (ColorRGB)rectangleScheme.SecondaryColor;
			var tertiary = (ColorRGB)rectangleScheme.TertiaryColor;
			var quaternary = (ColorRGB)rectangleScheme.QuaternaryColor;

			Assert.AreEqual(primary.Red, 240);
			Assert.AreEqual(primary.Green, 76);
			Assert.AreEqual(primary.Blue, 59);

			Assert.AreEqual(secondary.Red, 59);
			Assert.AreEqual(secondary.Green, 240);
			Assert.AreEqual(secondary.Blue, 76);

			Assert.AreEqual(tertiary.Red, 59);
			Assert.AreEqual(tertiary.Green, 223);
			Assert.AreEqual(tertiary.Blue, 240);

			Assert.AreEqual(quaternary.Red, 240);
			Assert.AreEqual(quaternary.Green, 59);
			Assert.AreEqual(quaternary.Blue, 223);
		}
	}
}

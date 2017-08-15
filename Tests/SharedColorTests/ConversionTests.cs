using System;
using NUnit.Framework;
using Styles;

namespace ColorTests
{
	[TestFixture()]
	public class ConversionTests
	{
		[Test()]
		public void RGBtoHSLConversion()
		{
			var rgb = ColorRGB.FromHex("#6653B2");
			var hsl = (ColorHSL)ColorHSL.FromColor(rgb);
			var rgbConverter = (ColorRGB)hsl.ToRgb();

			// HSL
			Assert.AreEqual(hsl.Hue, 252);
			Assert.AreEqual(hsl.Saturation, 38.15);
			Assert.AreEqual(hsl.Luminance, 51.18);

			// RGB Converted
			Assert.AreEqual(rgbConverter.Red, 102);
			Assert.AreEqual(rgbConverter.Green, 83);
			Assert.AreEqual(rgbConverter.Blue, 178);
		}

		[Test()]
		public void RGBtoHSBConversion()
		{
			var rgb = ColorRGB.FromHex("#6653B2");
			var hsb = (ColorHSB)ColorHSB.FromColor(rgb);
			var rgbConverted = (ColorRGB)hsb.ToRgb();

			// HSL
			Assert.AreEqual(hsb.Hue, 252);
			Assert.AreEqual(hsb.Saturation, 53.37);
			Assert.AreEqual(hsb.Brightness, 69.8);

			// RGB Converted
			Assert.AreEqual(rgbConverted.Red, 102);
			Assert.AreEqual(rgbConverted.Green, 83);
			Assert.AreEqual(rgbConverted.Blue, 178);
		}

		[Test()]
		public void RGBtoXYZConversion()
		{
			var rgb = ColorRGB.FromHex("#6653B2");
			var xyz = (ColorXYZ)ColorXYZ.FromColor(rgb);
			var rgbConverted = (ColorRGB)xyz.ToRgb();

			// LAB
			var x = Math.Round(xyz.X, 2);
			var y = Math.Round(xyz.Y, 2);
			var z = Math.Round(xyz.Z, 2);

			Assert.AreEqual(x, 16.61);
			Assert.AreEqual(y, 12.23);
			Assert.AreEqual(z, 43.6);

			// RGB Converted
			Assert.AreEqual(rgbConverted.Red, 102);
			Assert.AreEqual(rgbConverted.Green, 83);
			Assert.AreEqual(rgbConverted.Blue, 178);
		}

		[Test()]
		public void RGBtoLABConversion()
		{
			var rgb = ColorRGB.FromHex("#6653B2");
			var lab = (ColorLAB)ColorLAB.FromColor(rgb);
			var rgbConverted = (ColorRGB)lab.ToRgb();

			// LAB
			var l = Math.Truncate(100 * lab.L) / 100;
			var a = Math.Truncate(100 * lab.A) / 100;
			var b = Math.Truncate(100 * lab.B) / 100;
			
			Assert.AreEqual(l, 41.57);
			Assert.AreEqual(a, 31.37);
			Assert.AreEqual(b, -52.39); // Differs from Colorize.org which shows -48.16

			// RGB Converted
			Assert.AreEqual(rgbConverted.Red, 102);
			Assert.AreEqual(rgbConverted.Green, 83);
			Assert.AreEqual(rgbConverted.Blue, 178);
		}
	}
}

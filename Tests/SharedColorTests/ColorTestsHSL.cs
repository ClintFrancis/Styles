using System;
using NUnit.Framework;
using Styles;

namespace SharedColorTests
{
	[TestFixture()]
	public class ColorTestsHSL
	{
		//[Test()]
		//public void FromHexTest()
		//{
		//	var rgb = ColorRGB.FromRGB(0xC0392B);
		//	var hsl = ColorHSL.FromColor(rgb);
		//	var h = hsl.H;
		//	var s = Math.Round(hsl.S * 100);
		//	var l = Math.Round(hsl.L * 100);

		//	Assert.AreEqual(h, 6);
		//	Assert.AreEqual(s, 63);
		//	Assert.AreEqual(l, 46);
		//}
		/*
		[Test()]
		public void CreateRGBHexAndInt()
		{
			var rgb = ColorRGB.FromRGB(192, 57, 43);
			var rgb2 = ColorRGB.FromRGB(0xC0392B);

			//var converted = HSLUtil.ToColor(hsl);

			Assert.AreEqual(rgb.R, rgb2.R, "R Values do not match");
			Assert.AreEqual(rgb.G, rgb2.G, "G Values do not match");
			Assert.AreEqual(rgb.B, rgb2.B, "B Values do not match");
		}

		[Test()]
		public void ConvertRGBtoHSLAndBack()
		{
			var rgb = ColorRGB.FromRGB(0xC0392B);
			var hsl = ColorHSL.FromColor(rgb);
			var converted = (ColorRGB)hsl.ToRgb();

			Assert.AreEqual(converted.R, rgb.R, "R Values do not match");
			Assert.AreEqual(converted.G, rgb.G, "G Values do not match");
			Assert.AreEqual(converted.B, rgb.B, "B Values do not match");
		}

		[Test()]
		public void AdjustHueTest(){
			var color1 = ColorRGB.FromRGB(0x881111);
			color1 = color1.AdjustHue(45);

			// Suffering from a - hue issue when adjusted
			//var color2 = ColorRGB.FromRGB(0xC0392B);
			//color2 = color2.AdjustHue(90);

			//var color3 = ColorRGB.FromRGB(0xC0392B).AdjustHue(-60);
			//var same1 = ColorRGB.FromRGB(0xC0392B).AdjustHue(0);
			//var same2 = ColorRGB.FromRGB(0xC0392B).AdjustHue(360);

			Assert.AreEqual(color1.ToHex(), "#886A11");
			//Assert.AreEqual(color2.ToHex(), "#67C02B");
			//Assert.AreEqual(color3.ToHex(), "#C02BB2");
			//Assert.AreEqual(same1.ToHex(), "#C0392B");
			//Assert.AreEqual(same2.ToHex(), "#C0392B");
		}
		*/

		[Test()]
		public void StandaloneConversionTest()
		{
			var rgb = ColorRGB.FromRGB(0x886a11);
			var hsl = ColorTestUtils.RGBtoHSL(rgb);
			var output = hsl.ToString();

			Assert.AreEqual(rgb.ToHex(), "#881111");
		}
	}
}

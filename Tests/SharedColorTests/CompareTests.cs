using System;
using NUnit.Framework;
using Styles;

namespace ColorTests
{
	[TestFixture()]
	public class CompareTests
	{
		[Test()]
		public void RGBIsBlackOrWhiteColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbLight = ColorRGB.FromRGB(247, 242, 242);
			var rgbDark = ColorRGB.FromRGB(5, 5, 5);

			Assert.AreEqual(rgb.IsBlackOrWhite(), false);
			Assert.AreEqual(rgbLight.IsBlackOrWhite(), true);
			Assert.AreEqual(rgbDark.IsBlackOrWhite(), true);
			Assert.AreEqual(ColorRGB.Black.IsBlackOrWhite(), true);
			Assert.AreEqual(ColorRGB.White.IsBlackOrWhite(), true);
		}

		[Test()]
		public void RGBIsContrastingColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbLight = ColorRGB.FromRGB(247, 242, 242);
			var rgbDark = ColorRGB.FromRGB(5, 5, 5);

			Assert.AreEqual(rgb.IsContrastingColor(rgb), false);
			Assert.AreEqual(rgb.IsContrastingColor(ColorRGB.White), true);
			
			Assert.AreEqual(rgbLight.IsContrastingColor(ColorRGB.White), false);
			Assert.AreEqual(rgbLight.IsContrastingColor(ColorRGB.Black), true);

			Assert.AreEqual(rgbDark.IsContrastingColor(ColorRGB.White), true);
			Assert.AreEqual(rgbDark.IsContrastingColor(ColorRGB.Black), false);
		}

		[Test()]
		public void RGBIsDarkColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbLight = ColorRGB.FromRGB(247, 242, 242);
			var rgbDark = ColorRGB.FromRGB(5, 5, 5);

			Assert.AreEqual(rgb.IsDarkColor(), true);
			Assert.AreEqual(rgbLight.IsDarkColor(), false);
			Assert.AreEqual(rgbDark.IsDarkColor(), true);
			Assert.AreEqual(ColorRGB.Black.IsDarkColor(), true);
			Assert.AreEqual(ColorRGB.White.IsDarkColor(), false);
		}
	}
}

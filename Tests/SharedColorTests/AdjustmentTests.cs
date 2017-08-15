using System;
using NUnit.Framework;
using Styles;

namespace ColorTests
{
	[TestFixture()]
	public class AdjustmentTests
	{
		[Test()]
		public void HSLHueShiftTest(){
			var hsl = ColorRGB.FromRGB(178, 83, 83).To<ColorHSL>();
			hsl.Hue += 40;

			var rgb = (ColorRGB)hsl.ToRgb();

			Assert.AreEqual(rgb.Red, 178);
			Assert.AreEqual(rgb.Green, 146);
			Assert.AreEqual(rgb.Blue, 83);
		}

		[Test()]
		public void RGBHueShiftTest()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			rgb = rgb.AdjustHue(40);

			Assert.AreEqual(rgb.Red, 178);
			Assert.AreEqual(rgb.Green, 146);
			Assert.AreEqual(rgb.Blue, 83);
		}

		[Test()]
		public void RGBColorWithMinimumSaturation()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			rgb = rgb.ColorWithMinimumSaturation(.7);

			Assert.AreEqual(rgb.Red, 178);
			Assert.AreEqual(rgb.Green, 53);
			Assert.AreEqual(rgb.Blue, 53);
		}

		[Test()]
		public void RGBComplimentaryColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbCompliment = rgb.Complementary();

			Assert.AreEqual(rgbCompliment.Red, 83);
			Assert.AreEqual(rgbCompliment.Green, 178);
			Assert.AreEqual(rgbCompliment.Blue, 178);
		}

		[Test()]
		public void RGBDarkenedColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbDarkened = rgb.Darkened(.2);

			Assert.AreEqual(rgbDarkened.Red, 110);
			Assert.AreEqual(rgbDarkened.Green, 49);
			Assert.AreEqual(rgbDarkened.Blue, 49);
		}

		[Test()]
		public void RGBLightenedColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbLight = rgb.Lightened(.2);

			Assert.AreEqual(rgbLight.Red, 210);
			Assert.AreEqual(rgbLight.Green, 153);
			Assert.AreEqual(rgbLight.Blue, 153);
		}

		[Test()]
		public void RGBDesaturatedColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbDesaturated = rgb.Desaturated(.2);

			Assert.AreEqual(rgbDesaturated.Red, 153);
			Assert.AreEqual(rgbDesaturated.Green, 108);
			Assert.AreEqual(rgbDesaturated.Blue, 108);
		}

		[Test()]
		public void RGBSaturatedColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbSaturated = rgb.Saturated(.2);

			Assert.AreEqual(rgbSaturated.Red, 203);
			Assert.AreEqual(rgbSaturated.Green, 58);
			Assert.AreEqual(rgbSaturated.Blue, 58);
		}

		[Test()]
		public void RGBGrayScaleColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbGrayScale = rgb.GrayScale();

			Assert.AreEqual(rgbGrayScale.Red, 130);
			Assert.AreEqual(rgbGrayScale.Green, 130);
			Assert.AreEqual(rgbGrayScale.Blue, 130);
		}

		[Test()]
		public void RGBInvertedColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbInverted = rgb.Inverted();

			Assert.AreEqual(rgbInverted.Red, 77);
			Assert.AreEqual(rgbInverted.Green, 172);
			Assert.AreEqual(rgbInverted.Blue, 172);
		}

		[Test()]
		public void RGBMixColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var red = ColorRGB.FromRGB(255, 0, 0);
			var green = ColorRGB.FromRGB(0, 255, 0);
			var blue = ColorRGB.FromRGB(0, 0, 255);

			// Mix 1
			var mixRed = rgb.Mix(red, .5);
			Assert.AreEqual(mixRed.Red, 216);
			Assert.AreEqual(mixRed.Green, 42);
			Assert.AreEqual(mixRed.Blue, 42);

			// Mix 2
			var mixGreen = rgb.Mix(green, .5);
			Assert.AreEqual(mixGreen.Red, 89);
			Assert.AreEqual(mixGreen.Green, 169);
			Assert.AreEqual(mixGreen.Blue, 42);

			// Mix 3
			var mixBlue = rgb.Mix(blue, .5);
			Assert.AreEqual(mixBlue.Red, 89);
			Assert.AreEqual(mixBlue.Green, 42);
			Assert.AreEqual(mixBlue.Blue, 169);
		}

		[Test()]
		public void RGBShadedColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbShaded = rgb.Shaded(.2);

			Assert.AreEqual(rgbShaded.Red, 142);
			Assert.AreEqual(rgbShaded.Green, 66);
			Assert.AreEqual(rgbShaded.Blue, 66);
		}

		[Test()]
		public void RGBTintedColor()
		{
			var rgb = ColorRGB.FromRGB(178, 83, 83);
			var rgbTinted = rgb.Tinted(.2);

			Assert.AreEqual(rgbTinted.Red, 193);
			Assert.AreEqual(rgbTinted.Green, 117);
			Assert.AreEqual(rgbTinted.Blue, 117);
		}
	}
}

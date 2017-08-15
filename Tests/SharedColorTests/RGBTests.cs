using NUnit.Framework;
using System;
using Styles;

namespace ColorTests
{
	[TestFixture()]
	public class RGBTests
	{
		[Test()]
		public void BasicRGBHexConversion()
		{
			var color1 = new ColorRGB(1, 0, 0);
			var hex1 = color1.ToHex();

			var color2 = new ColorRGB(0, 1, 0);
			var hex2 = color2.ToHex();

			var color3 = new ColorRGB(0, 0, 1);
			var hex3 = color3.ToHex();

			Assert.AreEqual(hex1, "#FF0000");
			Assert.AreEqual(hex2, "#00FF00");
			Assert.AreEqual(hex3, "#0000FF");
		}

		[Test()]
		public void BasicHexToRGBConversion()
		{
			var hex1 = "#886A11";
			var hex2 = "#55B0A5";
			var hex3 = "#6653B2";

			var color1 = ColorRGB.FromHex(hex1);
			var color2 = ColorRGB.FromHex(hex2);
			var color3 = ColorRGB.FromHex(hex3);

			Assert.AreEqual(color1.ToHex(), hex1);
			Assert.AreEqual(color2.ToHex(), hex2);
			Assert.AreEqual(color3.ToHex(), hex3);
		}

		[Test()]
		public void BasicHexToRGBIntConversion()
		{
			var hex1 = "#886A11";
			var hex2 = "#55B0A5";
			var hex3 = "#6653B2";

			var color1 = ColorRGB.FromHex(hex1);
			var color2 = ColorRGB.FromHex(hex2);
			var color3 = ColorRGB.FromHex(hex3);

			Assert.AreEqual(color1.ToValueRGB(), 8940049);
			Assert.AreEqual(color1.ToValueARGB(), 4287130129);

			Assert.AreEqual(color2.ToValueRGB(), 5615781);
			Assert.AreEqual(color2.ToValueARGB(), 4283805861);
			
			Assert.AreEqual(color3.ToValueRGB(), 6706098);
			Assert.AreEqual(color3.ToValueARGB(), 4284896178);
		}

		[Test()]
		public void BasicRGBIntToHexConversion()
		{
			var colorRgb1 = ColorRGB.FromRGB(8940049);
			var colorArgb1 = ColorRGB.FromARGB(4287130129);

			var colorRgb2 = ColorRGB.FromRGB(5615781);
			var colorArgb2 = ColorRGB.FromARGB(4283805861);

			var colorRgb3 = ColorRGB.FromRGB(6706098);
			var colorArgb3 = ColorRGB.FromARGB(4284896178);


			Assert.AreEqual(colorRgb1.ToHex(), "#886A11");
			Assert.AreEqual(colorArgb1.ToHex(), "#886A11");

			Assert.AreEqual(colorRgb2.ToHex(), "#55B0A5");
			Assert.AreEqual(colorArgb2.ToHex(), "#55B0A5");

			Assert.AreEqual(colorRgb3.ToHex(), "#6653B2");
			Assert.AreEqual(colorArgb3.ToHex(), "#6653B2");
		}
	}
}

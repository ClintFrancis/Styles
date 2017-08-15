using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Styles;

namespace ColorStyleDemo.iOS
{
	public partial class ColorAdjustmentViewController : UIViewController
	{
		float swatchSize = 100;

		public ColorAdjustmentViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			var padding = 40;
			swatchSize = (float)(View.Frame.Width - padding * 2) / 3f;

			var rowRect = new CGRect (40, swatchSize, swatchSize, swatchSize);
			var primaryColor = ColorRGB.FromHex ("#F04C3B");

			// Row 
			var primary = new ColorSwatchView (rowRect, primaryColor, "Primary\n" + primaryColor.ToHex ());
			Add (primary);

			rowRect.Offset (swatchSize, 0);
			var colorLightened = primaryColor.Lightened ();
			var lighter = new ColorSwatchView (rowRect, colorLightened, "Lighter\n" + colorLightened.ToHex ());
			Add (lighter);

			rowRect.Offset (swatchSize, 0);
			var colorDarkened = primaryColor.Darkened ();
			var darker = new ColorSwatchView (rowRect, colorDarkened, "Darker\n" + colorDarkened.ToHex ());
			Add (darker);

			// Row 2
			rowRect.Offset (-swatchSize * 2, swatchSize);
			var colorSaturated = primaryColor.Saturated ();
			var saturated = new ColorSwatchView (rowRect, colorSaturated, "Saturated\n" + colorSaturated.ToHex ());
			Add (saturated);

			rowRect.Offset (swatchSize, 0);
			var colorDesaturated = primaryColor.Desaturated ();
			var desaturated = new ColorSwatchView (rowRect, colorDesaturated, "Desaturated\n" + colorDesaturated.ToHex ());
			Add (desaturated);

			rowRect.Offset (swatchSize, 0);
			var colorGray = primaryColor.GrayScale ();
			var grayscaled = new ColorSwatchView (rowRect, colorGray, "Grayscale\n" + colorGray.ToHex ());
			Add (grayscaled);

			// Row 32
			rowRect.Offset (-swatchSize * 2, swatchSize);
			var colorAdjusted = primaryColor.AdjustHue (45);
			var adjusted = new ColorSwatchView (rowRect, colorAdjusted, "Adjusted\n" + colorAdjusted.ToHex ());
			Add (adjusted);

			rowRect.Offset (swatchSize, 0);
			var colorCompliment = primaryColor.Complementary ();
			var compliment = new ColorSwatchView (rowRect, colorCompliment, "Compliment\n" + colorCompliment.ToHex ());
			Add (compliment);

			rowRect.Offset (swatchSize, 0);
			var colorInverted = primaryColor.Inverted ();
			var invert = new ColorSwatchView (rowRect, colorInverted, "Invert\n" + colorInverted.ToHex ());
			Add (invert);

			// Row3
			rowRect.Offset (-swatchSize * 2, swatchSize);
			var colorMixBlue = primaryColor.Mix (ConvertColor (UIColor.Blue));
			var mixBlue = new ColorSwatchView (rowRect, colorMixBlue, "Mix Blue\n" + colorMixBlue.ToHex ());
			Add (mixBlue);

			rowRect.Offset (swatchSize, 0);
			var colorMixGreen = primaryColor.Mix (ConvertColor (UIColor.Green));
			var mixGreen = new ColorSwatchView (rowRect, colorMixGreen, "Mix Green\n" + colorMixGreen.ToHex ());
			Add (mixGreen);

			rowRect.Offset (swatchSize, 0);
			var colorMixYellow = primaryColor.Mix (ConvertColor (UIColor.Yellow));
			var mixYellow = new ColorSwatchView (rowRect, colorMixYellow, "Mix Yellow\n" + colorMixYellow.ToHex ());
			Add (mixYellow);

			// Row 5 3
			rowRect.Offset (-swatchSize * 2, swatchSize);
			var colorTinted = primaryColor.Tinted ();
			var tint = new ColorSwatchView (rowRect, colorTinted, "Tint\n" + colorTinted.ToHex ());
			Add (tint);

			rowRect.Offset (swatchSize, 0);
			var colorShaded = primaryColor.Shaded ();
			var shade = new ColorSwatchView (rowRect, colorShaded, "Shade\n" + colorShaded.ToHex ());
			Add (shade);
		}

		private ColorRGB ConvertColor (UIColor target)
		{
			nfloat r, g, b, a;
			target.GetRGBA (out r, out g, out b, out a);
			return new ColorRGB ((float)r, (float)g, (float)b, (float)a);

		}
	}
}
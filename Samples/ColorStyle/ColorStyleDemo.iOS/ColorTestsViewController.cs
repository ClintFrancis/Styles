using Foundation;
using System;
using UIKit;
using Styles;
using System.Text;
using System.Collections.Generic;

namespace ColorStyleDemo.iOS
{
	public class TestColor
	{
		public string Name { get; set; }
		public string Value { get; set; }
		public string NativeValue { get; set; }
	}

	public partial class ColorTestsViewController : UIViewController
	{
		public ColorTestsViewController (IntPtr handle) : base (handle)
		{
		}

		ColorRGB testColor;

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			testColor = ColorRGB.FromHex ("#745EC4");

			var builder = new StringBuilder ();
			builder.AppendLine ("MAIN COLOR: " + testColor.ToHex ());
			builder.AppendLine ();

			var colors = new TestColor []{
				TestColorRGB (),
				TestColorLAB (),
				TestColorHSB (),
				TestColorHSL (),
				TestColorXYZ ()
			};

			var matching = new StringBuilder ();
			var mismatching = new StringBuilder ();

			for (int i = 0; i < colors.Length; i++) {
				var output = colors [i].Name + " " + colors [i].Value + " " + colors [i].NativeValue;
				if (colors [i].Value == testColor.ToHex ()) {
					matching.AppendLine (output);
				} else {
					mismatching.AppendLine (output);
				}
			}

			// Display Matching
			matching.AppendLine ();
			matching.AppendLine ("MISMATCHED:");
			matching.AppendLine (mismatching.ToString ());

			textDisplay.Text = matching.ToString ();
		}

		private TestColor TestColorRGB ()
		{
			var color = ColorRGB.Empty;
			color.Initialize (testColor);

			var color2 = (ColorRGB)color.ToRgb ();
			var nativeValue = string.Format ("R:{0:0.0#} G:{1:0.0#} B:{2:0.0#}", color.R, color.G, color.B);

			return new TestColor { Name = "RGB", Value = color2.ToHex (), NativeValue = nativeValue };
		}

		private TestColor TestColorLAB ()
		{
			var color = ColorLAB.FromColor (testColor);

			var color2 = (ColorRGB)color.ToRgb (); ;
			var nativeValue = string.Format ("L:{0:0.0#} A:{1:0.0#} B:{2:0.0#}", color.L, color.A, color.B);

			return new TestColor { Name = "LAB", Value = color2.ToHex (), NativeValue = nativeValue };
		}

		private TestColor TestColorHSB ()
		{
			var color = ColorHSB.FromColor (testColor);

			var color2 = (ColorRGB)color.ToRgb (); ;
			var nativeValue = string.Format ("H:{0:0.0#} S:{1:0.0#} B:{2:0.0#}", color.H, color.S, color.B);

			return new TestColor { Name = "HSB", Value = color2.ToHex (), NativeValue = nativeValue };
		}

		private TestColor TestColorHSL ()
		{
			var color = ColorHSL.FromColor (testColor);
			var color2 = (ColorRGB)color.ToRgb ();

			// debug line
			//color2 = (ColorRGB)ColorHSL.ToColor (253, .464, .569);

			var nativeValue = string.Format ("H:{0:0.00#} S:{1:0.00#} L:{2:0.00#}", color.H, color.S, color.L);

			return new TestColor { Name = "HSL", Value = color2.ToHex (), NativeValue = nativeValue };
		}

		private TestColor TestColorXYZ ()
		{
			var color = ColorXYZ.FromColor (testColor);
			var color2 = (ColorRGB)color.ToRgb (); ;
			var nativeValue = string.Format ("X:{0:0.00#} Y:{1:0.00#} Z:{2:0.00#}", color.X, color.Y, color.Z);

			return new TestColor { Name = "XYZ", Value = color2.ToHex (), NativeValue = nativeValue };
		}
	}
}
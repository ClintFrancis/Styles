using System;
using UIKit;
using CoreGraphics;

namespace TextStyleDemo.iOS
{
	public sealed class SwapButton : UIButton
	{
		public static float SIZE = 44f;

		public SwapButton ()
		{
			SetImage (IconGraphics.ImageOfRefreshIcon (Colors.Grey), UIControlState.Normal);
			SetImage (IconGraphics.ImageOfRefreshIcon (Colors.SpotColor), UIControlState.Highlighted);
		}
	}

	public sealed class NextButton : UIButton
	{
		public static float SIZE = 44f;

		public NextButton ()
		{
			SetImage (IconGraphics.ImageOfArrowIcon (Colors.Grey), UIControlState.Normal);
			SetImage (IconGraphics.ImageOfArrowIcon (Colors.SpotColor), UIControlState.Highlighted);
		}
	}
}


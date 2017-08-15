using System;
using UIKit;
using System.IO;
using System.Collections.Generic;
using CoreGraphics;
using System.Diagnostics;
using Styles.Text;
using Styles;

namespace TextStyleDemo.iOS
{
	public partial class ViewController : UIViewController
	{
		const string headingOne = @"The difference between";
		const string headingTwo = @"Ordinary & Extraordinary";
		const string headingThree = @"Is that little <spot>extra</spot>";
		const string textBody = @"Geometry can produce legible letters but <i>art alone</i> makes them beautiful.<br/><br/>Art begins where geometry ends and imparts to letters a character trascending mere measurement.";

		StyleManager _styleManager;
		UIView _divider;
		bool _isFirstStyleSheet = true;

		Dictionary<string, TextStyleParameters> _parsedStylesOne;
		Dictionary<string, TextStyleParameters> _parsedStylesTwo;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			//TextUtils.ListFontNames ("open");
			_parsedStylesOne = CssTextStyleParser.Parse (File.ReadAllText ("StyleOne.css"));
			_parsedStylesTwo = CssTextStyleParser.Parse (File.ReadAllText ("StyleTwo.css"));
			TextStyle.Main.SetStyles (_parsedStylesOne);

			// TEMP
			var stopwatch = Stopwatch.StartNew ();

			// Create a StyleManager to handle any CSS changes automatically
			_styleManager = new StyleManager (TextStyle.Main);
			_styleManager.Add (labelOne, "h2", headingOne);
			_styleManager.Add (labelTwo, "h1", headingTwo);
			_styleManager.Add (labelThree, "h2", headingThree, new List<CssTag> {
				new CssTag ("spot"){ CSS = "spot{color:" + Colors.SpotColor.ToHex () + "}" }
			});
			_styleManager.Add (body, "body", textBody);
			_styleManager.Add (entry, "body", @"hello <i>world</i>", enableHtmlEditing: true);

			body.DataDetectorTypes = UIDataDetectorType.PhoneNumber;

			// Using extension methods
			//body.AttributedText = "Hello world <b>this is a test</b>".ToAttributedString ();

			// TEMP
			//labelOne.Style("h2", headingOne);
			//_styleManager.Add(labelOne, "h2", headingOne);

			Console.WriteLine ("Elapsed time {0}", stopwatch.ElapsedMilliseconds);

			var tapper = new UITapGestureRecognizer (() => { View.EndEditing (true); }) { CancelsTouchesInView = false };
			View.AddGestureRecognizer (tapper);

			AddUIElements ();
		}

		void AddUIElements ()
		{
			// Add a visual divider
			_divider = new UIView ();
			_divider.BackgroundColor = Colors.Grey;
			_divider.UserInteractionEnabled = false;
			_divider.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			Add (_divider);

			// Swapping Button
			var frame = View.Frame;
			var button = new SwapButton ();
			button.Frame = new CGRect (frame.Width / 2 - SwapButton.SIZE * 2, frame.Height - SwapButton.SIZE * 1.5f, SwapButton.SIZE, SwapButton.SIZE);
			button.TouchUpInside += (sender, e) => {
				var styles = _isFirstStyleSheet ? _parsedStylesTwo : _parsedStylesOne;
				TextStyle.Main.SetStyles (styles);

				_isFirstStyleSheet = !_isFirstStyleSheet;
			};
			Add (button);

			// Next Page Button
			var nextPageButton = new NextButton ();
			nextPageButton.Frame = new CGRect (frame.Width / 2 + SwapButton.SIZE, frame.Height - SwapButton.SIZE * 1.5f, SwapButton.SIZE, SwapButton.SIZE);
			nextPageButton.TouchUpInside += (sender, e) => NavigationController.PushViewController (new ManualViewController (), true);
			Add (nextPageButton);
		}

		public override void ViewDidLayoutSubviews ()
		{
			base.ViewDidLayoutSubviews ();
			_divider.Frame = new CGRect (0, body.Frame.Y, View.Frame.Width, 1.0 / UIScreen.MainScreen.Scale);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			NavigationController.SetNavigationBarHidden (true, true);
		}

		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}
	}
}



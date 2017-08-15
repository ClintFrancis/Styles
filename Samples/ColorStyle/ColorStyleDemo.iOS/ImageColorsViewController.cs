using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Styles;

namespace ColorStyleDemo.iOS
{
	public partial class ImageColorsViewController : UIViewController
	{
		UIView backgroundView;
		UIView primaryView;
		UIView secondaryView;
		UIView detailView;
		UIImageView imageView;
		UIImage [] images;
		int count = 0;

		public ImageColorsViewController (IntPtr handle) : base (handle)
		{
			
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			// Perform any additional setup after loading the view, typically from a nib.
			var frame = this.View.Frame;

			images = new UIImage []{
					UIImage.FromFile ("art1.jpeg"),
					UIImage.FromFile ("art2.jpeg"),
					UIImage.FromFile ("art3.jpeg"),
					UIImage.FromFile ("art4.jpeg")
				};

			imageView = new UIImageView (images [0]);
			imageView.Frame = new CGRect (frame.Width / 2 - 150, 80f, 300, 300);
			Add (imageView);


			// Background View
			backgroundView = new UIView () {
				Frame = new CGRect (40f, imageView.Frame.Bottom + 40f, frame.Width - 80f, 120)
			};
			Add (backgroundView);

			// Primary Color
			primaryView = new UIView () {
				Frame = new CGRect (backgroundView.Frame.X + 40f, backgroundView.Frame.Y + 40f, 40f, 40f)
			};
			Add (primaryView);

			// Secondary Color
			secondaryView = new UIView () {
				Frame = new CGRect (backgroundView.Frame.X + 80f, backgroundView.Frame.Y + 40f, 40f, 40f)
			};
			Add (secondaryView);

			// Detail Color
			detailView = new UIView () {
				Frame = new CGRect (backgroundView.Frame.X + 120f, backgroundView.Frame.Y + 40f, 40f, 40f)
			};
			Add (detailView);

			UpdateColorsToImage (images [0]);

			var tapGesture = new UITapGestureRecognizer (() => {
				count++;
				if (count >= images.Length) {
					count = 0;

				}
				imageView.Image = images [count];
				UpdateColorsToImage (images [count]);

			});
			View.AddGestureRecognizer (tapGesture);
		}

		void UpdateColorsToImage (UIImage image)
		{
			var bitmap = image.ToBitmap();
			var colors = bitmap.GetColorSet ();

			backgroundView.BackgroundColor = colors.BackgroundColor.ToNative ();
			primaryView.BackgroundColor = colors.PrimaryColor.ToNative ();
			secondaryView.BackgroundColor = colors.SecondaryColor.ToNative ();
			detailView.BackgroundColor = colors.DetailColor.ToNative ();

		}
	}
}
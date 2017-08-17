// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace NativeTextDemo.iOS
{
	[Register ("MainViewController")]
	partial class MainViewController
	{
		[Outlet]
		UIKit.UIView contentView { get; set; }

		[Outlet]
		UIKit.UIScrollView scrollView { get; set; }

		[Outlet]
		UIKit.UITextView textBody { get; set; }

		[Outlet]
		UIKit.UILabel titleOne { get; set; }

		[Outlet]
		UIKit.UILabel titleThree { get; set; }

		[Outlet]
		UIKit.UILabel titleTwo { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (titleOne != null) {
				titleOne.Dispose ();
				titleOne = null;
			}

			if (titleTwo != null) {
				titleTwo.Dispose ();
				titleTwo = null;
			}

			if (titleThree != null) {
				titleThree.Dispose ();
				titleThree = null;
			}

			if (textBody != null) {
				textBody.Dispose ();
				textBody = null;
			}

			if (contentView != null) {
				contentView.Dispose ();
				contentView = null;
			}

			if (scrollView != null) {
				scrollView.Dispose ();
				scrollView = null;
			}
		}
	}
}

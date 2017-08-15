// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace ColorStyleDemo.iOS
{
    [Register ("ColorTestsViewController")]
    partial class ColorTestsViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView textDisplay { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (textDisplay != null) {
                textDisplay.Dispose ();
                textDisplay = null;
            }
        }
    }
}
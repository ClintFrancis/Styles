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

namespace TextStyleDemo.iOS
{
    [Register ("ViewController")]
    partial class ViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextView body { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UITextField entry { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelOne { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelThree { get; set; }

        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelTwo { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (body != null) {
                body.Dispose ();
                body = null;
            }

            if (entry != null) {
                entry.Dispose ();
                entry = null;
            }

            if (labelOne != null) {
                labelOne.Dispose ();
                labelOne = null;
            }

            if (labelThree != null) {
                labelThree.Dispose ();
                labelThree = null;
            }

            if (labelTwo != null) {
                labelTwo.Dispose ();
                labelTwo = null;
            }
        }
    }
}
using System;
using UIKit;
using CoreGraphics;
using System.Collections.Generic;
using Styles.Text;
using Styles;

namespace TextStyleDemo.iOS
{
    public class ManualViewController : UIViewController
    {
        UILabel _heading;
        UILabel _subtitle;
        UITextField _textEntry;

        public ManualViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            View.BackgroundColor = UIColor.White;
            Title = "Manual UI";

            var frame = View.Frame;

            // Create a heading using the "h1" css style
            _heading = TextStyle.Default.Create<UILabel>("h1", "Hello World");
            var headingFrame = _heading.Frame = new CGRect(20f, 120f, frame.Width - 40f, 60f);
            Add(_heading);

            // Create a subheading using the "h2" css style with a custom tag
            _subtitle = TextStyle.Default.Create<UILabel>("h2", "This is a <spot>subtitle</spot>", new List<CssTag>() {
                new CssTag ("spot"){ CSS = "spot{color:" + Colors.SpotColor.ToHex () + "}" }
            });
            var subtitleFrame = _subtitle.Frame = new CGRect(headingFrame.X, headingFrame.Bottom, headingFrame.Width, 40f);
            Add(_subtitle);

            // Create a text entry field
            _textEntry = TextStyle.Default.Create<UITextField>("body");
            _textEntry.Frame = new CGRect(subtitleFrame.X, subtitleFrame.Bottom, subtitleFrame.Width, 40);
            _textEntry.Layer.BorderColor = Colors.Grey.CGColor;
            _textEntry.Layer.BorderWidth = .5f;
            _textEntry.Layer.CornerRadius = 6f;
            _textEntry.Placeholder = "Type Here";
            _textEntry.LeftViewMode = UITextFieldViewMode.Always;
            _textEntry.LeftView = new UIView()
            {
                Frame = new CGRect(0, 0, 6, 6)
            };

            var tapper = new UITapGestureRecognizer(() => { View.EndEditing(true); }) { CancelsTouchesInView = false };
            View.AddGestureRecognizer(tapper);

            Add(_textEntry);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            NavigationController.SetNavigationBarHidden(false, true);
        }
    }
}


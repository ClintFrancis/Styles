using System;
using System.Drawing;
using CoreGraphics;
using Styles;
using Styles.Text;
using UIKit;

namespace ColorStyleDemo.iOS
{
    public class ColorSwatchView : UIView
    {
        public ColorSwatchView(CGRect frame, IRgb color, string name)
        {
            this.Frame = frame;
            this.BackgroundColor = UIColor.FromRGB((nfloat)color.R, (nfloat)color.G, (nfloat)color.B);

            if (!String.IsNullOrEmpty(name))
            {
                var label = TextStyle.Default.Create<UILabel>("swatch", name);
                label.Frame = new CGRect(0, 0, frame.Width, frame.Height);
                var textColor = label.TextColor.ToColorRGB();

                if (!color.IsContrastingColor(textColor))
                {
                    label.TextColor = textColor.Inverted().ToNative();
                }
                Add(label);
            }
        }
    }
}


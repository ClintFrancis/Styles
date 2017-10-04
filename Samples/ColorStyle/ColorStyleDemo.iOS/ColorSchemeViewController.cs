using Foundation;
using System;
using UIKit;
using CoreGraphics;
using Styles;
using System.Collections.Generic;
using System.Linq;
using Styles.Color;

namespace ColorStyleDemo.iOS
{
    public partial class ColorSchemeViewController : UIViewController
    {
        nfloat padding = 10;
        float swatchSize = 50;
        List<UIView> swatches;

        public ColorSchemeViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var hsbPrimary = ColorSwatches.FlatMagenta;
            var primaryColor = ColorRGB.FromHex("#F04C3B");

            swatches = new List<UIView>() {
				//CreateSwatchView (primaryColor, ColorSchemeType.Monochromatic, "Monochromatic", useFlatColors),
				CreateSwatchView (primaryColor, ColorSchemeType.Complementary, "Complementary"),
                CreateSwatchView (primaryColor, ColorSchemeType.Triadic, "Triadic"),
                CreateSwatchView(primaryColor, ColorSchemeType.Square, "Square"),
                CreateSwatchView (primaryColor, ColorSchemeType.Analogous, "Analogous"),
                CreateSwatchView (primaryColor, ColorSchemeType.SplitComplementary, "Split Complementary"),
                CreateSwatchView(primaryColor, ColorSchemeType.Rectangle, "Rectangle")
            };

            var curFrame = new CGRect();
            var offset = 70f;
            for (int i = 0; i < swatches.Count; i++)
            {
                var currentView = swatches[i];

                if (i > 0)
                {
                    offset = 0;
                }

                curFrame = new CGRect(
                        View.Frame.Width / 2 - currentView.Frame.Width / 2,
                        curFrame.Bottom + padding + offset,
                        currentView.Frame.Width,
                        currentView.Frame.Height
                );

                currentView.Frame = curFrame;
                Add(currentView);
            }
        }



        UIView CreateSwatchView(ColorRGB color, ColorSchemeType type, string title)
        {
            var labelHeight = 20d;
            var scheme = ColorScheme.CreateColorScheme(color, type);
            var rowRect = new CGRect(0, labelHeight, swatchSize, swatchSize);
            var colorView = new UIView()
            {
                Frame = new CGRect(0, 0, scheme.Colors.Count * swatchSize, swatchSize + labelHeight)
            };

            var label = new UILabel
            {
                Text = title,
                Font = UIFont.SystemFontOfSize(8f),
                TextAlignment = UITextAlignment.Center,
                Frame = new CGRect(0, 0, colorView.Frame.Width, labelHeight)
            };
            colorView.Add(label);

            for (int i = 0; i < scheme.Colors.Count; i++)
            {
                if (i > 0)
                    rowRect.Offset(swatchSize, 0);

                var colorRgb = (ColorRGB)scheme.Colors.Values.ElementAt(i);
                var swatch = new ColorSwatchView(rowRect, colorRgb, colorRgb.ToHex());
                colorView.Add(swatch);
            }

            return colorView;
        }
    }
}
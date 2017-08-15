using System;
using CoreAnimation;
using UIKit;

namespace Styles.Color
{
    public class GradientLayer : CALayer
    {
        public GradientType Type { get; private set; }

        LinearGradient linearGradient;
        RadialGradient radialGradient;
        EllipticalGradient ellipticalGradient;
        MultiGradient multiGradient;

        public GradientLayer(LinearGradient gradient)
        {
            this.linearGradient = gradient;
            Type = GradientType.Linear;
            Initalize();
        }

        public GradientLayer(RadialGradient gradient)
        {
            this.radialGradient = gradient;
            Type = GradientType.Radial;
            Initalize();
        }

        public GradientLayer(EllipticalGradient gradient)
        {
            this.ellipticalGradient = gradient;
            Type = GradientType.Ellipse;
            Initalize();
        }

        public GradientLayer(MultiGradient gradient)
        {
            this.multiGradient = gradient;
            Type = GradientType.Multi;
            Initalize();
        }

        public GradientLayer(Gradient[] gradients)
        {
            this.multiGradient = new MultiGradient()
            {
                Gradients = gradients
            };
            Type = GradientType.Multi;
            Initalize();
        }

        void Initalize()
        {
            BackgroundColor = UIColor.Clear.CGColor;
            SetNeedsDisplay();
        }

        public override void DrawInContext(CoreGraphics.CGContext ctx)
        {
            base.DrawInContext(ctx);
            var rect = this.Frame;
            switch (Type)
            {
                case GradientType.Linear:
                    linearGradient.Draw(ctx, rect);
                    break;
                case GradientType.Radial:
                    radialGradient.Draw(ctx, rect);
                    break;
                case GradientType.Ellipse:
                    ellipticalGradient.Draw(ctx, rect);
                    break;
                case GradientType.Multi:
                    multiGradient.Draw(ctx, rect);
                    break;
                default:
                    throw new NotImplementedException("The target gradient type is not supported: " + Type);
            }
        }
    }
}


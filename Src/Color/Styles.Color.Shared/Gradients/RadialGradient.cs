using System;
using System.Drawing;

namespace Styles.Color
{
    public class RadialGradient : Gradient
    {
        // TODO Implement in Bait & Switch
        public PointF Center { get; set; }

        public RadialGradient(IRgb[] colors) : base(colors)
        {
            Center = new PointF(0, 0);
            Type = GradientType.Radial;
        }

        public RadialGradient(IRgb[] colors, float locationX, float locationY) : base(colors)
        {
            Center = new PointF(locationX, locationY);
            Type = GradientType.Radial;
        }

        public RadialGradient(IRgb[] colors, PointF location) : base(colors)
        {
            Center = location;
            Type = GradientType.Radial;
        }

        // Work around method while not working in bait and switch
        public void SetCenter(float x, float y)
        {
            this.Center = new PointF(x, y);
        }

        new public RadialGradient Clone()
        {
            return new RadialGradient((IRgb[])Colors.Clone(), this.Center.X, this.Center.Y)
            {
                Locations = this.Locations,
                Reversed = this.Reversed,
                Repeating = this.Repeating,
                Scale = this.Scale,
                Frame = this.Frame,
                DrawingOptions = this.DrawingOptions
            };
        }
    }
}

using System;
using System.Drawing;

namespace Styles.Color
{
    public class LinearGradient : Gradient
    {
        // -360 -> 0 <- 360
        public double Rotation { get; set; }

        public LinearGradient(IRgb[] colors, double rotation) : base(colors)
        {
            Rotation = rotation;
            Type = GradientType.Linear;
        }

        new public LinearGradient Clone()
        {
            return new LinearGradient((IRgb[])Colors.Clone(), this.Rotation)
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

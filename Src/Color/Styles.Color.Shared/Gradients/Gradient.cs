using System;
using System.Drawing;

namespace Styles.Color
{
    // CSS Gradients
    // https://www.w3schools.com/css/css3_gradients.asp

    public enum GradientType
    {
        None,
        Linear,
        Radial,
        Ellipse,
        Multi,
        Custom
    }

    [Flags]
    public enum GradientDrawingOptions : uint
    {
        None,
        BeforeStartLocation,
        AfterEndLocation
    }

    public class ColorOffset
    {
        public IRgb Color { get; set; }
        public double Offset { get; set; }

        public ColorOffset(IRgb color, double offset)
        {
            Color = color;
            Offset = offset;
        }
    }

    public class Gradient
    {
        public GradientDrawingOptions DrawingOptions { get; set; } = GradientDrawingOptions.BeforeStartLocation | GradientDrawingOptions.AfterEndLocation;

        // TODO Implement in Bait & Switch
        public RectangleF Frame { get; set; }

        public GradientType Type { get; protected set; }

        public IRgb[] Colors { get; set; }

        public float[] Locations { get; set; } = new float[0];

        // TODO Reversed
        public bool Reversed { get; set; }

        // TODO repeating
        public bool Repeating { get; set; }

        // TODO Scale
        public PointF Scale { get; set; } = new PointF(1f, 1f);

        public ColorOffset[] Offsets
        {
            get
            {
                var offsets = new ColorOffset[Colors.Length];
                for (int i = 0; i < Colors.Length; i++)
                {
                    offsets[i] = new ColorOffset(Colors[i], Locations[i]);
                }
                return offsets;
            }
        }

        public Gradient(IRgb[] colors)
        {
            this.Colors = colors;
        }

        // Work around method while not working in bait and switch
        public void SetFrame(float x, float y, float width, float height)
        {
            this.Frame = new RectangleF(x, y, width, height);
        }

        // Work around method while not working in bait and switch
        public void SetScale(float scaleX, float scaleY)
        {
            this.Scale = new PointF(scaleX, scaleY);
        }

        public void Update()
        {
            if (Colors.Length < 2)
            {
                throw new Exception("Gradients must contain a minimum of two colors");
            }

            if (Locations.Length == 0)
            {
                float stepSize = 1f / (Colors.Length - 1f);
                float currentStep = 0;
                Locations = new float[Colors.Length];

                for (int i = 0; i < Colors.Length; i++)
                {
                    Locations[i] = currentStep;
                    currentStep += stepSize;
                }
            }
            else if (Locations.Length != Colors.Length)
            {
                throw new Exception("Gradient Colors length is not equal to its Locations length");
            }
        }

        public static Gradient FromColors<T>(IColorSpace[] colors)
        {
            throw new NotImplementedException();
        }

        public LinearGradient ToLinear(double rotation = 0)
        {
            return new LinearGradient(this.Colors, rotation)
            {
                Locations = this.Locations,
                Frame = this.Frame,
                Reversed = this.Reversed,
                Repeating = this.Repeating,
                Scale = this.Scale,
                DrawingOptions = this.DrawingOptions
            };
        }

        public RadialGradient ToRadial(float locationX = .5f, float locationY = .5f)
        {
            return new RadialGradient(this.Colors, locationX, locationY)
            {
                Locations = this.Locations,
                Frame = this.Frame,
                Reversed = this.Reversed,
                Repeating = this.Repeating,
                Scale = this.Scale,
                DrawingOptions = this.DrawingOptions
            };
        }

        public EllipticalGradient ToEllipse(float locationX = .5f, float locationY = .5f, double rotation = 0)
        {
            return new EllipticalGradient(this.Colors, locationX, locationY)
            {
                Locations = this.Locations,
                Frame = this.Frame,
                Reversed = this.Reversed,
                Repeating = this.Repeating,
                Scale = this.Scale,
                Rotation = rotation,
                DrawingOptions = this.DrawingOptions
            };
        }

        public Gradient ShiftHues(double value)
        {
            var target = this.Clone();
            for (int i = 0; i < target.Colors.Length; i++)
            {
                target.Colors[i] = target.Colors[i].AdjustHue(value);
            }

            return target;
        }

        /// <summary>
        /// Returns the color at the given scale by interpolating the colors.
        /// </summary>
        /// <returns>IRgb<see cref="T:Styles.IRgb"/>.</returns>
        /// <param name="percent">Percent</param>
        public IRgb GetColorAt(double percent)
        {
            return GradientUtils.GetColorByOffset(this, percent);
        }

        /// <summary>
        /// Returns a color IRgb palette of x steps by selecting equidistant colors.
        /// </summary>
        /// <returns>Array of IRgb colors</returns>
        /// <param name="steps">The number of color steps to return. 2 by default</param>
        public IRgb[] CreateColorPalette(int steps = 2)
        {
            var stepSize = 1.0 / steps;
            var colorPalette = new IRgb[steps];
            for (int i = 0; i < steps; i++)
            {
                var step = stepSize * i;
                colorPalette[i] = GradientUtils.GetColorByOffset(this, step);
            }

            return colorPalette;
        }

        public Gradient Clone()
        {
            return new Gradient((IRgb[])Colors.Clone())
            {
                Locations = this.Locations,
                Reversed = this.Reversed,
                Repeating = this.Repeating,
                Scale = this.Scale,
                Frame = this.Frame
            };
        }
    }
}

using System;
namespace Styles.Colors
{
    public struct RGBA : IRgb
    {
        #region Accessors

        double red;
        double green;
        double blue;
        double alpha;

        public double R
        {
            get { return red; }
            set { red = Math.Max(0, Math.Min(1, value)); }
        }

        public byte Red
        {
            get { return (byte)Math.Round(this.R * 255); }
            set { this.R = value / 255; }
        }

        public double G
        {
            get { return green; }
            set { green = Math.Max(0, Math.Min(1, value)); }
        }

        public byte Green
        {
            get { return (byte)Math.Round(this.G * 255); }
            set { this.G = value / 255; }
        }

        public double B
        {
            get { return blue; }
            set { blue = Math.Max(0, Math.Min(1, value)); }
        }

        public byte Blue
        {
            get { return (byte)Math.Round(this.B * 255); }
            set { this.B = value / 255; }
        }

        public double A
        {
            get { return alpha; }
            set { alpha = Math.Max(0, Math.Min(1, value)); }
        }

        public byte Alpha
        {
            get { return (byte)Math.Round(this.A * 255); }
            set { this.A = value / 255; }
        }

        #endregion

        public RGBA(double r, double g, double b, double a)
        {
            red = r;
            green = g;
            blue = b;
            alpha = a;
        }

        public void Initialize(IRgb color)
        {
            this.R = color.R;
            this.G = color.G;
            this.B = color.B;
            this.A = color.A;
        }

        public IRgb ToRgb()
        {
            return this;
        }
    }
}

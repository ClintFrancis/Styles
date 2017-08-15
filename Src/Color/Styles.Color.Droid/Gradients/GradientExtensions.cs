using System;
using Android.Graphics;
using Android.Graphics.Drawables;
using Styles;

namespace Styles.Color
{
    public static class GradientExtensions
    {
        // todo ToNative

        static int[] DetermineGradientPoints(LinearGradient target)
        {
            // create coordinates
            var x = target.Rotation / 360d;
            var a = (int)Math.Pow(Math.Sin((2 * Math.PI * ((x + 0.75) / 2))), 2);
            var b = (int)Math.Pow(Math.Sin((2 * Math.PI * ((x + 0.0) / 2))), 2);
            var c = (int)Math.Pow(Math.Sin((2 * Math.PI * ((x + 0.25) / 2))), 2);
            var d = (int)Math.Pow(Math.Sin((2 * Math.PI * ((x + 0.5) / 2))), 2);

            return new int[] { a, b, c, d };
        }

        //static GradientDrawable DrawGradient()
        //{
        //    var gradient = new GradientDrawable();
        //    gradient.Color



        //    return null;
        //}


        public static Shader Draw(this LinearGradient target, RectF bounds)
        {
            target.Update();

            var grad = new Android.Graphics.LinearGradient(
                0, 0, bounds.Width(), 0,
                target.Colors.ToInt(),
                target.Locations,
                Shader.TileMode.Clamp
            );

            Matrix matrix = new Matrix();
            matrix.SetRotate((float)target.Rotation);
            grad.SetLocalMatrix(matrix);

            return grad;
        }

        public static Shader Draw(this RadialGradient target, RectF bounds)
        {
            var center = new PointF((bounds.Width() * target.Center.X), (bounds.Height() * target.Center.Y));
            var radius = (float)Math.Min(bounds.Width() / 2f, bounds.Height() / 2f);

            return target.Draw(center, radius);
        }

        public static Shader Draw(this RadialGradient target, PointF position, float radius)
        {
            target.Update();

            var grad = new Android.Graphics.RadialGradient(
                position.X, position.Y, radius,
                target.Colors.ToInt(),
                target.Locations,
                Shader.TileMode.Clamp
            );

            return grad;
        }

        public static Shader Draw(this EllipticalGradient target, RectF bounds)
        {
            target.Update();

            var center = new PointF((bounds.Width() * target.Center.X), (bounds.Height() * target.Center.Y));
            var radius = (float)Math.Min(bounds.Width() / 2f, bounds.Height() / 2f);

            var grad = new Android.Graphics.RadialGradient(
                center.X, center.Y, radius,
                target.Colors.ToInt(),
                target.Locations,
                Shader.TileMode.Clamp
            );

            Matrix matrix = new Matrix();
            matrix.SetScale(target.Scale.X, target.Scale.Y, center.X, center.Y);
            matrix.PostRotate((float)target.Rotation, center.X, center.Y);
            grad.SetLocalMatrix(matrix);

            return grad;
        }

        // TODO implement GradientDrawable https://stackoverflow.com/questions/31968086/adding-a-gradient-layer-in-xamarin-forms-android

        //public static GradientDrawable Draw(this EllipticalGradient target, RectF bounds)
        //{

        //    return null;
        //}

        public static GradientDrawable Draw(this Gradient[] items, RectF bounds)
        {
            for (int i = 0; i < items.Length; i++)
            {
                var gradient = items[i];
                switch (gradient.Type)
                {
                    case GradientType.Linear:
                        var linearGradient = gradient as LinearGradient;
                        //linearGradient.Draw(ctx, bounds);
                        break;
                    case GradientType.Radial:
                        var radialGradient = gradient as RadialGradient;
                        //radialGradient.Draw(ctx, bounds);
                        break;
                    case GradientType.Ellipse:
                        var ellispseGradient = gradient as EllipticalGradient;
                        //ellispseGradient.Draw(ctx, bounds);
                        break;
                    default:
                        throw new NotImplementedException("Multigradients does not support the supplied gradient type: " + gradient.Type);
                }
            }

            return null;
        }

        public static GradientDrawable Draw(this MultiGradient target, RectF bounds)
        {

            return null;
        }
    }
}

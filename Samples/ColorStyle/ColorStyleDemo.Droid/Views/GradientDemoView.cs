using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Util;
using Android.Views;
using Styles.Color;
using Styles;

namespace ColorStyleDemo.Droid
{
    public class GradientDemoView : View
    {
        GradientDrawable mDrawable;

        public GradientDemoView(Context context) :
            base(context)
        {
            Initialize();
        }

        public GradientDemoView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public GradientDemoView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        void Initialize()
        {

        }

        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            var linearGradient = new Styles.Color.LinearGradient(
              new IRgb[] { ColorSwatches.FlatMint, ColorSwatches.FlatBlue },
              45
            );

            var radialGradient = new Styles.Color.RadialGradient(
                new IRgb[] { ColorSwatches.FlatMint, ColorSwatches.FlatBlue },
                .5f, .5f
            );

            var ellipticalGradient = new EllipticalGradient(
                new IRgb[] { ColorSwatches.FlatMint, ColorSwatches.FlatBlue, ColorSwatches.DeepPurple },
                .5f, .5f
            );
            ellipticalGradient.SetScale(1f, 2f);
            ellipticalGradient.Rotation = 45;

            var bounds = canvas.ClipBounds;
            var shader = ellipticalGradient.Draw(new RectF(bounds.Left, bounds.Top, bounds.Right, bounds.Bottom));

            var paint = new Paint();
            paint.SetShader(shader);

            canvas.DrawPaint(paint);
        }
    }
}

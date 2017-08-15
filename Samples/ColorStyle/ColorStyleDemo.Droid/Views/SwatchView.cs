
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace ColorStyleDemo.Droid
{
    public class SwatchView : View
    {
        public SwatchView(Context context) :
            base(context)
        {
            Initialize();
        }

        public SwatchView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public SwatchView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }

        void Initialize()
        {
        }

        protected override void OnDraw(Canvas canvas)
        {
            var paint = new Paint() { Color = Color.Blue };
            canvas.DrawRect(new RectF(0, 0, 200, 200), paint);
        }
    }
}

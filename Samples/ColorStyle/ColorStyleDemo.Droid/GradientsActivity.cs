
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Styles.Color;

namespace ColorStyleDemo.Droid
{
    [Activity(Label = "GradientsActivity")]
    public class GradientsActivity : Activity
    {
        LinearLayout layout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Gradients);

            layout = FindViewById<LinearLayout>(Resource.Id.gradientLayout);

            layout.AddView(new GradientDemoView(this));
        }
    }
}

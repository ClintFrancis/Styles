
using Android.App;
using Android.OS;
using Android.Widget;

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

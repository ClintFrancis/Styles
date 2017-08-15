using Android.App;
using Android.Widget;
using Android.OS;
using Android.Content;

namespace ColorStyleDemo.Droid
{
    [Activity(Label = "Droid", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Buttons
            Button btnImageColors = FindViewById<Button>(Resource.Id.btnImageColors);
            btnImageColors.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ImageColorsActivity));
                StartActivity(intent);
            };

            Button btnColorAdjust = FindViewById<Button>(Resource.Id.btnColorAdjust);
            btnColorAdjust.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ColorAdjustActivity));
                StartActivity(intent);
            };

            Button btnColorScheme = FindViewById<Button>(Resource.Id.btnColorScheme);
            btnColorScheme.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(ColorSchemeActivity));
                StartActivity(intent);
            };

            Button btnGradients = FindViewById<Button>(Resource.Id.btnGradients);
            btnGradients.Click += (sender, e) =>
            {
                var intent = new Intent(this, typeof(GradientsActivity));
                StartActivity(intent);
            };

        }
    }
}


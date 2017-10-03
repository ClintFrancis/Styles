using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Views.InputMethods;
using Android.Widget;
using Styles;
using Styles.Text;

namespace TextStyleDemo.Droid
{
    [Activity(Theme = "@android:style/Theme.Holo.Light", Label = "TextStyle Demo", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        const string headingOne = @"The difference between";
        const string headingTwo = @"Ordinary & Extraordinary";
        const string headingThree = @"Is that little <spot>extra</spot>";
        const string textBody = @"Geometry can produce legible letters but <i>art alone</i> makes them beautiful.<p>Art begins where geometry ends, and imparts to letters a character trascending mere measurement.</p>";
        const string editbody = @"<i>art alone</i> makes";

        bool _isFirstStyleSheet = true;
        StyleManager _styleManager;
        Dictionary<string, TextStyleParameters> _parsedStylesOne;
        Dictionary<string, TextStyleParameters> _parsedStylesTwo;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            this.Window.SetSoftInputMode(Android.Views.SoftInput.AdjustPan);

            // Register Fonts
            TextStyle.Default.AddFont("Archistico-Normal", "Archistico_Simple.ttf");
            TextStyle.Default.AddFont("Avenir-Medium", "Avenir-Medium.ttf");
            TextStyle.Default.AddFont("Avenir-Book", "Avenir-Book.ttf");
            TextStyle.Default.AddFont("Avenir-Heavy", "Avenir-Heavy.ttf");
            TextStyle.Default.AddFont("BreeSerif-Regular", "BreeSerif-Regular.ttf");
            TextStyle.Default.AddFont("OpenSans-CondBold", "OpenSans-CondBold.ttf");
            TextStyle.Default.AddFont("OpenSans-CondLight", "OpenSans-CondLight.ttf");

            // Pre-parse the style sheets
            _parsedStylesOne = CssTextStyleParser.Parse(OpenCSSFile("StyleOne.css"));
            _parsedStylesTwo = CssTextStyleParser.Parse(OpenCSSFile("StyleTwo.css"));
            TextStyle.Default.SetStyles(_parsedStylesOne);

            // Get references to our UI Elements
            var labelOne = FindViewById<TextView>(Resource.Id.labelOne);
            var labelTwo = FindViewById<TextView>(Resource.Id.labelTwo);
            var labelThree = FindViewById<TextView>(Resource.Id.labelThree);
            var body = FindViewById<TextView>(Resource.Id.body);
            var editText = FindViewById<TextView>(Resource.Id.editText);
            editText.Click += (sender, e) =>
            {
                editText.SetCursorVisible(true);
            };

            // Create a StyleManager to handle any CSS changes automatically
            _styleManager = new StyleManager(TextStyle.Default);
            //_styleManager.Add(labelOne, "h2", headingOne);
            //_styleManager.Add(labelTwo, "h1", headingTwo);
            _styleManager.Add(labelThree, "h2", headingThree, new List<CssTag> {
                new CssTag ("spot"){ CSS = "spot{color:" + Colors.SpotColor.ToHex() + "}" }
            });
            _styleManager.Add(body, "body", textBody);
            _styleManager.Add(editText, "body", editbody, enableHtmlEditing: true);

            // Dismiss keyboard on tap of background
            var layout = (LinearLayout)FindViewById(Resource.Id.layout);
            layout.Touch += (sender, e) =>
            {
                InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(editText.WindowToken, 0);
                editText.ClearFocus();
                //editText.SetCursorVisible (false);
            };

            // Create a toggle button for swapping between styles
            var toggleButton = FindViewById<ImageButton>(Resource.Id.refreshIcon);
            toggleButton.SetBackgroundColor(Color.Transparent);
            toggleButton.Click += (sender, e) =>
            {
                var styles = _isFirstStyleSheet ? _parsedStylesTwo : _parsedStylesOne;
                TextStyle.Default.SetStyles(styles);

                _isFirstStyleSheet = !_isFirstStyleSheet;
            };

            editText.ClearFocus();
        }

        string OpenCSSFile(string filename)
        {
            string style;
            using (var sr = new StreamReader(Assets.Open(filename)))
            {
                style = sr.ReadToEnd();
            }

            return style;
        }
    }
}
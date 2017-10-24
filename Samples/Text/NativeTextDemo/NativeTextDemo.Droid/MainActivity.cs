using Android.App;
using Android.Widget;
using Android.OS;
using GalaSoft.MvvmLight.Ioc;
using Styles.Text;
using Android.Views;
using NativeTextDemo.ViewModel;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;

namespace NativeTextDemo.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "NativeTextDemo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity
    {
        TextView titleOne;
        TextView titleTwo;
        TextView titleThree;
        TextView textBody;
        EditText textEntry;

        StyleManager styleManager;
        IManagedStyle styleOne;
        IManagedStyle styleTwo;
        IManagedStyle styleThree;
        IManagedStyle styleBody;
        IManagedStyle styleEntry;

        // Keep track of bindings to avoid premature garbage collection
        readonly List<Binding> bindings = new List<Binding>();

        MainViewModel Vm
        {
            get
            {
                return ViewModelLocator.Main;
            }
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Register a new text style
            SimpleIoc.Default.Register<ITextStyle>(() => TextStyle.Default);

            // Register Fonts
            TextStyle.Default.AddFont("Archistico-Normal", "Archistico_Simple.ttf");
            TextStyle.Default.AddFont("Avenir-Medium", "Avenir-Medium.ttf");
            TextStyle.Default.AddFont("Avenir-Book", "Avenir-Book.ttf");
            TextStyle.Default.AddFont("Avenir-Heavy", "Avenir-Heavy.ttf");
            TextStyle.Default.AddFont("BreeSerif-Regular", "BreeSerif-Regular.ttf");
            TextStyle.Default.AddFont("OpenSans-CondBold", "OpenSans-CondBold.ttf");
            TextStyle.Default.AddFont("OpenSans-CondLight", "OpenSans-CondLight.ttf");

            AppBootstrapper.Init();

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            toolbar.Title = "Native Text Demo";
            SetActionBar(toolbar);

            // Get our ui elements from the layout resource
            titleOne = FindViewById<TextView>(Resource.Id.labelOne);
            titleTwo = FindViewById<TextView>(Resource.Id.labelTwo);
            titleThree = FindViewById<TextView>(Resource.Id.labelThree);
            textBody = FindViewById<TextView>(Resource.Id.body);
            textEntry = FindViewById<EditText>(Resource.Id.editText);

            // Set up the Style Manager
            styleManager = new StyleManager();
            styleOne = styleManager.Add(titleOne, TextStyles.H2);
            styleTwo = styleManager.Add(titleTwo, TextStyles.H1);
            styleThree = styleManager.Add(titleThree, TextStyles.H2);
            styleThree.AutoUpdate = true;
            styleBody = styleManager.Add(textBody, TextStyles.Body);
            styleEntry = styleManager.Add(textEntry, TextStyles.Body);
            styleEntry.EnableHtmlEditing = true;

            bindings.Add(
                this.SetBinding(
                    () => ViewModelLocator.Styles.CustomTags,
                    () => styleManager.CustomTags
                )
            );

            bindings.Add(
                this.SetBinding(
                    () => Vm.TitleOne,
                    () => styleOne.Text
                )
            );

            bindings.Add(
                this.SetBinding(
                    () => Vm.TitleTwo,
                    () => styleTwo.Text
                )
            );

            bindings.Add(
                this.SetBinding(
                    () => Vm.TitleThree,
                    () => styleThree.Text
                )
            );

            bindings.Add(
                this.SetBinding(
                    () => Vm.Body,
                    () => styleBody.Text
                )
            );

            bindings.Add(
                this.SetBinding(
                    () => Vm.Entry,
                    () => styleEntry.Text
                )
            );
        }

        /// <Docs>The options menu in which you place your items.</Docs>
        /// <returns>To be added.</returns>
        /// <summary>
        /// This is the menu for the Toolbar/Action Bar to use
        /// </summary>
        /// <param name="menu">Menu.</param>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.home, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            Toast.MakeText(this, "Top ActionBar pressed: " + item.TitleFormatted, ToastLength.Short).Show();
            return base.OnOptionsItemSelected(item);
        }
    }
}


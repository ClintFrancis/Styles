using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Ioc;
using NativeTextDemo.ViewModel;
using Styles.Text;
using static Android.Views.View;

namespace NativeTextDemo.Droid
{
    [Activity(Theme = "@style/MyTheme", Label = "NativeTextDemo", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : Activity, IOnKeyListener
    {
        StyleManager styleManager;
        IManagedStyle styleOne;
        IManagedStyle styleTwo;
        IManagedStyle styleThree;
        IManagedStyle styleBody;
        IManagedStyle styleEntry;
        EditText editText;

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

            styleManager = new StyleManager();
            styleOne = styleManager.Add(FindViewById<TextView>(Resource.Id.titleOne), TextStyles.H2);
            styleTwo = styleManager.Add(FindViewById<TextView>(Resource.Id.titleTwo), TextStyles.H1);
            styleThree = styleManager.Add(FindViewById<TextView>(Resource.Id.titleThree), TextStyles.H2);
            styleThree.AutoUpdate = true;
            styleBody = styleManager.Add(FindViewById<TextView>(Resource.Id.textBody), TextStyles.Body);

            editText = FindViewById<EditText>(Resource.Id.textEdit);
            styleEntry = styleManager.Add(editText, TextStyles.Body, enableHtmlEditing: true);

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

            // Dismiss keyboard on tap of background
            editText.EditorAction += (sender, e) =>
            {
                if (e.ActionId == ImeAction.Done)
                    DismissKeyboard();
            };


            var layout = (LinearLayout)FindViewById(Resource.Id.layout);
            layout.Touch += (sender, e) => DismissKeyboard();
        }

        void DismissKeyboard()
        {
            InputMethodManager imm = (InputMethodManager)GetSystemService(Context.InputMethodService);
            imm.HideSoftInputFromWindow(editText.WindowToken, 0);
            editText.ClearFocus();
        }

        void StyleTextAlt()
        {
            var titleOne = FindViewById<TextView>(Resource.Id.titleOne);
            TextStyle.Default.Style(titleOne, TextStyles.H2, Vm.TitleOne);

            var titleTwo = FindViewById<TextView>(Resource.Id.titleTwo);
            TextStyle.Default.Style(titleTwo, TextStyles.H1, Vm.TitleTwo);

            var titleThree = FindViewById<TextView>(Resource.Id.titleThree);
            TextStyle.Default.Style(titleThree, TextStyles.H2, Vm.TitleThree);

            var textBody = FindViewById<TextView>(Resource.Id.textBody);
            TextStyle.Default.Style(textBody, TextStyles.Body, Vm.Body);

            var textEdit = FindViewById<TextView>(Resource.Id.textEdit);
            TextStyle.Default.Style(textEdit, TextStyles.Body, Vm.Entry);
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
            ViewModelLocator.Styles.RefreshCommand.Execute(null);
            StyleTextAlt();
            return base.OnOptionsItemSelected(item);
        }
    }
}


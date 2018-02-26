// This file has been autogenerated from a class added in the UI designer.

using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Helpers;
using NativeTextDemo.ViewModel;
using Styles.Text;
using UIKit;

namespace NativeTextDemo.iOS
{
    public partial class MainViewController : UIViewController
    {
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

        public MainViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            refreshButton.Clicked += RefreshButton_Clicked;

            // Set the binding BEFORE adding items so as to avoid restyling everything
            styleManager = new StyleManager();
            bindings.Add(this.SetBinding(() => ViewModelLocator.Styles.CustomTags, () => styleManager.CustomTags));

            styleOne = styleManager.Add(titleOne, TextStyles.H2);
            bindings.Add(this.SetBinding(() => Vm.TitleOne, () => styleOne.Text));

            styleTwo = styleManager.Add(titleTwo, TextStyles.H1);
            bindings.Add(this.SetBinding(() => Vm.TitleTwo, () => styleTwo.Text));

            styleThree = styleManager.Add(titleThree, TextStyles.H2);
            bindings.Add(this.SetBinding(() => Vm.TitleThree, () => styleThree.Text));

            styleBody = styleManager.Add(textBody, TextStyles.Body);
            bindings.Add(this.SetBinding(() => Vm.Body, () => styleBody.Text));

            styleEntry = styleManager.Add(textEntry, TextStyles.Body, enableHtmlEditing: true);
            bindings.Add(this.SetBinding(() => Vm.Entry, () => styleEntry.Text));

            Xamarin.IQKeyboardManager.SharedManager.EnableAutoToolbar = true;
            Xamarin.IQKeyboardManager.SharedManager.ShouldResignOnTouchOutside = true;

            var touchGesture = new UITapGestureRecognizer(HandleAction);
            View.AddGestureRecognizer(touchGesture);
        }

        void RefreshButton_Clicked(object sender, EventArgs e)
        {
            if (Vm != null)
            {
                ViewModelLocator.Styles.RefreshCommand.Execute(null);
            }
        }

        void HandleAction()
        {
            if (textEntry.IsEditing)
                View.EndEditing(true);
        }
    }
}

using NativeTextDemo.ViewModel;
using UIKit;

namespace NativeTextDemo.iOS
{
    public class Application
    {
        private static ViewModelLocator locator;
        public static ViewModelLocator Locator
        {
            get
            {
                return locator ?? (locator = new ViewModelLocator());
            }
        }

        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
        }
    }
}

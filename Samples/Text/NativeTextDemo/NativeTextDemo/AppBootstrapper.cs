using GalaSoft.MvvmLight.Ioc;
using NativeTextDemo.ViewModel;
using Styles.Text;

namespace NativeTextDemo
{
    public static class AppBootstrapper
    {
        public static void Init()
        {
            // Set the assembly reference for the Asset Loader
            Assets.AssemblyType = typeof(AppBootstrapper);

            // Initalise the ViewModelLocator
            ViewModelLocator.Init();
        }
    }
}

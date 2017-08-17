using GalaSoft.MvvmLight.Ioc;
using Styles.Text;

namespace NativeTextDemo
{
    public static class AppBootstrapper
    {
        public static void Init()
        {
            // Set the assembly reference for the Asset Loader
            Assets.AssemblyType = typeof(AppBootstrapper);

            // Register the TextStyle
            SimpleIoc.Default.Register<TextStyle>(() => new TextStyle());
        }
    }
}

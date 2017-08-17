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
            //SimpleIoc.Default.Register<ITextStyle>(() => new TextStyle());

            // Ensure that the Styles.Text library has been initialised
            var textStyle = SimpleIoc.Default.GetInstance<ITextStyle>();
            var css = Assets.LoadString("NativeTextDemo.Resources.StyleOne.css");
            textStyle.SetCSS(css);
        }
    }
}

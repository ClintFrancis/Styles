using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Styles.Text;

namespace NativeTextDemo
{
    public class StylesViewModel : ViewModelBase
    {
        public string CSS1 { get; private set; }
        public string CSS2 { get; private set; }

        public StylesViewModel()
        {
            Init();
        }

        public void Init()
        {
            var textStyle = SimpleIoc.Default.GetInstance<ITextStyle>();
            CSS1 = Assets.LoadString("NativeTextDemo.Resources.StyleTwo.css");
            //CSS2 = Assets.LoadString("NativeTextDemo.Resources.StyleOne.css");
            textStyle.SetCSS(CSS1);
        }
    }
}

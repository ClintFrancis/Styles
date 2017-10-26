using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Styles;
using Styles.Text;

namespace NativeTextDemo
{
    public class StylesViewModel : ViewModelBase
    {
        public string CSS1 { get; private set; }
        public string CSS2 { get; private set; }
        ITextStyle textStyle;
        bool isCss1 = true;


        List<CssTag> _customTags;

        public List<CssTag> CustomTags
        {
            get
            {
                return _customTags;
            }

            private set
            {
                if (_customTags == value)
                    return;

                _customTags = value;
                RaisePropertyChanged("CustomTags");
            }
        }

        RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                  ?? (_refreshCommand = new RelayCommand(
                    () =>
                    {
                        if (isCss1)
                        {
                            textStyle.SetCSS(CSS2);
                            isCss1 = false;
                        }
                        else
                        {
                            textStyle.SetCSS(CSS1);
                            isCss1 = true;
                        }
                    }));
            }
        }


        public StylesViewModel()
        {
            Init();
        }

        public void Init()
        {
            textStyle = SimpleIoc.Default.GetInstance<ITextStyle>();
            CSS1 = Assets.LoadString("NativeTextDemo.Resources.StyleTwo.css");
            CSS2 = Assets.LoadString("NativeTextDemo.Resources.StyleOne.css");
            textStyle.SetCSS(CSS2);
            isCss1 = false;

            CustomTags = new List<CssTag> {
                new CssTag ("spot"){ CSS = "spot{color:" + ColorSwatches.SpotColor.ToHex () + "}" }
            };
        }
    }
}

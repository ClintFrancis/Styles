using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace NativeTextDemo.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _titleOne;

        public string TitleOne
        {
            get
            {
                return _titleOne;
            }

            private set
            {
                if (_titleOne == value)
                    return;

                _titleOne = value;
                RaisePropertyChanged("TitleOne");
            }
        }

        private string _titleTwo;

        public string TitleTwo
        {
            get
            {
                return _titleTwo;
            }

            private set
            {
                if (_titleTwo == value)
                    return;

                _titleTwo = value;
                RaisePropertyChanged("TitleTwo");
            }
        }

        private string _titleThree;

        public string TitleThree
        {
            get
            {
                return _titleThree;
            }

            private set
            {
                if (_titleThree == value)
                    return;

                _titleThree = value;
                RaisePropertyChanged("TitleThree");
            }
        }

        private string _body;

        public string Body
        {
            get
            {
                return _body;
            }

            private set
            {
                if (_body == value)
                    return;

                _body = value;
                RaisePropertyChanged("Body");
            }
        }

        private RelayCommand _refreshCommand;

        public RelayCommand RefreshCommand
        {
            get
            {
                return _refreshCommand
                  ?? (_refreshCommand = new RelayCommand(
                    () =>
                    {
                        //this.TitleOne = "Test Rules!";
                    }));
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _titleOne = @"The difference between";
            _titleTwo = @"Ordinary & Extraordinary";
            _titleThree = @"Is that little <spot>extra</spot>";
            _body = @"Geometry can produce legible letters but <i>art alone</i> makes them beautiful.<br/><br/>Art begins where geometry ends and imparts to letters a character trascending mere measurement.";
        }
    }
}
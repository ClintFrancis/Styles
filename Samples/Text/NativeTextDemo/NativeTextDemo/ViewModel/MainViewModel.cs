using GalaSoft.MvvmLight;

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
                {
                    return;
                }

                _titleOne = value;
                // Update bindings, no broadcast
                RaisePropertyChanged(_titleOne);
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
                {
                    return;
                }

                _titleTwo = value;
                // Update bindings, no broadcast
                RaisePropertyChanged(_titleTwo);
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
                {
                    return;
                }

                _titleThree = value;
                // Update bindings, no broadcast
                RaisePropertyChanged(_titleThree);
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
                {
                    return;
                }

                _body = value;
                // Update bindings, no broadcast
                RaisePropertyChanged(_body);
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            _titleOne = "Hello World";
            _titleTwo = "This is a heading";
            _titleThree = "about a heading...";
            _body = "Lorum Ipsum Facto";
        }
    }
}
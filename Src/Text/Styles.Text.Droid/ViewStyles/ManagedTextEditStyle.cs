using System;
using Android.Widget;

namespace Styles.Text
{
    public class ManagedTextEditStyle : ManagedStyleBase
    {
        public EditText Target { get; private set; }

        bool _enableHtmlEditing;
        override public bool EnableHtmlEditing
        {
            get { return _enableHtmlEditing; }
            set
            {
                _enableHtmlEditing = value;

                if (Target != null)
                {
                    if (value)
                    {
                        Target.TextChanged += TextEditingChanged;
                        Target.FocusChange += TextFocusChanged;

                    }
                    else
                    {
                        Target.TextChanged -= TextEditingChanged;
                        Target.FocusChange -= TextFocusChanged;
                    }
                }
            }
        }

        public ManagedTextEditStyle(ITextStyle instance, EditText target, string styleID, string text, bool updateConstraints)
        {
            styleInstance = instance as TextStyle;
            _updateConstraints = updateConstraints;
            Target = target;

            StyleID = styleID;
            Text = !string.IsNullOrEmpty(text) ? text : Target.Text;
        }

        public override void Dispose()
        {
            EnableHtmlEditing = false;
            Target = null;
        }

        public override void UpdateDisplay()
        {
            if (IsDirty)
            {
                styleInstance.Style(Target, StyleID, _rawText, CustomTags, true);
                _isDirty = false;
            }
            else
            {
                Target.SetText(AttributedValue, TextView.BufferType.Spannable);
            }
        }

        public override void UpdateFrame()
        {
            // Does nothing on Android
        }

        void TextEditingChanged(object sender, EventArgs e)
        {
            if (Target.IsFocused)
                _rawText = Target.Text;
        }

        void TextFocusChanged(object sender, Android.Views.View.FocusChangeEventArgs e)
        {
            if (Target.IsFocused)
                Target.Text = _rawText;
            else
                Text = Target.Text;
        }
    }
}

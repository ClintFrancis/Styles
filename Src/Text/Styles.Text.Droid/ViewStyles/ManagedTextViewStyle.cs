using System;
using Android.Widget;

namespace Styles.Text
{
    public class ManagedTextViewStyle : ManagedStyleBase
    {
        public TextView Target { get; private set; }

        public ManagedTextViewStyle(ITextStyle instance, TextView target, string styleID, string text, bool updateConstraints)
        {
            styleInstance = instance as TextStyle;
            _updateConstraints = updateConstraints;
            Target = target;

            StyleID = styleID;
            Text = !string.IsNullOrEmpty(text) ? text : Target.Text;
        }

        public override void Dispose()
        {
            Target = null;
        }

        public override void UpdateDisplay()
        {
            if (IsDirty)
            {
                styleInstance.Style(Target, StyleID, Text, CustomTags, true);
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
    }
}

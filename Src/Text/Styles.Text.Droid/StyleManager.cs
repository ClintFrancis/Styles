using System.Collections.Generic;
using Android.Widget;

namespace Styles.Text
{
    public class StyleManager : StyleManagerBase
    {
        public StyleManager(ITextStyle instance)
        {
            Init(instance);
        }

        override protected IManagedStyle CreateViewStyle<T>(T target, string styleID, string text, List<CssTag> customTags, bool useExistingStyles = true, bool enableHtmlEditing = false)
        {
            var tags = customTags ?? CustomTags;
            var type = target.GetType();

            IManagedStyle style;
            if (type == TextStyle.typeEditText)
            {
                style = new ManagedTextEditStyle(_instance, target as EditText, styleID, text, true)
                {
                    CustomTags = tags,
                    EnableHtmlEditing = enableHtmlEditing
                };
            }

            else
            {
                style = new ManagedTextViewStyle(_instance, target as TextView, styleID, text, true)
                {
                    CustomTags = tags,
                    EnableHtmlEditing = enableHtmlEditing
                };
            }

            style.AutoUpdate = AutoUpdate;
            style.UpdateDisplay();

            return style;
        }
    }
}
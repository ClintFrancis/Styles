using System;
using System.Collections.Generic;
using UIKit;

namespace Styles.Text
{
    public class StyleManager : StyleManagerBase
    {
        public StyleManager()
        {
            Init(TextStyle.Default);
        }

        public StyleManager(ITextStyle instance)
        {
            Init(instance);
        }

        override protected IManagedStyle CreateViewStyle<T>(T target, string styleID, string text, List<CssTag> customTags, bool useExistingStyles = true, bool enableHtmlEditing = false)
        {
            var tags = customTags ?? CustomTags;
            var type = target.GetType();

            IManagedStyle style;
            if (type == TextStyle.typeLabel)
            {
                style = new LabelStyle(_instance, target as UILabel, styleID, text, true)
                {
                    CustomTags = tags,
                    EnableHtmlEditing = enableHtmlEditing
                };
            }

            else if (type == TextStyle.typeTextView)
            {
                style = new TextViewStyle(_instance, target as UITextView, styleID, text, true)
                {
                    CustomTags = tags,
                    EnableHtmlEditing = enableHtmlEditing
                };
            }

            else
            {
                style = new TextFieldStyle(_instance, target as UITextField, styleID, text, true)
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


using System;
using System.Collections.Generic;
using Foundation;
using UIKit;

namespace Styles.Text
{
    [Foundation.Preserve(AllMembers = true)]
    public class ManagedTextFieldStyle : ManagedStyleBase
    {
        public UITextField Target { get; set; }

        bool _enableHtmlEditing;
        public override bool EnableHtmlEditing
        {
            get { return _enableHtmlEditing; }
            set
            {
                _enableHtmlEditing = value;

                if (Target != null)
                {
                    if (value)
                    {
                        Target.EditingChanged += TextEditingChanged;
                        Target.EditingDidBegin += TextEditingStarted;
                        Target.EditingDidEnd += TextEditingEnded;

                    }
                    else
                    {
                        Target.EditingChanged -= TextEditingChanged;
                        Target.EditingDidBegin -= TextEditingStarted;
                        Target.EditingDidEnd -= TextEditingEnded;
                    }
                }
            }
        }

        public ManagedTextFieldStyle(ITextStyle instance, UITextField target, string styleID, string text, bool updateConstraints)
        {
            styleInstance = instance as TextStyle;
            _updateConstraints = updateConstraints;
            Target = target;

            StyleID = styleID;
            Text = !string.IsNullOrEmpty(text) ? text : Target.Text;
        }

        override public void UpdateFrame()
        {
            // Offset the frame if needed
            if (_updateConstraints && Style.LineHeight < 0f)
            {
                var heightOffset = Style.GetLineHeightOffset();
                var targetFrame = Target.Frame;
                targetFrame.Height = (nfloat)Math.Ceiling(targetFrame.Height) + heightOffset;

                if (Target.Constraints.Length > 0)
                {
                    foreach (var constraint in Target.Constraints)
                    {
                        if (constraint.FirstAttribute == NSLayoutAttribute.Height)
                        {
                            constraint.Constant = targetFrame.Height;
                            break;
                        }
                    }
                }
                else
                {
                    Target.Frame = targetFrame;
                }
            }
        }

        override public void UpdateDisplay()
        {
            if (IsDirty)
            {
                styleInstance.Style(Target, StyleID, Text, CustomTags, true);
                _isDirty = false;
            }
            else
                Target.AttributedText = AttributedValue;
        }

        override public void Dispose()
        {
            EnableHtmlEditing = false;
            Target = null;
        }

        void TextEditingChanged(object sender, EventArgs e)
        {
            if (Target.Focused)
                _rawText = Target.Text;
        }

        void TextEditingStarted(object sender, EventArgs e)
        {
            Target.Text = _rawText;
        }

        void TextEditingEnded(object sender, EventArgs e)
        {
            Text = Target.Text;
        }
    }
}


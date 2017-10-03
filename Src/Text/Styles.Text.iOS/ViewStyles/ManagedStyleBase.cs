using System;
using System.Collections.Generic;
using Foundation;

namespace Styles.Text
{
    public abstract class ManagedStyleBase : IManagedStyle
    {
        protected TextStyle styleInstance;
        protected string _rawText;
        protected bool _updateConstraints;
        protected bool _isDirty = true;
        TextStyleParameters _style;

        public bool IsDirty
        {
            get { return _isDirty; }
            set
            {
                _isDirty = value;
                _style = null;
                _attributedValue = null;

                if (AutoUpdate) UpdateDisplay();
            }
        }

        public bool AutoUpdate
        {
            get; set;
        }

        string _text;
        public string Text
        {
            get { return _text; }

            set
            {
                _rawText = value;
                ContainsHtml = (!String.IsNullOrEmpty(value) && Common.MatchHtmlTags.IsMatch(value));
                _text = TextStyle.ParseString(Style, _rawText);
                _attributedValue = null;

                if (AutoUpdate) UpdateDisplay();
            }
        }

        protected TextStyleParameters Style
        {
            get
            {
                if (_style == null)
                    _style = styleInstance.GetStyle(StyleID);

                return _style;
            }
        }

        string _styleID;
        public string StyleID
        {
            get { return _styleID; }
            set
            {
                _styleID = value;
                IsDirty = true;
            }
        }

        List<CssTag> _customTags;
        public List<CssTag> CustomTags
        {
            get
            {
                return _customTags;
            }
            set
            {
                _customTags = new List<CssTag>(value);
                _attributedValue = null;

                if (AutoUpdate) UpdateDisplay();
            }
        }

        NSAttributedString _attributedValue;
        public NSAttributedString AttributedValue
        {
            get
            {
                if (_attributedValue == null)
                    _attributedValue = ContainsHtml ? styleInstance.CreateHtmlString(_text, CustomTags) : styleInstance.CreateStyledString(Style, _text); ;

                return _attributedValue;
            }
        }

        public bool ContainsHtml { get; protected set; }

        public virtual bool EnableHtmlEditing { get; set; }

        public abstract void Dispose();

        public abstract void UpdateDisplay();

        public abstract void UpdateFrame();
    }
}

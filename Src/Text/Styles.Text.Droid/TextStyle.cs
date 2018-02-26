using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Android.Text;
using Android.Widget;
using Android.Graphics;
using Android.App;
using Android.Util;
using Android.Views;
using System.Text.RegularExpressions;
using System.Linq;

namespace Styles.Text
{
    public class TextStyle : TextStyleBase, ITextStyle
    {

        #region Parameters

        public static Dictionary<string, ITextStyle> Instances { get { return _instances; } }

        internal static Type typeTextView = typeof(TextView);
        internal static Type typeEditText = typeof(EditText);
        internal Dictionary<string, Typeface> _typeFaces;

        public static TextStyle Default
        {
            get
            {
                lock (padlock)
                {
                    if (!_instances.ContainsKey(DefaultID))
                    {
                        _instances[DefaultID] = new TextStyle();
                    }
                    return _instances[DefaultID] as TextStyle;
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TextStyles.Android.TextStyle"/> class.
        /// </summary>
        public TextStyle() : base(DefaultID)
        {
            _instances[this.Id] = this;
            _typeFaces = new Dictionary<string, Typeface>();
        }

        public TextStyle(string id) : base(id)
        {
            _instances[this.Id] = this;
            _typeFaces = new Dictionary<string, Typeface>();
        }

        #region Public Methods

        /// <summary>
        /// Adds the typeface.
        /// </summary>
        /// <param name="fontName">Font name.</param>
        /// <param name="font">TypeFace</param>
        public virtual void AddFont(string fontName, string fontUrl)
        {
            Typeface font = Typeface.CreateFromAsset(Application.Context.Assets, fontUrl);
            _typeFaces.Add(fontName, font);
        }

        /// <summary>
        /// Gets an instance of the font by font name.
        /// </summary>
        /// <returns>The font.</returns>
        /// <param name="fontName">Font name.</param>
        public virtual Typeface GetFont(string fontName)
        {
            Typeface font = null;
            _typeFaces.TryGetValue(fontName, out font);
            return font;
        }

        public ISpanned CreateHtmlString(string text, string defaultStyle, List<CssTag> customTags = null, bool mergeExistingStyles = true, bool includeExistingStyles = true)
        {
            var styles = customTags != null ? MergeStyles(defaultStyle, customTags, mergeExistingStyles) : _textStyles;
            if (!styles.ContainsKey(defaultStyle))
                styles.Add(defaultStyle, _textStyles[defaultStyle]);

            if (includeExistingStyles)
            {
                // Create a unique list of tags used within the text
                var tagList = new List<string>();
                string pattern = @"(?<=</?)([^ >/]+)";
                var matches = Regex.Matches(text, pattern);

                for (int i = 0; i < matches.Count; i++)
                    tagList.Add(matches[i].ToString());

                tagList = tagList.Distinct().ToList();

                // If the tag isnt already included, add it into the styles
                foreach (var tag in tagList)
                    if (_textStyles.ContainsKey(tag) && !styles.ContainsKey(tag))
                        styles.Add(tag, _textStyles[tag]);
            }

            var converter = new CustomHtmlParser(this, text, styles, defaultStyle);
            return converter.Convert();
        }

        Dictionary<string, TextStyleParameters> MergeStyles(string defaultStyleID, List<CssTag> customTags, bool mergeExistingStyles = true)
        {
            var customCSS = new StringBuilder();
            foreach (var customTag in customTags)
                customCSS.AppendLine(customTag.CSS);

            var customStyles = CssTextStyleParser.Parse(customCSS.ToString());
            var defaultStyle = _textStyles[defaultStyleID];
            if (defaultStyle == null)
                throw new Exception("Default Style ID not found: " + defaultStyleID);

            TextStyleParameters existingStyle;
            foreach (var style in customStyles)
            {

                if (mergeExistingStyles)
                {
                    _textStyles.TryGetValue(style.Key, out existingStyle);

                    if (existingStyle != null)
                    {
                        style.Value.Merge(existingStyle, false);
                    }
                    else
                    {
                        style.Value.Merge(defaultStyle, false);
                    }
                }

                // If no font, use the default one
                if (string.IsNullOrEmpty(style.Value.Font))
                {
                    style.Value.Font = defaultStyle.Font;
                }
            }

            return customStyles;
        }

        public ISpanned CreateStyledString(string styleID, string text, int startIndex = 0, int endIndex = -1)
        {
            var style = GetStyle(styleID);
            return CreateStyledString(style, text, startIndex, endIndex);
        }

        public ISpanned CreateStyledString(TextStyleParameters style, string text, int startIndex = 0, int endIndex = -1)
        {
            if (text.Length == 0)
                return null;

            if (endIndex == -1)
                endIndex = text.Length;

            if (startIndex >= endIndex)
                throw new Exception("Unable to create styled string, StartIndex is too high: " + startIndex);

            // Parse the text
            text = ParseString(style, text);

            // If we have HMTL
            if (!string.IsNullOrEmpty(text) && Common.MatchHtmlTags.IsMatch(text))
            {
                return CreateHtmlString(text, style.Name);
            }
            else
            {
                var builder = new SpannableStringBuilder(text);
                var font = GetFont(style.Font);
                var span = new CustomTypefaceSpan("", font, style);

                builder.SetSpan(span, startIndex, endIndex, SpanTypes.ExclusiveExclusive);
                return builder;
            }
        }

        public T Create<T>(string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true)
        {
            var target = Activator.CreateInstance<T>();

            Style(target, styleID, text);
            return target;
        }

        public void Style<T>(T target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true)
        {
            var style = GetStyle(styleID);
            var type = typeof(T);
            var textView = (type == typeTextView) ? target as TextView : target as EditText;
            if (textView == null)
            {
                throw new NotSupportedException("The specified type is not supported, please use a TextView or EditText: " + type.ToString());
            }

            text = text ?? textView.Text;
            var isHTML = (!string.IsNullOrEmpty(text) && Common.MatchHtmlTags.IsMatch(text));

            // Style the TextView
            if (isHTML)
            {
                StyleTextView(textView, style, true);
                var html = CreateHtmlString(text, styleID, customTags, useExistingStyles);
                textView.SetText(html, TextView.BufferType.Spannable);
            }
            else if (style.RequiresHtmlTags())
            {
                StyleTextView(textView, style, false);

                var builder = new SpannableStringBuilder(ParseString(style, text));
                var font = GetFont(style.Font);
                var span = new CustomTypefaceSpan("", font, style);

                builder.SetSpan(span, 0, builder.Length(), SpanTypes.ExclusiveExclusive);
                textView.SetText(builder, TextView.BufferType.Spannable);
            }
            else
            {
                StyleTextView(textView, style, true);
                textView.SetText(ParseString(style, text), TextView.BufferType.Normal);
            }
        }

        #endregion

        #region Private Methods

        // TODO implement this function for styling plain text views, for html based views perhaps not
        internal void StyleTextView(TextView target, TextStyleParameters style, bool isPlainText, bool ignoreHtml = false)
        {
            var fontSize = (style.FontSize <= 0f) ? DefaultTextSize : style.FontSize;
            target.SetTextSize(ComplexUnitType.Sp, fontSize);

            // Plain text fonts and colors
            if (isPlainText)
            {
                if (style.Color != ColorRGB.Empty)
                    target.SetTextColor(style.Color.ToNative());

                if (!String.IsNullOrEmpty(style.Font) && _typeFaces.ContainsKey(style.Font))
                {
                    target.Typeface = _typeFaces[style.Font];
                    target.PaintFlags = target.PaintFlags | PaintFlags.SubpixelText;
                }
            }

            // Lines
            if (style.Lines > 0)
                target.SetLines(style.Lines);

            if (style.BackgroundColor != ColorRGB.Empty)
                target.SetBackgroundColor(style.BackgroundColor.ToNative());

            // Does nothing on the string attribs, needs to be part of a NSMutableAttributedString
            if (Math.Abs(style.LineHeight) > 0)
                target.SetLineSpacing(0, style.LineHeight / fontSize);

            // Assing the text gravity
            target.TextAlignment = TextAlignment.Gravity;
            switch (style.TextAlign)
            {
                case CssAlign.Center:
                    target.Gravity = GravityFlags.CenterHorizontal;
                    break;
                case CssAlign.Justified:
                    target.Gravity = GravityFlags.FillHorizontal;
                    break;
                case CssAlign.Right:
                    target.Gravity = GravityFlags.Right;
                    break;
                default:
                    target.Gravity = GravityFlags.Left;
                    break;
            }

            // Padding
            target.SetPadding(
                (style.PaddingLeft > float.MinValue) ? (int)style.PaddingLeft : target.PaddingLeft,
                (style.PaddingTop > float.MinValue) ? (int)style.PaddingTop : target.PaddingTop,
                (style.PaddingRight > float.MinValue) ? (int)style.PaddingRight : target.PaddingRight,
                (style.PaddingBottom > float.MinValue) ? (int)style.PaddingBottom : target.PaddingBottom
            );

            // Overflow
            // TODO implement this fully
            switch (style.TextOverflow)
            {
                case CssTextOverflow.Ellipsis:
                    target.Ellipsize = TextUtils.TruncateAt.End;
                    break;
                default:
                    break;
            }
        }

        internal static string ParseString(TextStyleParameters style, string text)
        {
            // Text transformations
            if (!string.IsNullOrEmpty(text))
            {
                if (style.TextTransform != CssTextTransform.None)
                {
                    switch (style.TextTransform)
                    {
                        case CssTextTransform.UpperCase:
                            text = text.ToUpper();
                            break;
                        case CssTextTransform.LowerCase:
                            text = text.ToLower();
                            break;
                        case CssTextTransform.Capitalize:
                            text = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(text.ToLower());
                            break;
                    }
                }
            }

            return text;
        }

        /// <summary>
        /// Dummy method to ensure classes are included for the Linker
        /// </summary>
        /// <param name="injector">Injector.</param>
        /// <param name="textView">Text view.</param>
        /// <param name="textField">Text field.</param>
        private void LinkerInclude(TextView textView, EditText textField)
        {
            textView = new TextView(null);
            textField = new EditText(null);
        }

        #endregion
    }

    /// <summary>
    /// Text attributes range.
    /// </summary>
    public class TextAttributesRange
    {
        public int StartIndex;
        public int Length;
        public string Text;
    }
}


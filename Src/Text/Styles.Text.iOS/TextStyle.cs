using System;
using UIKit;
using Foundation;
using System.Collections.Generic;
using System.Globalization;

namespace Styles.Text
{
    [Foundation.Preserve(AllMembers = true)]
    public class TextStyle : TextStyleBase, ITextStyle
    {
        #region Parameters

        public static Dictionary<string, ITextStyle> Instances { get { return _instances; } }

        internal static Type typeLabel = typeof(UIKit.UILabel);
        internal static Type typeTextView = typeof(UIKit.UITextView);
        internal static Type typeTextField = typeof(UIKit.UITextField);

        public static TextStyle Default
        {
            get
            {
                lock (padlock)
                {
                    if (!_instances.ContainsKey(DefaultID))
                    {
                        _instances[DefaultID] = new TextStyle(DefaultID);
                    }
                    return _instances[DefaultID] as TextStyle;
                }
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TextStyles.iOS,TextStyle"/> class.
        /// </summary>
        public TextStyle(string id) : base(id)
        {
            _instances[this.Id] = this;
        }

        public TextStyle() : base(DefaultID)
        {
            _instances[this.Id] = this;
        }

        #region Public Methods

        /// <summary>
        /// Creates an NSAttibutedString html string using the custom tags for styling.
        /// </summary>
        /// <returns>NSAttibutedString</returns>
        /// <param name="text">Text to display including html tags</param>
        /// <param name="customTags">A list of custom <c>CSSTagStyle</c> instances that set the styling for the html</param>
        /// <param name="useExistingStyles">Existing CSS styles willl be used If set to <c>true</c></param>
        public NSAttributedString CreateHtmlString(string text, List<CssTag> customTags = null, bool useExistingStyles = true)
        {
            var error = new NSError();

            text = HtmlTextStyleParser.StyleString(text, _textStyles, customTags, useExistingStyles);

            var stringAttribs = new NSAttributedStringDocumentAttributes
            {
                DocumentType = NSDocumentType.HTML,
                StringEncoding = NSStringEncoding.UTF8
            };

            var htmlString = new NSAttributedString(text, stringAttribs, ref error);

            return htmlString;
        }

        /// <summary>
        /// Creates a styled string as an NSAttibutedString 
        /// </summary>
        /// <returns>NSMutableAttributedString</returns>
        /// <param name="styleID">The CSS selector name for the style</param>
        /// <param name="text">Text to style</param>
        /// <param name="startIndex">Style start index</param>
        /// <param name="endIndex">Style end index</param>
        public NSMutableAttributedString CreateStyledString(string styleID, string text, int startIndex = 0, int endIndex = -1)
        {
            var style = GetStyle(styleID);
            return CreateStyledString(style, text, startIndex, endIndex);
        }

        /// <summary>
        /// Creates a styled string as an NSAttibutedString 
        /// </summary>
        /// <returns>The styled string.</returns>
        /// <param name="style">TextStyleParameters for styling</param>
        /// <param name="text">Text to style</param>
        /// <param name="startIndex">Style start index</param>
        /// <param name="endIndex">Style end index</param>
        public NSMutableAttributedString CreateStyledString(TextStyleParameters style, string text, int startIndex = 0, int endIndex = -1)
        {
            var attribs = GetStringAttributes(style, DefaultTextSize);
            text = ParseString(style, text);

            if (endIndex == -1)
            {
                endIndex = text.Length;
            }

            var prettyString = new NSMutableAttributedString(text);
            prettyString.SetAttributes(attribs, new NSRange(startIndex, endIndex));

            return prettyString;
        }

        /// <summary>
        /// Creates and styles a new Text container (UIlabel, UITextView, UITextField)
        /// </summary>
        /// <param name="styleID">The CSS selector name for the style</param>
        /// <param name="text">Text to display including html tags</param>
        /// <param name="customTags">A list of custom <c>CSSTagStyle</c> instances that set the styling for the html</param>
        /// <param name="useExistingStyles">Existing CSS styles willl be used If set to <c>true</c></param>
        /// <param name="encoding">String encoding type</param>
        /// <typeparam name="T">Text container type (UIlabel, UITextView, UITextField)</typeparam>
        public T Create<T>(string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true)
        {
            var target = Activator.CreateInstance<T>();
            var type = typeof(T);
            if (type == typeLabel)
            {
                StyleUILabel(target as UILabel, styleID, text, customTags, useExistingStyles, true);
            }
            else if (type == typeTextView)
            {
                StyleUITextView(target as UITextView, styleID, text, customTags, useExistingStyles, true);
            }
            else if (type == typeTextField)
            {
                StyleUITextField(target as UITextField, styleID, text, customTags, useExistingStyles, true);
            }
            else
            {
                throw new NotSupportedException("The specified type is not supported, please use a UILabel, UITextView or UITextField: " + type.ToString());
            }

            return target;
        }

        /// <summary>
        /// Styles a text container (UIlabel, UITextView, UITextField)
        /// </summary>
        /// <param name="target">Target text container</param>
        /// <param name="styleID">The CSS selector name for the style</param>
        /// <param name="text">Text to display</param>
        /// <typeparam name="T">Text container type (UIlabel, UITextView, UITextField)</typeparam>
        public void Style<T>(T target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true)
        {
            var type = target.GetType();
            if (type == typeLabel)
            {
                StyleUILabel(target as UILabel, styleID, text, customTags, useExistingStyles, true);
            }
            else if (type == typeTextView)
            {
                StyleUITextView(target as UITextView, styleID, text, customTags, useExistingStyles, true);
            }
            else if (type == typeTextField)
            {
                StyleUITextField(target as UITextField, styleID, text, customTags, useExistingStyles, true);
            }
            else
            {
                throw new NotSupportedException("The specified type is not supported, please use a UILabel, UITextView or UITextField: " + type.ToString());
            }
        }

        // Style a UILable
        public void StyleUILabel(UILabel target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true, bool setFonts = true, bool ignoreHtml = false)
        {


            var style = GetStyle(styleID);
            if (style == null)
            {
                throw new NullReferenceException($"No style has been registerd with the id '{styleID}' on the '{this.Id}' Styles.Text instance ");
            }
            text = text ?? target.Text;

            if (!ignoreHtml && IsHtml(text))
            {
                var tags = (customTags != null) ? new List<CssTag>(customTags) : null;
                SetBaseStyle(style.Name, ref tags);
                target.AttributedText = CreateHtmlString(text, tags, useExistingStyles);
                return;
            }

            if (!string.IsNullOrEmpty(text))
                target.AttributedText = CreateStyledString(style, text);

            //-------------------------
            var attribs = GetStringAttributes(style, DefaultTextSize);
            target.TextColor = attribs.ForegroundColor;

            // If setting the font attributes
            if (setFonts)
            {
                target.Font = attribs.Font;
            }

            // Lines
            if (style.Lines > int.MinValue)
            {
                target.Lines = style.Lines;
            }

            // Text Alignment
            target.TextAlignment = GetAlignment(style.TextAlign);

            // Overflow
            switch (style.TextOverflow)
            {
                case CssTextOverflow.Ellipsis:
                    target.LineBreakMode = UILineBreakMode.TailTruncation;
                    break;
                case CssTextOverflow.Clip:
                    target.LineBreakMode = UILineBreakMode.Clip;
                    break;
                default:
                    target.LineBreakMode = UILineBreakMode.WordWrap;
                    break;
            }
        }

        // Style a UITextView
        public void StyleUITextView(UITextView target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true, bool setFonts = true, bool ignoreHtml = false)
        {
            var style = GetStyle(styleID);
            text = text ?? target.Text;

            if (!ignoreHtml && IsHtml(text))
            {
                var tags = (customTags != null) ? new List<CssTag>(customTags) : null;
                SetBaseStyle(style.Name, ref customTags);
                target.AttributedText = CreateHtmlString(text, customTags, useExistingStyles);
                return;
            }

            target.AttributedText = CreateStyledString(style, text);

            //-------------------------
            var attribs = GetStringAttributes(style, DefaultTextSize);
            target.Font = attribs.Font;

            // If setting the font attributes
            if (setFonts)
            {
                target.TextColor = attribs.ForegroundColor;
            }

            // Text Alignment
            target.TextAlignment = GetAlignment(style.TextAlign);

            // Padding
            if (style.Padding != null)
            {
                var padding = style.Padding;
                target.TextContainerInset = new UIEdgeInsets(padding[0], padding[1], padding[2], padding[3]);
            }
            else
            {
                var curInset = target.TextContainerInset;
                UpdateMargins(style, ref curInset);
                target.TextContainerInset = curInset;
            }
        }

        // Style a UITextField
        public void StyleUITextField(UITextField target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true, bool setFonts = true, bool ignoreHtml = false)
        {
            var style = GetStyle(styleID);
            text = text ?? target.Text;

            if (!ignoreHtml && IsHtml(text))
            {
                var tags = (customTags != null) ? new List<CssTag>(customTags) : null;
                SetBaseStyle(style.Name, ref customTags);
                target.AttributedText = CreateHtmlString(text, customTags, useExistingStyles);
                return;
            }

            target.AttributedText = CreateStyledString(style, text);

            //-------------------------
            var attribs = GetStringAttributes(style, DefaultTextSize);

            target.TextColor = attribs.ForegroundColor;

            // If setting the font attributes
            if (setFonts)
                target.Font = attribs.Font;

            target.TextAlignment = GetAlignment(style.TextAlign);

            // Padding
            if (style.Padding != null)
            {
                var padding = style.Padding;
                target.LayoutMargins = new UIEdgeInsets(padding[0], padding[1], padding[2], padding[3]);
            }
            else
            {
                var curInset = target.LayoutMargins;
                UpdateMargins(style, ref curInset);
                target.LayoutMargins = curInset;
            }
        }

        #endregion

        #region Private Methods

        internal bool IsHtml(string text)
        {
            return (!string.IsNullOrEmpty(text) && Common.MatchHtmlTags.IsMatch(text));
        }

        static void UpdateMargins(TextStyleParameters style, ref UIEdgeInsets inset)
        {
            inset.Top = (style.PaddingTop > float.MinValue) ? style.PaddingTop : inset.Top;
            inset.Bottom = (style.PaddingBottom > float.MinValue) ? style.PaddingBottom : inset.Bottom;
            inset.Left = (style.PaddingLeft > float.MinValue) ? style.PaddingLeft : inset.Left;
            inset.Right = (style.PaddingRight > float.MinValue) ? style.PaddingRight : inset.Right;
        }

        internal static UIStringAttributes GetStringAttributes(TextStyleParameters style, float defaultTextSize)
        {
            var stringAttribs = new UIStringAttributes();

            var fontSize = style.FontSize;
            if (fontSize <= 0f)
                fontSize = defaultTextSize;

            if (!string.IsNullOrEmpty(style.Font))
            {
                stringAttribs.Font = UIFont.FromName(style.Font, fontSize);
                if (stringAttribs.Font == null) throw new Exception($"Unable to find a registered system font with the name '{style.Font}'");
            }

            if (style.Color != ColorRGB.Empty)
                stringAttribs.ForegroundColor = style.Color.ToNative();

            if (style.Color != ColorRGB.Empty)
                stringAttribs.BackgroundColor = style.BackgroundColor.ToNative();

            if (style.LetterSpacing > 0f)
                stringAttribs.KerningAdjustment = style.LetterSpacing;

            // Does nothing on the string attribs, needs to be part of a NSMutableAttributedString
            if (style.LineHeight != 0f)
            {
                var paragraphStyle = new NSMutableParagraphStyle()
                {
                    LineHeightMultiple = style.LineHeight / fontSize,
                    Alignment = GetAlignment(style.TextAlign)
                };
                stringAttribs.ParagraphStyle = paragraphStyle;
            }

            if (style.TextDecoration != CssDecoration.None)
            {
                switch (style.TextDecoration)
                {
                    case CssDecoration.LineThrough:
                        stringAttribs.StrikethroughStyle = NSUnderlineStyle.Single;
                        break;
                    case CssDecoration.Underline:
                        stringAttribs.UnderlineStyle = NSUnderlineStyle.Single;
                        break;
                }

                if (!string.IsNullOrEmpty(style.TextDecorationColor))
                    stringAttribs.StrikethroughColor = UIColor.Clear.FromHex(style.TextDecorationColor);
            }


            return stringAttribs;
        }

        internal NSAttributedString ParseHtmlString(TextStyleParameters style, string text)
        {
            var attribs = GetStringAttributes(style, DefaultTextSize);
            text = ParseString(style, text);

            var prettyString = new NSMutableAttributedString(text);
            prettyString.AddAttributes(attribs, new NSRange(0, text.Length));
            return prettyString;
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
            else
            {
                text = "";
            }

            return text;
        }

        static UITextAlignment GetAlignment(CssAlign alignment)
        {
            switch (alignment)
            {
                case CssAlign.Left:
                    return UITextAlignment.Left;
                case CssAlign.Right:
                    return UITextAlignment.Right;
                case CssAlign.Center:
                    return UITextAlignment.Center;
                case CssAlign.Justified:
                    return UITextAlignment.Justified;
                default:
                    return UITextAlignment.Left;
            }
        }

        /// <summary>
        /// Dummy method to ensure classes are included for the Linker
        /// </summary>
        /// <param name="injector">Injector.</param>
        /// <param name="textView">Text view.</param>
        /// <param name="textField">Text field.</param>
        private void LinkerInclude(UILabel injector, UITextView textView, UITextField textField)
        {
            injector = new UILabel();
            textView = new UITextView();
            textField = new UITextField();
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
        public UIStringAttributes Attributes;
    }
}


using System;
using System.Collections.Generic;
using System.Text;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Text;
using Android.Text.Style;
using Java.IO;
using Org.Xml.Sax;
using Org.Xml.Sax.Helpers;

namespace Styles.Text
{
	public class CustomHtmlParser : Java.Lang.Object, IContentHandler
	{
		static float [] HEADER_SIZES = {
			1.5f, 1.4f, 1.3f, 1.2f, 1.1f, 1f,
		};

		readonly string _htmlSource;

		readonly SpannableStringBuilder _spannableStringBuilder;

		readonly Html.IImageGetter _imageGetter;

		readonly Html.ITagHandler _tagHandler;

		readonly IXMLReader _reader;

		readonly Dictionary<string, TextStyleParameters> _styles;

		static TextStyleParameters _defaultStyle;

		TextStyle _instance;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:TextStyles.Android.CustomHtmlParser"/> class.
		/// </summary>
		/// <param name="source">Source.</param>
		/// <param name="textStyles">Text styles.</param>
		/// <param name="defaultStyleID">Default style identifier.</param>
		public CustomHtmlParser (TextStyle instance, string source, Dictionary<string, TextStyleParameters> textStyles, string defaultStyleID = null)
		{
			_instance = instance;
			_htmlSource = source;
			_styles = textStyles;

			if (!String.IsNullOrEmpty (defaultStyleID) && _styles.ContainsKey (defaultStyleID)) {
				_defaultStyle = _styles [defaultStyleID];
			}

			_spannableStringBuilder = new SpannableStringBuilder ();
			_reader = XMLReaderFactory.CreateXMLReader ("org.ccil.cowan.tagsoup.Parser");
			_imageGetter = null;
			_tagHandler = new CustomTagHandler (_instance, _styles);
		}

		/// <summary>
		/// Convert the source to an ISpanned instance.
		/// </summary>
		public ISpanned Convert ()
		{
			_reader.ContentHandler = this;

			try {
				_reader.Parse (new InputSource (new StringReader (_htmlSource)));
			} catch (Exception e) {
				// We are reading from a string. There should not be IO problems.
				throw e;
			}

			// Fix flags and range for paragraph-type markup.
			int startIndex;
			int endIndex;

			var obj = _spannableStringBuilder.GetSpans (0, _spannableStringBuilder.Length (), Java.Lang.Class.FromType (typeof (IParagraphStyle)));
			for (int i = 0; i < obj.Length; i++) {
				startIndex = _spannableStringBuilder.GetSpanStart (obj [i]);
				endIndex = _spannableStringBuilder.GetSpanEnd (obj [i]);

				// If the last line of the range is blank, back off by one.
				if (endIndex - 2 >= 0) {
					if (_spannableStringBuilder.CharAt (endIndex - 1) == '\n' &&
						_spannableStringBuilder.CharAt (endIndex - 2) == '\n') {
						endIndex--;
					}
				}

				if (endIndex == startIndex) {
					_spannableStringBuilder.RemoveSpan (obj [i]);
				} else {
					_spannableStringBuilder.SetSpan (obj [i], startIndex, endIndex, SpanTypes.Paragraph);
				}
			}

			// loop through spans and apply text formating where needed
			if (_defaultStyle?.TextTransform != CssTextTransform.None) {
				var allSpans = _spannableStringBuilder.GetSpans (0, _spannableStringBuilder.Length (), Java.Lang.Class.FromType (typeof (Java.Lang.Object)));
				var hasDefaultStyle = (_defaultStyle != null);

				// Pre first span
				var firstSpanStart = allSpans.Length > 0 ? _spannableStringBuilder.GetSpanStart (allSpans [0]) : 0;
				if (hasDefaultStyle)
					TransformTextRange (_spannableStringBuilder, _defaultStyle, 0, firstSpanStart);

				// In between spans & last span
				for (int i = (hasDefaultStyle ? 1 : 0); i < allSpans.Length; i++) {
					startIndex = _spannableStringBuilder.GetSpanEnd (allSpans [i]);
					endIndex = (i + 1 < allSpans.Length) ? _spannableStringBuilder.GetSpanStart (allSpans [i + 1]) : _spannableStringBuilder.Length ();

					TransformTextRange (_spannableStringBuilder, _defaultStyle, startIndex, endIndex);
				}
			}

			return _spannableStringBuilder;
		}

		/// <summary>
		/// Transforms the text case within the given range.
		/// </summary>
		/// <returns>The text range.</returns>
		/// <param name="text">Text.</param>
		/// <param name="style">Style.</param>
		/// <param name="startIndex">Start index.</param>
		/// <param name="endIndex">End index.</param>
		void TransformTextRange (SpannableStringBuilder text, TextStyleParameters style, int startIndex, int endIndex)

		{
			if (startIndex == endIndex)
				return;

			var transformed = TextStyle.ParseString (style, text.SubSequence (startIndex, endIndex));
			text.Replace (startIndex, endIndex, transformed);
		}

		#region Methods in progress

		/// <summary>
		/// Handles the start of an HTML tag.
		/// </summary>
		/// <returns>The start tag.</returns>
		/// <param name="tag">Tag.</param>
		/// <param name="attributes">Attributes.</param>
		void handleStartTag (String tag, IAttributes attributes)
		{
			if (tag != _defaultStyle?.Name && _styles.ContainsKey (tag)) {
				_tagHandler.HandleTag (true, tag, _spannableStringBuilder, _reader);
			} else if (String.Equals (tag, "br", StringComparison.OrdinalIgnoreCase)) {
				// We don't need to handle this. TagSoup will ensure that there's a </br> for each <br>
				// so we can safely emite the linebreaks when we handle the close tag.
			} else if (String.Equals (tag, "p", StringComparison.OrdinalIgnoreCase)) {
				handleP (_spannableStringBuilder);
			} else if (String.Equals (tag, "div", StringComparison.OrdinalIgnoreCase)) {
				handleP (_spannableStringBuilder);
			} else if (String.Equals (tag, "strong", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Bold ());
			} else if (String.Equals (tag, "b", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Bold ());
			} else if (String.Equals (tag, "em", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Italic ());
			} else if (String.Equals (tag, "cite", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Italic ());
			} else if (String.Equals (tag, "dfn", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Italic ());
			} else if (String.Equals (tag, "i", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Italic ());
			} else if (String.Equals (tag, "big", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Big ());
			} else if (String.Equals (tag, "small", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Small ());
			} else if (String.Equals (tag, "font", StringComparison.OrdinalIgnoreCase)) {
				startFont (_spannableStringBuilder, attributes);
			} else if (String.Equals (tag, "blockquote", StringComparison.OrdinalIgnoreCase)) {
				handleP (_spannableStringBuilder);
				start (_spannableStringBuilder, new Blockquote ());
			} else if (String.Equals (tag, "tt", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Monospace ());
			} else if (String.Equals (tag, "a", StringComparison.OrdinalIgnoreCase)) {
				startA (_spannableStringBuilder, attributes);
			} else if (String.Equals (tag, "u", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Underline ());
			} else if (String.Equals (tag, "sup", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Super ());
			} else if (String.Equals (tag, "sub", StringComparison.OrdinalIgnoreCase)) {
				start (_spannableStringBuilder, new Sub ());
			} else if (tag.Length == 2 &&
					   Char.ToLower (tag [0]) == 'h' &&
					   tag [1] >= '1' && tag [1] <= '6') {
				handleP (_spannableStringBuilder);
				//				start (mSpannableStringBuilder, new Header (tag [1] - '1'));
			} else if (String.Equals (tag, "img", StringComparison.OrdinalIgnoreCase)) {
				startImg (_spannableStringBuilder, attributes, _imageGetter);
			}
		}

		/// <summary>
		/// Handles the end of an HTML tag.
		/// </summary>
		/// <returns>void</returns>
		/// <param name="tag">CSS Tag Name</param>
		void handleEndTag (String tag)
		{
			if (tag != _defaultStyle?.Name && _styles.ContainsKey (tag)) {
				_tagHandler.HandleTag (false, tag, _spannableStringBuilder, _reader);
			} else if (String.Equals (tag, "br", StringComparison.OrdinalIgnoreCase)) {
				handleBr (_spannableStringBuilder);
			} else if (String.Equals (tag, "p", StringComparison.OrdinalIgnoreCase)) {
				handleP (_spannableStringBuilder);
			} else if (String.Equals (tag, "div", StringComparison.OrdinalIgnoreCase)) {
				handleP (_spannableStringBuilder);
			} else if (String.Equals (tag, "strong", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Bold)), new StyleSpan (TypefaceStyle.Bold));
			} else if (String.Equals (tag, "b", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Bold)), new StyleSpan (TypefaceStyle.Bold));
			} else if (String.Equals (tag, "em", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Italic)), new StyleSpan (TypefaceStyle.Italic));
			} else if (String.Equals (tag, "cite", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Italic)), new StyleSpan (TypefaceStyle.Italic));
			} else if (String.Equals (tag, "dfn", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Italic)), new StyleSpan (TypefaceStyle.Italic));
			} else if (String.Equals (tag, "i", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Italic)), new StyleSpan (TypefaceStyle.Italic));
			} else if (String.Equals (tag, "big", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Big)), new RelativeSizeSpan (1.25f));
			} else if (String.Equals (tag, "small", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Small)), new RelativeSizeSpan (0.8f));
			} else if (String.Equals (tag, "font", StringComparison.OrdinalIgnoreCase)) {
				endFont (_spannableStringBuilder);
			} else if (String.Equals (tag, "blockquote", StringComparison.OrdinalIgnoreCase)) {
				handleP (_spannableStringBuilder);
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Blockquote)), new QuoteSpan ());
			} else if (String.Equals (tag, "tt", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Monospace)), new TypefaceSpan ("monospace"));
			} else if (String.Equals (tag, "a", StringComparison.OrdinalIgnoreCase)) {
				endA (_spannableStringBuilder);
			} else if (String.Equals (tag, "u", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Underline)), new UnderlineSpan ());
			} else if (String.Equals (tag, "sup", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Super)), new SuperscriptSpan ());
			} else if (String.Equals (tag, "sub", StringComparison.OrdinalIgnoreCase)) {
				end (_spannableStringBuilder, Java.Lang.Class.FromType (typeof (Sub)), new SubscriptSpan ());
			} else if (tag.Length == 2 &&
					   Char.ToLower (tag [0]) == 'h' &&
					   Char.ToLower (tag [1]) >= '1' && Char.ToLower (tag [1]) <= '6') {
				handleP (_spannableStringBuilder);
				endHeader (_spannableStringBuilder);
			}
		}

		/// <summary>
		/// Handles the P tag.
		/// </summary>
		/// <returns>void</returns>
		/// <param name="text">Text within the P Tag</param>
		static void handleP (SpannableStringBuilder text)
		{
			int len = text.Length ();

			if (len >= 1 && text.CharAt (len - 1) == '\n') {
				if (len >= 2 && text.CharAt (len - 2) == '\n')
					return;

				text.Append ("\n");
				return;
			}

			if (len != 0)
				text.Append ("\n\n");
		}

		/// <summary>
		/// Handles the br tag.
		/// </summary>
		/// <returns>void</returns>
		/// <param name="text">Inserts a break</param>
		static void handleBr (SpannableStringBuilder text)
		{
			text.Append ("\n");
		}

		/// <summary>
		/// Gets the last instance of a given Spanned Object
		/// </summary>
		/// <returns>The instance of a Spanned Object</returns>
		/// <param name="text">The text contained within the span</param>
		/// <param name="kind">Object type</param>
		static Java.Lang.Object getLast (ISpanned text, Java.Lang.Class kind)

		{
			/*
		 * This knows that the last returned object from getSpans()
		 * will be the most recently added.
		 */
			Java.Lang.Object [] objs = text.GetSpans (0, text.Length (), kind);

			if (objs.Length == 0) {
				return null;
			} else {
				return objs [objs.Length - 1];
			}
		}

		/// <summary>
		/// Specified the start of a span object
		/// <param name="text">String Builder instance</param>
		/// <param name="mark">Type of Object to use for the span</param>
		static void start (SpannableStringBuilder text, Java.Lang.Object mark)
		{
			int len = text.Length ();
			text.SetSpan (mark, len, len, SpanTypes.MarkMark);
		}

		/// <summary>
		/// Ends the span of an object
		/// </summary>
		/// <param name="text">String Builder instance</param>
		/// <param name="kind">Type of Object to use for the span</param>
		/// <param name="repl">Type of Object to replace for the span.</param>
		void end (SpannableStringBuilder text, Java.Lang.Class kind, Java.Lang.Object repl)
		{
			int len = text.Length ();
			Java.Lang.Object obj = getLast (text, kind);
			int start = text.GetSpanStart (obj);

			text.RemoveSpan (obj);

			if (start != len) {
				// Apply any text transforms here
				if (_defaultStyle.TextTransform != CssTextTransform.None)
					TransformTextRange (_spannableStringBuilder, _defaultStyle, start, len);

				text.SetSpan (repl, start, len, SpanTypes.ExclusiveExclusive);
			}
		}

		/// <summary>
		/// Handles IMG tags
		/// </summary>
		/// <returns>The image.</returns>
		/// <param name="text">Text.</param>
		/// <param name="attributes">Attributes.</param>
		/// <param name="img">Image.</param>
		static void startImg (SpannableStringBuilder text, IAttributes attributes, Html.IImageGetter img)
		{
			var src = attributes.GetValue ("src");
			Drawable d = null;

			if (img != null) {
				d = img.GetDrawable (src);
			}

			if (d == null) {
				throw new NotImplementedException ("Missing Inline image implementation");
				//				d = Resources.System.GetDrawable ();
				//					getDrawable(com.android.internal.R.drawable.unknown_image);

				//				d.SetBounds (0, 0, d.IntrinsicWidth, d.IntrinsicHeight);
			}

			int len = text.Length ();
			text.Append ("\uFFFC");

			text.SetSpan (new ImageSpan (d, src), len, text.Length (),
				SpanTypes.ExclusiveExclusive);

		}

		/// <summary>
		/// Starts a font tag
		/// </summary>
		/// <returns>void</returns>
		/// <param name="text">SpannableStringBuilder</param>
		/// <param name="attributes">IAttributes</param>
		static void startFont (SpannableStringBuilder text, IAttributes attributes)
		{
			String color = attributes.GetValue ("color");
			String face = attributes.GetValue ("face");

			int len = text.Length ();
			text.SetSpan (new Font (color, face), len, len, SpanTypes.MarkMark);
		}

		/// <summary>
		/// Ends a font tag.
		/// </summary>
		/// <returns>void</returns>
		/// <param name="text">SpannableStringBuilder</param>
		static void endFont (SpannableStringBuilder text)
		{
			int len = text.Length ();
			var obj = getLast (text, Java.Lang.Class.FromType (typeof (Font)));
			int where = text.GetSpanStart (obj);

			text.RemoveSpan (obj);

			if (where != len) {
				var f = (Font)obj;

				if (!TextUtils.IsEmpty (f.mColor)) {
					if (f.mColor.StartsWith ("@")) {
						Resources res = Resources.System;
						String name = f.mColor.Substring (1);
						int colorRes = res.GetIdentifier (name, "color", "android");
						if (colorRes != 0) {
							ColorStateList colors = res.GetColorStateList (colorRes);
							text.SetSpan (new TextAppearanceSpan (null, 0, 0, colors, null),
								where, len,
								SpanTypes.ExclusiveExclusive);
						}
					} else {
						var c = Android.Graphics.Color.ParseColor (f.mColor);
						//						if (c != null) {
						text.SetSpan (new ForegroundColorSpan (c),
							where, len,
							SpanTypes.ExclusiveExclusive);
						//						}
					}
				}

				if (f.mFace != null) {
					text.SetSpan (new TypefaceSpan (f.mFace), where, len,
						SpanTypes.ExclusiveExclusive);
				}
			}

		}

		/// <summary>
		/// Starts an A tag
		/// </summary>
		/// <returns>void</returns>
		/// <param name="text">SpannableStringBuilder</param>
		/// <param name="attributes">IAttributes</param>
		static void startA (SpannableStringBuilder text, IAttributes attributes)

		{
			String href = attributes.GetValue ("href");

			int len = text.Length ();
			text.SetSpan (new Href (href), len, len, SpanTypes.MarkMark);
		}

		/// <summary>
		/// Ends an A tag.
		/// </summary>
		/// <returns>void</returns>
		/// <param name="text">SpannableStringBuilder</param>
		static void endA (SpannableStringBuilder text)
		{
			int len = text.Length ();
			var obj = getLast (text, Java.Lang.Class.FromType (typeof (Href)));
			int where = text.GetSpanStart (obj);

			text.RemoveSpan (obj);

			if (where != len) {
				Href h = (Href)obj;

				if (h.mHref != null) {
					text.SetSpan (new URLSpan (h.mHref), where, len,
						SpanTypes.ExclusiveExclusive);
				}
			}
		}

		/// <summary>
		/// Ends a Header tag.
		/// </summary>
		/// <returns>void</returns>
		/// <param name="text">SpannableStringBuilder</param>
		static void endHeader (SpannableStringBuilder text)
		{
			int len = text.Length ();
			var obj = getLast (text, Java.Lang.Class.FromType (typeof (Header)));
			int where = text.GetSpanStart (obj);

			text.RemoveSpan (obj);

			// Back off not to change only the text, not the blank line.
			while (len > where && text.CharAt (len - 1) == '\n') {
				len--;
			}

			if (where != len) {
				Header h = (Header)obj;

				text.SetSpan (new RelativeSizeSpan (HEADER_SIZES [h.mLevel]),
					where, len, SpanTypes.ExclusiveExclusive);
				text.SetSpan (new StyleSpan (TypefaceStyle.Bold),
					where, len, SpanTypes.ExclusiveExclusive);
			}
		}

		#endregion

		#region IContentHandler implementation

		public void SetDocumentLocator (ILocator locator)
		{
		}

		public void StartDocument ()
		{
			System.Console.WriteLine (_spannableStringBuilder.ToString ());
		}

		public void EndDocument ()
		{

		}

		public void StartPrefixMapping (string prefix, string uri)
		{
		}

		public void EndPrefixMapping (string prefix)
		{
		}

		public void StartElement (string uri, string localName, string qName, IAttributes atts)
		{
			handleStartTag (localName, atts);
		}

		public void EndElement (string uri, string localName, string qName)
		{
			handleEndTag (localName);
		}

		public void Characters (char [] ch, int start, int length)
		{
			StringBuilder sb = new StringBuilder ();

			/*
			 * Ignore whitespace that immediately follows other whitespace;
			 * newlines count as spaces.
			 */

			for (int i = 0; i < length; i++) {
				char c = ch [i + start];

				if (c == ' ' || c == '\n') {
					char pred;
					int len = sb.Length;

					if (len == 0) {
						len = _spannableStringBuilder.Length ();

						if (len == 0) {
							pred = '\n';
						} else {
							pred = _spannableStringBuilder.CharAt (len - 1);
						}
					} else {
						pred = sb [len - 1];
					}

					if (pred != ' ' && pred != '\n') {
						sb.Append (' ');
					}
				} else {
					sb.Append (c);
				}
			}

			_spannableStringBuilder.Append (sb.ToString ());
		}

		public void IgnorableWhitespace (char [] ch, int start, int length)
		{
		}

		public void ProcessingInstruction (string target, string data)
		{
		}

		public void SkippedEntity (string name)
		{
		}

		#endregion

		#region internal classes

		class HtmlTag : Java.Lang.Object
		{
		}

		class Bold : Java.Lang.Object
		{
		}

		class Italic : Java.Lang.Object
		{
		}

		class Underline : Java.Lang.Object
		{
		}

		class Big : Java.Lang.Object
		{
		}

		class Small : Java.Lang.Object
		{
		}

		class Monospace : Java.Lang.Object
		{
		}

		class Blockquote : Java.Lang.Object
		{
		}

		class Super : Java.Lang.Object
		{
		}

		class Sub : Java.Lang.Object
		{
		}

		class Font : Java.Lang.Object
		{
			public String mColor;
			public String mFace;

			public Font (String color, String face)
			{
				mColor = color;
				mFace = face;
			}
		}

		class Href : Java.Lang.Object
		{
			public String mHref;

			public Href (String href)
			{
				mHref = href;
			}
		}

		class Header : Java.Lang.Object
		{
			internal int mLevel;

			public Header (int level)
			{
				mLevel = level;
			}
		}

		#endregion

		#region IDisposable implementation

		//		public void Dispose ()
		//		{
		//			throw new NotImplementedException ();
		//		}

		#endregion

		#region IJavaObject implementation

		//		public IntPtr Handle {
		//			get {
		//				throw new NotImplementedException ();
		//			}
		//		}

		#endregion
	}
}


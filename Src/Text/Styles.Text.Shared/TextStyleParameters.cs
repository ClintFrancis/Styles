using System;
using System.Reflection;
using System.Linq;

namespace Styles.Text
{
	/// <summary>
	/// Text style parameters.
	/// </summary>
	public class TextStyleParameters : CssParameters
	{
		#region CSS Properties

		/// <summary>
		/// Sets all the font properties in one declaration
		/// </summary>
		/// <value>The font.</value>
		[CssAttribute("font-family")]
		public string Font { get; set; }

		/// <summary>
		/// Specifies the font size of text
		/// </summary>
		/// <value>The size of the font.</value>
		[CssAttribute("font-size")]
		public float FontSize { get; set; }

		/// <summary>
		///  Specifies the font style
		/// </summary>
		/// <value>The font style.</value>
		[CssAttribute("font-style")]
		public CssFontStyle FontStyle { get; set; }

		/// <summary>
		///  Specifies the font weight
		/// </summary>
		/// <value>The font style.</value>
		[CssAttribute("font-weight")]
		public CssFontWeight FontWeight { get; set; }

		/// <summary>
		/// Increases or decreases the space between characters in a text
		/// </summary>
		/// <value>The letter spacing.</value>
		[CssAttribute("letter-spacing")]
		public float LetterSpacing { get; set; }

		/// <summary>
		/// line-height	Sets the line height
		/// </summary>
		/// <value>The height of the line.</value>
		[CssAttribute("line-height")]
		public float LineHeight { get; set; }

		/// <summary>
		/// Specifies the horizontal alignment of text
		/// </summary>
		/// <value>The text align.</value>
		[CssAttribute("text-align")]
		public CssAlign TextAlign { get; set; }

		/// <summary>
		/// Specifies the decoration added to text
		/// </summary>
		/// <value>The text decoration.</value>
		[CssAttribute("text-decoration")]
		public CssDecoration TextDecoration { get; set; }

		/// <summary>
		/// Specifies the indentation of the first line in a text-block
		/// </summary>
		/// <value>The text indent.</value>
		[CssAttribute("text-indent")]
		public float TextIndent { get; set; }

		/// <summary>
		/// Specifies the how excess text is displayed
		/// </summary>
		/// <value>The text overflow.</value>
		[CssAttribute("text-overflow")]
		public CssTextOverflow TextOverflow { get; set; }

		/// <summary>
		/// Controls the capitalization of text
		/// </summary>
		/// <value>The text transform.</value>
		[CssAttribute("text-transform")]
		public CssTextTransform TextTransform { get; set; }

		#endregion

		#region Custom Properties

		/// <summary>
		/// Custom CSS atribute that specifies the number of lines
		/// </summary>
		/// <value>The lines.</value>
		[CssAttribute("lines")]
		public int Lines { get; set; }

		/// <summary>
		/// Specifies the color of the decoration added to text
		/// </summary>
		/// <value>The color of the text decoration.</value>
		[CssAttribute("text-decoration-color")]
		public string TextDecorationColor { get; set; }

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Occur.TextStyles.Core.TextStyleParameters"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		public TextStyleParameters(string name) : base(name)
		{
			Lines = int.MinValue;
		}

		/// <summary>
		/// Gets the line height offset.
		/// </summary>
		/// <returns>The line height offset.</returns>
		public float GetLineHeightOffset()
		{
			return FontSize - LineHeight;
		}

		/// <summary>
		/// Merge the specified style with this instance and optionally ovwerite any conflicting parameters
		/// </summary>
		/// <param name="style">Source.</param>
		/// <param name="overwriteExisting">Overwrite existing.</param>
		public void Merge(TextStyleParameters style, bool overwriteExisting)
		{
			Type t = typeof(TextStyleParameters);

			var properties = t.GetRuntimeProperties().Where(prop => prop.CanRead && prop.CanWrite);

			foreach (var prop in properties)
			{
				var sourceValue = prop.GetValue(style, null);

				if (sourceValue != null)
				{
					var targetValue = prop.GetValue(this, null);

					switch (prop.Name)
					{
						case "TextAlign":
							if ((CssAlign)sourceValue != CssAlign.Left)
								targetValue = null;
							break;
						case "TextDecoration":
							if ((CssDecoration)sourceValue != CssDecoration.None)
								targetValue = null;
							break;
						case "TextOverflow":
							if ((CssTextOverflow)sourceValue != CssTextOverflow.None)
								targetValue = null;
							break;
						case "TextTransform":
							if ((CssTextTransform)sourceValue != CssTextTransform.None)
								targetValue = null;
							break;
					}

					if (targetValue != null && !overwriteExisting)
						continue;

					prop.SetValue(this, sourceValue, null);
				}
			}
		}

		/// <summary>
		/// Clone this instance.
		/// </summary>
		public TextStyleParameters Clone()
		{
			return (TextStyleParameters)MemberwiseClone();
		}
	}
}


using System;

namespace Styles.Text
{
	public static class TextStyleParametersExtension
	{
		public static bool RequiresHtmlTags (this TextStyleParameters target)
		{
			if (target.TextDecoration != CssDecoration.None)
				return true;

			if (Math.Abs (target.LetterSpacing) > 0)
				return true;

			if (target.FontStyle == CssFontStyle.Italic)
				return true;

			if (target.FontWeight == CssFontWeight.Bold)
				return true;

			return false;
		}
	}
}


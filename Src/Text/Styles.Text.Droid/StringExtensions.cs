using System.Collections.Generic;
using Android.Text;
using Styles;
using Styles.Text;

namespace System
{
	public static class StringExtensions
	{
		// Convert an HTML string to NSAttributedString

		public static ISpanned ToSpannedString (this string target, string defaultStyle, List<CssTag> customStyles = null, bool useExisting = true)
		{
			return TextStyle.Default.CreateHtmlString (target, defaultStyle, customStyles, useExisting);
		}

		public static ISpanned ToSpannedString (this string target, TextStyle instance, string defaultStyle, List<CssTag> customStyles = null, bool useExisting = true)
		{
			return instance.CreateHtmlString (target, defaultStyle, customStyles, useExisting);
		}
	}
}


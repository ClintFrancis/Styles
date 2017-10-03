using System;
using Foundation;
using System.Collections.Generic;
using Styles.Text;
using Styles;

namespace System
{
	[Foundation.Preserve (AllMembers = true)]
	public static class StringExtensions
	{
		// Convert an HTML string to NSAttributedString

		public static NSAttributedString ToAttributedString (this string target, List<CssTag> customStyles = null, bool useExisting = true)
		{
			return TextStyle.Default.CreateHtmlString (target, customStyles, useExisting);
		}

		public static NSAttributedString ToAttributedString (this string target, TextStyle instance, List<CssTag> customStyles = null, bool useExisting = true)
		{
			return instance.CreateHtmlString (target, customStyles, useExisting);
		}
	}
}


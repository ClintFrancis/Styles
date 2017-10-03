using System;
using System.Collections.Generic;

namespace Styles.Text
{
	public interface ITextStyle
	{
		void SetCSS(string css);

		void SetStyles(Dictionary<string, TextStyleParameters> styles);

		List<TextStyleParameters> GetStyles();

		TextStyleParameters GetStyle(string selector);

		void Refresh();

		void Dispose();

		void SetBaseStyle(string baseStyleID, ref List<CssTag> customTags);

		T Create<T>(string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true);

		void Style<T>(T target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true);

		event EventHandler StylesChanged;
	}
}


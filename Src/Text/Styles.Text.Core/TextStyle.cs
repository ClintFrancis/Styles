using System;
using System.Collections.Generic;

namespace Styles.Text
{
    /// <summary>
    /// Bait and switch TextStyle class for PCL instances
    /// </summary>
	public class TextStyle : ITextStyle
    {
        public static Dictionary<string, ITextStyle> Instances { get { return null; } }

        public static TextStyle Default
        {
            get
            {
                return null;
            }
        }

        public TextStyle(string id)
        {
        }

        public TextStyle()
        {
            throw new Exception();
        }

        public event EventHandler StylesChanged;

        public T Create<T>(string styleID, string text = "", List<CssTag> customTags = null, bool useExistingStyles = true)
        {
            return default(T);
        }

        public void Dispose()
        {
            //
        }

        public TextStyleParameters GetStyle(string selector)
        {
            return null;
        }

        public List<TextStyleParameters> GetStyles()
        {
            return null;
        }

        public void Refresh()
        {

        }

        public void SetBaseStyle(string baseStyleID, ref List<CssTag> customTags)
        {

        }

        public void SetCSS(string css)
        {

        }

        public void SetStyles(Dictionary<string, TextStyleParameters> styles)
        {

        }

        public void Style<T>(T target, string styleID, string text = null, List<CssTag> customTags = null, bool useExistingStyles = true)
        {

        }
    }
}

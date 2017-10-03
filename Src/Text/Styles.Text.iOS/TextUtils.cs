using System;
using UIKit;

namespace Styles.Text
{
	public static class TextUtils
	{
		public static void ListFontNames (string search = "")
		{
			Console.WriteLine ("---- Font Names ----");
			var familyNames = UIFont.FamilyNames;
			foreach (var family in familyNames) {
				var fonts = UIFont.FontNamesForFamilyName (family);
				foreach (var font in fonts) {
					if (!string.IsNullOrEmpty (search)) {
						if (font.ToLower ().Contains (search.ToLower ())) Console.WriteLine (font);
					} else {
						Console.WriteLine (font);
					}
				}
			}
			Console.WriteLine ("--------");
		}
	}
}


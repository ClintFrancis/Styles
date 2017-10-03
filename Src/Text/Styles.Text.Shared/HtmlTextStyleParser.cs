using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace Styles.Text
{
	public static class HtmlTextStyleParser
	{
		/// <summary>
		/// The default body tag.
		/// </summary>
		public static string BODYTAG = "body";

		/// <summary>
		/// The tag names regex.
		/// </summary>
		static readonly Regex tagNamesRegex = new Regex(@"<(\w+)+.*?>");

		/// <summary>
		/// Styles the string adding all necessary HTML information
		/// </summary>
		/// <returns>Formatted HTML string</returns>
		/// <param name="source">Source text</param>
		/// <param name="textStyles">Dictionary of TextStyles</param>
		/// <param name="customTags">Custom CSS tags.</param>
		/// <param name="useExistingStyles">If set to <c>true</c> use existing styles.</param>
		public static string StyleString(string source, Dictionary<string, TextStyleParameters> textStyles, List<CssTag> customTags, bool useExistingStyles)
		{
			// TODO move this into the iOS library as it creates HTML specific to iOS / OR update this so it displays full HTML?
			var styles = @"<style>";
			var specifiedTags = new List<string>();

			if (customTags != null)
			{
				foreach (var customTag in customTags)
				{
					if (!string.IsNullOrEmpty(customTag.Name))
					{
						var curStyle = textStyles[customTag.Name];

						if (curStyle != null)
						{
							// Append any CSS to the style
							if (!String.IsNullOrEmpty(customTag.CSS))
								curStyle = CssTextStyleParser.MergeRule(curStyle, customTag.CSS, true);

							styles += CssTextStyleParser.ParseToCSSString(customTag.Tag, curStyle);
							specifiedTags.Add(customTag.Tag);
						}
					}
					else if (!string.IsNullOrEmpty(customTag.CSS))
					{

						// See if this style exists already, if it does CLONE the TextStyleParameter and merge the new CSS in

						styles += customTag.CSS;
						specifiedTags.Add(customTag.Tag);
					}
				}
			}

			// Update any missing tags with existing styles
			if (useExistingStyles)
			{
				// Find all tags specified in the source
				var regexResults = tagNamesRegex.Matches(source);
				var tagNames = regexResults.Cast<Match>()
					.Where(x => x.Groups.Count > 1)
					.Select(x => x.Groups[1].Value)
					.Distinct().ToList();

				var missingTags = tagNames.Except(specifiedTags);

				foreach (var tagName in missingTags)
				{
					if (textStyles.ContainsKey(tagName))
					{
						var curStyle = textStyles[tagName];
						var newStyle = CssTextStyleParser.ParseToCSSString(tagName, curStyle);
						styles += newStyle;
					}
				}

				// Add body if not declared
				var bodyTagExists = customTags != null && customTags.Any(x => x.Tag == BODYTAG);
				if (!bodyTagExists && textStyles.ContainsKey(BODYTAG))
				{
					var bodyStyle = textStyles[BODYTAG];
					var bodyCSS = CssTextStyleParser.ParseToCSSString(BODYTAG, bodyStyle);
					styles += bodyCSS;
				}
			}

			styles += @"</style>";
			source += styles;

			return source;
		}
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Styles
{
	// TODO Depreciate
	public interface ICustomParser<T>
	{
		Dictionary<string, T> Parse(CssRuleSet ruleSet);

		T MergeRule(T curStyle, string css, bool clone);

		void ParseCSSRule(ref T curStyle, CssParserRule rule, Dictionary<string, string> cssVariables);

		string ParseToCSSString(string tagName, T style);
	}

	// TODO Depreciate
	public abstract class CustomCssParser<T> where T : CssParameters
	{
		protected Dictionary<string, PropertyInfo> customProperties;

		public CustomCssParser()
		{
			// Dictionary of all properties found on TextStyleParameters
			customProperties = ParserUtils.GetCssAttributes<T>();
		}

		public Dictionary<string, T> Parse(CssRuleSet ruleSet)
		{
			var parsedStyles = new Dictionary<string, T>();

			// Process all the rules
			foreach (var rule in ruleSet.Rules)
			{
				// Process each selector
				foreach (var selector in rule.Selectors)
				{
					// If it doesnt exist, create it
					if (!parsedStyles.ContainsKey(selector))
						parsedStyles[selector] = (T)Activator.CreateInstance(typeof(T), selector);

					var curStyle = parsedStyles[selector];
					ParseCSSRule(ref curStyle, rule, ruleSet.Variables);
				}
			}

			return parsedStyles;
		}

		public abstract T MergeRule(T curStyle, string css, bool clone);

		public abstract void ParseCSSRule(ref T curStyle, CssParserRule rule, Dictionary<string, string> cssVariables);

		public abstract string ParseToCSSString(string tagName, T style);
	}
}


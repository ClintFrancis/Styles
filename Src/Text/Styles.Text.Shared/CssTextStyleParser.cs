using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Styles.Text
{
    public static class CssTextStyleParser
    {
        // Dictionary of all properties found on TextStyleParameters
        static Dictionary<string, PropertyInfo> _textStyleProperties = ParserUtils.GetCssAttributes<TextStyleParameters>();

        /// <summary>
        /// Parses the specified CSS
        /// </summary>
        /// <param name="css">Css.</param>
        public static Dictionary<string, TextStyleParameters> Parse(string css)
        {
            var rules = new CssParser().ParseAll(css);
            var ruleset = new CssRuleSet(rules);

            return Parse(ruleset);
        }

        public static Dictionary<string, TextStyleParameters> Parse(CssRuleSet ruleSet)
        {
            var textStyles = new Dictionary<string, TextStyleParameters>();

            // Process all the rules
            foreach (var rule in ruleSet.Rules)
            {
                // Process each rule
                foreach (var selector in rule.Selectors)
                {
                    // If it doesnt exist, create it
                    if (!textStyles.ContainsKey(selector))
                        textStyles[selector] = new TextStyleParameters(selector);

                    var curStyle = textStyles[selector];
                    ParseCSSRule(ref curStyle, rule, ruleSet.Variables);
                }

            }

            return textStyles;
        }

        /// <summary>
        /// Merges a css rule.
        /// </summary>
        /// <returns>The rule.</returns>
        /// <param name="curStyle">Target TextStyleParameters</param>
        /// <param name="css">Css value</param>
        /// <param name="clone">If set to <c>true</c> clone the style</param>
        public static TextStyleParameters MergeRule(TextStyleParameters curStyle, string css, bool clone)
        {
            var parser = new CssParser();
            var rules = parser.ParseAll(css);
            if (rules.Count() != 1)
            {
                throw new NotSupportedException("Only a single css class may be merged at a time");
            }

            var mergedStyle = clone ? curStyle.Clone() : curStyle;

            var rule = rules.FirstOrDefault();
            if (rule != null)
            {
                ParseCSSRule(ref mergedStyle, rule, null);
            }

            return mergedStyle;
        }

        /// <summary>
        /// Parses the CSS rule.
        /// </summary>
        /// <param name="curStyle">Current style.</param>
        /// <param name="rule">Rule.</param>
        internal static void ParseCSSRule(ref TextStyleParameters curStyle, CssParserRule rule, Dictionary<string, string> cssVariables)
        {
            foreach (var declaration in rule.Declarations)
            {

                if (_textStyleProperties.ContainsKey(declaration.Property))
                {

                    // Assign the variable if it exists
                    if (cssVariables != null && declaration.ReferencesVariable)
                    {
                        declaration.Value = cssVariables[declaration.Value];
                    }

                    var cleanedValue = declaration.Value.Replace("\"", "");
                    cleanedValue = cleanedValue.Trim();

                    var prop = _textStyleProperties[declaration.Property];
                    switch (prop.PropertyType.Name)
                    {
                        case "String":
                            curStyle.SetValue(prop.Name, cleanedValue);
                            break;
                        case "Int32":
                            int numInt;
                            if (int.TryParse(cleanedValue, out numInt))
                            {
                                curStyle.SetValue(prop.Name, numInt);
                            }
                            break;
                        case "Single":
                            cleanedValue = cleanedValue.Replace("px", "");
                            float numFloat;
                            if (float.TryParse(cleanedValue, out numFloat))
                            {
                                curStyle.SetValue(prop.Name, numFloat);
                            }
                            else
                                throw new Exception("Failed to Parse Single value " + cleanedValue);
                            break;
                        case "Single[]":
                            var parts = cleanedValue.Split(new char[] { ' ', ',' }, StringSplitOptions.RemoveEmptyEntries);
                            var parsedValues = new float[parts.Length];
                            for (int i = 0; i < parts.Length; i++)
                            {
                                float numArrayFloat;
                                if (float.TryParse(parts[i], out numArrayFloat))
                                {
                                    parsedValues[i] = numArrayFloat;
                                }
                            }
                            curStyle.SetValue(prop.Name, parsedValues);
                            break;
                        case "ColorRGB":
                            curStyle.SetValue(prop.Name, ColorRGB.FromHex(cleanedValue));
                            break;
                        case "CssAlign":
                            curStyle.TextAlign = EnumUtils.FromDescription<CssAlign>(cleanedValue);
                            break;
                        case "CssDecoration":
                            curStyle.TextDecoration = EnumUtils.FromDescription<CssDecoration>(cleanedValue);
                            break;
                        case "CssTextTransform":
                            curStyle.TextTransform = EnumUtils.FromDescription<CssTextTransform>(cleanedValue);
                            break;
                        case "CssTextOverflow":
                            curStyle.TextOverflow = EnumUtils.FromDescription<CssTextOverflow>(cleanedValue);
                            break;
                        case "CssFontStyle":
                            curStyle.FontStyle = EnumUtils.FromDescription<CssFontStyle>(cleanedValue);
                            break;
                        case "CssFontWeight":
                            curStyle.FontWeight = EnumUtils.FromDescription<CssFontWeight>(cleanedValue);
                            break;
                        default:
                            throw new InvalidCastException("Could not find the appropriate type " + prop.PropertyType.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Parses to CSS string.
        /// </summary>
        /// <returns>The to CSS string.</returns>
        /// <param name="tagName">Tag name.</param>
        /// <param name="style">Style.</param>
        public static string ParseToCSSString(string tagName, TextStyleParameters style)
        {
            var builder = new StringBuilder();
            builder.Append(tagName + "{");

            string cast;
            var runtimeProperties = style.GetType().GetRuntimeProperties();
            foreach (var prop in runtimeProperties)
            {
                try
                {
                    var value = prop.GetValue(style);

                    if (value != null)
                    {
                        string parsedValue = null;
                        switch (prop.PropertyType.Name)
                        {
                            case "String":
                                if ((value as string).StartsWith("#"))
                                    parsedValue = (string)value;
                                else
                                    parsedValue = "'" + value + "'";
                                break;
                            case "Single":
                                if (Convert.ToSingle(value) > float.MinValue)
                                {
                                    parsedValue = Convert.ToString(value);
                                    if (prop.Name == "FontSize") // Dirty, I really need a list of things that can be set in pixel values
                                        parsedValue += "px";
                                }
                                break;
                            case "Int32":
                                if (Convert.ToInt32(value) > int.MinValue)
                                    parsedValue = Convert.ToString(value);
                                break;
                            case "Single[]":
                                parsedValue = Convert.ToString(value);
                                break;
                            case "CssAlign":
                            case "CssDecoration":
                            case "CssTextTransform":
                            case "CssTextOverflow":
                                cast = Convert.ToString(value);
                                if (cast != "None")
                                    parsedValue = cast.ToLower();
                                break;
                            case "CssFontStyle":
                            case "CssFontWeight":
                                cast = Convert.ToString(value);
                                if (cast != "Normal")
                                    parsedValue = cast.ToLower();
                                break;
                            case "ColorRGB":
                                var colorRGB = (ColorRGB)value;
                                cast = colorRGB.ToString();
                                break;
                            default:
                                throw new InvalidCastException("Could not find the appropriate type " + prop.PropertyType.Name);
                        }

                        var attributes = (CssAttribute[])prop.GetCustomAttributes(
                                             typeof(CssAttribute), false);

                        if (attributes.Length > 0 && parsedValue != null)
                            builder.Append(attributes[0].Name + ":" + parsedValue + ";");
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            builder.Append("}");

            return builder.ToString();
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Styles
{
	public static class ParserUtils
	{
		public static Dictionary<string, PropertyInfo> GetCssAttributes<T>() where T:CssParameters{

			// Dictionary of all properties found on TextStyleParameters
			Dictionary<string, PropertyInfo> customProperties = typeof(T).GetRuntimeProperties()
				.Select(p => new { p, attr = p.GetCustomAttributes(typeof(CssAttribute), true) })
				.Where(prop => prop.attr.Count() == 1)
				.Select(obj => new { Property = obj.p, Attribute = obj.attr.First() as CssAttribute })
				.ToDictionary(t => t.Attribute.Name, t => t.Property);

			return customProperties;
		}
	}
}

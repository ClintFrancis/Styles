using System;
using System.Reflection;
using System.Linq;

namespace Styles
{
	public static class EnumUtils
	{
		public static T FromDescription<T>(string description)
		{
			Type t = typeof(T);
			foreach (FieldInfo fi in t.GetRuntimeFields())
			{
				object[] attrs = fi.GetCustomAttributes(typeof(CssAttribute), true).ToArray();
				if (attrs != null && attrs.Length > 0)
				{
					foreach (CssAttribute attr in attrs)
					{
						if (attr.Name.Equals(description))
							return (T)fi.GetValue(null);
					}
				}
			}
			return default(T);
		}
	}
}


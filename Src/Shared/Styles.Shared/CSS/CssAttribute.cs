using System;
namespace Styles
{
	/// <summary>
	/// Css attribute.
	/// </summary>
	[System.AttributeUsage(System.AttributeTargets.All)]
	public class CssAttribute : System.Attribute
	{
		/// <summary>
		/// Name of the CSS selector
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Occur.TextStyles.Core.CssAttribute"/> class.
		/// </summary>
		/// <param name="name">Name.</param>
		public CssAttribute(string name)
		{
			this.Name = name;
		}
	}
}


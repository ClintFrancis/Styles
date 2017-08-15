using System;
using System.Reflection;

namespace Styles
{
	public class CssParameters : Object
	{
		/// <summary>
		/// Sets the color
		/// </summary>
		/// <value>The color.</value>
		[CssAttribute("color")]
		public ColorRGB Color { get; set; }

		/// <summary>
		/// Specifies the color of the background.
		/// </summary>
		/// <value>The color of the background.</value>
		[CssAttribute("background-color")]
		public ColorRGB BackgroundColor { get; set; }

		/// <summary>
		/// A shorthand property for setting all the padding properties in one declaration
		/// </summary>
		/// <value>The padding.</value>
		[CssAttribute("padding")]
		public float[] Padding { get; set; }

		/// <summary>
		/// Sets the bottom padding of an element
		/// </summary>
		/// <value>The padding bottom.</value>
		[CssAttribute("padding-bottom")]
		public float PaddingBottom { get; set; }

		/// <summary>
		/// Sets the left padding of an element
		/// </summary>
		/// <value>The padding left.</value>
		[CssAttribute("padding-left")]
		public float PaddingLeft { get; set; }

		/// <summary>
		/// Sets the right padding of an element
		/// </summary>
		/// <value>The padding right.</value>
		[CssAttribute("padding-right")]
		public float PaddingRight { get; set; }

		/// <summary>
		/// Sets the top padding of an element
		/// </summary>
		/// <value>The padding top.</value>
		[CssAttribute("padding-top")]
		public float PaddingTop { get; set; }

		#region Custom Properties

		/// <summary>
		/// Stores reference to the raw CSS
		/// </summary>
		/// <value>The raw CS.</value>
		public string RawCSS { get; set; }

		#endregion

		/// <summary>
		/// Name of the Selector
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; private set; }

		public CssParameters(string name)
		{
			Name = name;

			PaddingBottom = float.MinValue;
			PaddingLeft = float.MinValue;
			PaddingRight = float.MinValue;
			PaddingTop = float.MinValue;
			Color = ColorRGB.Empty;
			BackgroundColor = ColorRGB.Empty;
		}

		public virtual void SetValue(string propertyName, object value)
		{

			var myType = this.GetType();
			PropertyInfo myPropInfo = myType.GetRuntimeProperty(propertyName);
			myPropInfo.SetValue(this, value, null);
		}

		public virtual object GetValue(string propertyName)
		{
			var myType = this.GetType();
			PropertyInfo myPropInfo = myType.GetRuntimeProperty(propertyName);
			return myPropInfo.GetValue(this, null);
		}
	}
}


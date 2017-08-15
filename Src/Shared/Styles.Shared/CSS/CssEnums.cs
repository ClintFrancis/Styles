using System;
namespace Styles
{
	/// <summary>
	/// Alignment enum.
	/// </summary>
	public enum CssAlign
	{
		[CssAttribute("left")]
		Left,
		[CssAttribute("right")]
		Right,
		[CssAttribute("center")]
		Center,
		[CssAttribute("justified")]
		Justified
	}

	/// <summary>
	/// Text decoration enum.
	/// </summary>
	public enum CssDecoration
	{
		[CssAttribute("none")]
		None,
		[CssAttribute("underline")]
		Underline,
		[CssAttribute("line-through")]
		LineThrough
	}

	/// <summary>
	/// Text transform enum
	/// </summary>
	public enum CssTextTransform
	{
		[CssAttribute("none")]
		None,
		[CssAttribute("capitalize")]
		Capitalize,
		[CssAttribute("uppercase")]
		UpperCase,
		[CssAttribute("lowercase")]
		LowerCase
	}

	/// <summary>
	/// Text overflow enum
	/// </summary>
	public enum CssTextOverflow
	{
		[CssAttribute("none")]
		None,
		[CssAttribute("clip")]
		Clip,
		[CssAttribute("ellipsis")]
		Ellipsis
	}

	/// <summary>
	/// Font Style enum
	/// </summary>
	public enum CssFontStyle
	{
		[CssAttribute("normal")]
		Normal,
		[CssAttribute("italic")]
		Italic
	}

	/// <summary>
	/// Font Weight enum
	/// </summary>
	public enum CssFontWeight
	{
		[CssAttribute("normal")]
		Normal,
		[CssAttribute("bold")]
		Bold
	}
}


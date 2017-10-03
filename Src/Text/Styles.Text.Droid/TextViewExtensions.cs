using Styles.Text;

namespace Android.Widget
{
	public static class TextViewExtensions
	{
		public static void Style (this TextView target, string cssSelector, string text = null)
		{
			TextStyle.Default.Style<TextView> (target, cssSelector, text);
		}

		public static void Style (this TextView target, TextStyle instance, string cssSelector, string text = null)
		{
			instance.Style<TextView> (target, cssSelector, text);
		}

		public static void Style (this EditText target, string cssSelector, string text = null)
		{
			TextStyle.Default.Style<EditText> (target, cssSelector, text);
		}

		public static void Style (this EditText target, TextStyle instance, string cssSelector, string text = null)
		{
			instance.Style<EditText> (target, cssSelector, text);
		}
	}
}


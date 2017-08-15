using System;
namespace Styles.Color
{
	public static class GradientUtils
	{

		public static IRgb GetColorByOffset(Gradient gradient, double offset)
		{
			//GradientStop[] stops = collection.OrderBy(x => x.Offset).ToArray();
			gradient.Update();

			var colorOffsets = gradient.Offsets;

			// Bookedn the results
			if (offset <= 0) return colorOffsets[0].Color;
			if (offset >= 1) return colorOffsets[gradient.Colors.Length - 1].Color;

			ColorOffset left = null;
			ColorOffset right = null;

			// Figure out how far along the index is
			for (int i = 0; i<colorOffsets.Length; i++)
			{
				if (colorOffsets[i].Offset >= offset)
				{
					right = colorOffsets[i];
					break;
				}
				left = colorOffsets[i];
			}

			offset = Math.Round((offset - left.Offset) / (right.Offset - left.Offset), 2);
			var a = ((right.Color.A - left.Color.A) * offset + left.Color.A);
			var r = ((right.Color.R - left.Color.R) * offset + left.Color.R);
			var g = ((right.Color.G - left.Color.G) * offset + left.Color.G);
			var b = ((right.Color.B - left.Color.B) * offset + left.Color.B);

			return new ColorRGB(r, g, b, a);
		}

		/*
         * http://stackoverflow.com/questions/9650049/get-color-in-specific-location-on-gradient
         * 
		private static Color GetColorByOffset(GradientStopCollection collection, double offset)
		{
			GradientStop[] stops = collection.OrderBy(x => x.Offset).ToArray();
			if (offset <= 0) return stops[0].Color;
			if (offset >= 1) return stops[stops.Length - 1].Color;
			GradientStop left = stops[0], right = null;
			foreach (GradientStop stop in stops)
			{
				if (stop.Offset >= offset)
				{
					right = stop;
					break;
				}
				left = stop;
			}
			Debug.Assert(right != null);
			offset = Math.Round((offset - left.Offset) / (right.Offset - left.Offset), 2);
			byte a = (byte)((right.Color.A - left.Color.A) * offset + left.Color.A);
			byte r = (byte)((right.Color.R - left.Color.R) * offset + left.Color.R);
			byte g = (byte)((right.Color.G - left.Color.G) * offset + left.Color.G);
			byte b = (byte)((right.Color.B - left.Color.B) * offset + left.Color.B);
			return Color.FromArgb(a, r, g, b);
		}
		*/
	}
}

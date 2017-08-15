using System;
using System.Diagnostics;

namespace Styles
{
	public class Bitmap : IDisposable
	{
		public const byte BitsPerComponent = 8;
		public const byte BytesPerPixel = 4;

		public int Width { get; internal set; }
		public int Height { get; internal set; }
		public byte[] BitmapData { get; internal set; }

		public int BytesPerRow
		{
			get
			{
				return Width * BytesPerPixel;
			}
		}

		public Bitmap(int width, int height, byte[] data)
		{
			Width = width;
			Height = height;
			BitmapData = data;
		}

		public ColorRGB[] GetColorArray()
		{
			var count = 0;
			ColorRGB[] imageColors = new ColorRGB[Width * Height];

			for (int x = 0; x < Width; x++)
			{
				for (int y = 0; y < Height; y++)
				{
					var pixel = ((Width * y) + x) * BytesPerPixel;

					var color = ColorRGB.FromRGB(
						BitmapData[pixel + 0],
						BitmapData[pixel + 1],
						BitmapData[pixel + 2]
					);

					imageColors[count] = color;
					count++;
				}
			}

			return imageColors;
		}

		// TODO look for implementation
		public void TransformImage(Func<byte, byte, byte, double> pixelOperation)
		{
			byte r, g, b;

			// Pixel data order is RGBA
			try
			{
				for (int i = 0; i < BitmapData.Length; i += BytesPerPixel)
				{
					r = BitmapData[i];
					g = BitmapData[i + 1];
					b = BitmapData[i + 2];

					// Leave alpha value intact
					BitmapData[i] = BitmapData[i + 1] = BitmapData[i + 2] = (byte)pixelOperation(r, g, b);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}

		public void Dispose()
		{
			BitmapData = null;
		}
	}
}


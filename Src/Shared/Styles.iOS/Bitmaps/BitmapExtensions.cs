using System;
using CoreGraphics;
using UIKit;

namespace Styles
{
	public static class BitmapExtensions
	{
		public static UIImage Resize(this UIImage image, CGSize newSize)
		{
			UIGraphics.BeginImageContextWithOptions(newSize, false, 0);
			UIImage result = null;
			try
			{
				image.Draw(new CGRect(0, 0, newSize.Width, newSize.Height));
				result = UIGraphics.GetImageFromCurrentImageContext();
			}
			catch (Exception ex)
			{
				throw new Exception("UIImageColors.ResizeForUIImageColors failed: UIGraphics.GetImageFromCurrentImageContext returned null");
			}
			finally
			{
				UIGraphics.EndImageContext();
			}

			return result;
		}


		public static UIImage ToNative(this Bitmap bitmap)
		{
			using (var colourSpace = CGColorSpace.CreateDeviceRGB())
			{
				using (var context = new CGBitmapContext(bitmap.BitmapData, bitmap.Width, bitmap.Height, Bitmap.BitsPerComponent, Bitmap.BytesPerPixel * bitmap.Width, colourSpace, CGImageAlphaInfo.PremultipliedLast))
				{
					bitmap.Dispose();
					return UIImage.FromImage(context.ToImage());
				}
			}
		}

		public static Bitmap ToBitmap(this UIImage image)
		{
			return image.ToBitmap(CGSize.Empty);
		}

		public static Bitmap ToBitmap(this UIImage image, CGSize scaledSize)
		{
			if (scaledSize == CGSize.Empty)
			{
				var ratio = image.Size.Width / image.Size.Height;
				var r_width = 250f;
				scaledSize = new CGSize(r_width, r_width / ratio);
			}

			var cgImage = image.Resize(scaledSize).CGImage;
			var width = (int)image.CGImage.Width;
			var height = (int)image.CGImage.Height;

			var pixelData = new byte[width * height * Bitmap.BytesPerPixel];
			using (var colourSpace = CGColorSpace.CreateDeviceRGB())
			{

				using (var context = new CGBitmapContext(pixelData, width, height, Bitmap.BitsPerComponent, Bitmap.BytesPerPixel * width, colourSpace, CGImageAlphaInfo.PremultipliedLast))
				{
					context.DrawImage(new CGRect(0, 0, width, height), image.CGImage);
				}
			}

			return new Bitmap(width, height, pixelData);
		}

	}
}


using System;
namespace Styles
{
	internal static class ConvertHSB
	{
		internal static IHsb ToColorSpace(IRgb color)
		{
			var hsl = ConvertHSL.ToColorSpace(color);
			return FromHSL(hsl);
		}

		internal static IRgb ToColor(IHsb item)
		{
			return ConvertHSL.ToColor(ToHSL(item));
		}

		static IHsl ToHSL(IHsb color)
		{
			double hh, ll, ss;

			hh = color.H;
			ll = (2 - color.S) * color.B;
			ss = color.S * color.B;
			ss = ss / ((ll <= 1) ? (ll) : 2 - (ll));
			if (double.IsNaN(ss))
				ss = 0;
			ll = ll / 2;

			return new ColorHSL(hh, ss, ll, color.A);
		}

		static IHsb FromHSL(IHsl color)
		{
			double h, s, b;
			double hh = color.H;
			double ll = color.L;
			double ss = color.S;

			h = hh;
			ll = ll * 2;
			ss = ss * ((ll <= 1) ? ll : 2 - ll);
			b = (ll + ss) / 2;
			s = (2 * ss) / (ll + ss);
			if (double.IsNaN(s))
				s = 0;

			return new ColorHSB(h, s, b);
		}
	}
}


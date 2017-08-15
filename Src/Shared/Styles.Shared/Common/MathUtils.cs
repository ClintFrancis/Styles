using System;
using System.Diagnostics;

namespace Styles
{
	public class MathUtils
	{
		// v is input value, m is modulus. wrap(a, b) is a replacement for a % b.
		public static double Wrap(double v, double m)
		{
			if (m > 0)
			{
				if (v < 0) v = m - ((-v) % m);              // get negative v into region [0, m)
			}
			else
			{
				m = -m;                                     // the positive value is easier to work with
				if (v < 0) v += m * (1 + (int)(-v / m));    // add m enough times so v > 0
				v = m - (v % m);                            // get v % m, then flip the curve negative
			}

			return v % m; ;                                 // return v % m, now that both are positive
		}
	}
}


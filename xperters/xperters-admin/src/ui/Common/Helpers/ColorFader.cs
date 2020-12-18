using System;
using System.Collections.Generic;
using System.Drawing;

namespace Xperters.Admin.UI.Common.Helpers
{
	public sealed class ColorFader
	{
		private readonly Color _from;
		private readonly Color _to;

		private readonly double _stepR;
		private readonly double _stepG;
		private readonly double _stepB;

		private readonly int _steps;

		public ColorFader(Color from, Color to, int steps = 20)
		{
			if (steps <= 0)
				throw new ArgumentException("steps must be a positive number");

			_from = from;
			_to = to;
			_steps = steps;

			_stepR = (double)(_to.R - _from.R) / _steps;
			_stepG = (double)(_to.G - _from.G) / _steps;
			_stepB = (double)(_to.B - _from.B) / _steps;
		}

		public IEnumerable<Color> Fade()
		{
			for (var i = 0; i < _steps; ++i)
			{
				yield return Color.FromArgb((int)(_from.R + i * _stepR), (int)(_from.G + i * _stepG), (int)(_from.B + i * _stepB));
			}
			yield return _to; // make sure we always return the exact target color last
		}
	}
}

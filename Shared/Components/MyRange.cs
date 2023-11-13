namespace PasswordGeneratorWasm.Shared
{
	public class MyRange
	{
		protected int _min = 0;
		protected int _max = 100;
		protected int _value = 0;
		protected int _step = 1;

		public int Min
		{
			get => _min;
			set
			{
				if (value < _max)
				{
					_min = value;
					if (_value < _min) _value = _min;
				}
			}
		}
		public int Max
		{
			get => _max;
			set
			{
				if (value > _min)
				{
					_max = value;
					if (_value > _max) _value = _max;
				}
			}
		}
		public int Value
		{
			get => _value;
			set
			{
				if (value >= _min && value <= _max)
					_value = value;
			}
		}
		public int Range => _max - _min;
		public int Step
		{
			get => _step;
			set
			{
				if (value < 1)
					_step = 1;
				else
				{
					int[] x = { _value - value, _value + value };
					if (x[0] >= _min && x[1] <=_max)
						_step = value;
					else if (x[0]<_min && x[1]<=_max)
						_step = value;
					else
						_step = value;
				}


				//else if (value > _max / 2)
				//	_step = _max / 2;
				//else if (value > _max)
				//{
				//	_value = _min;
				//	_step = Range;
				//}
				//else
				//	_step = value;
			}
		}
		public MyRange(int min = 0, int max = 100, int value = 0, int step = 1)
		{
			Min = min;
			Max = max;
			Value = value;
			Step = step;
		}
	}
}

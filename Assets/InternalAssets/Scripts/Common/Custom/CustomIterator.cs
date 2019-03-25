using System.Collections.Generic;
using System.Linq;

namespace InternalAssets.Scripts.Common.Custom
{
	public class CustomIterator<T>
	{
		public int Length => _array.Length;
		
		private readonly T[] _array;
		private int _index;

		public CustomIterator(IEnumerable<T> items)
		{
			_array = items.ToArray();
			_index = 0;
		}

		public void Next()
		{
			if (_index + 1 >= _array.Length)
			{
				_index = 0;
				return;
			}

			_index++;
		}

		public void Prev()
		{
			if (_index - 1 < 0)
			{
				_index = _array.Length - 1;
				return;
			}

			_index--;
		}

		public T Current()
		{
			return _array[_index];
		}
	}
}
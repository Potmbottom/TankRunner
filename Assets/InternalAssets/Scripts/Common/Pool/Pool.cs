using System.Collections.Generic;
using UnityEngine;

namespace InternalAssets.Scripts.Common.Pool
{
	public class Pool<T> where T : PoolObject
	{
		private readonly Stack<PoolObject> _pool;
		
		public Pool(T prefab, int size)
		{
			_pool = new Stack<PoolObject>();
			Init(prefab, size);
		}

		public T Get()
		{
			var obj = _pool.Pop();
			obj.Activate();
			return (T)obj;
		}

		public void Return(T value)
		{
			value.Disable();
			_pool.Push(value);
		}

		private void Init(T prefab, int size)
		{
			for (var i = 0; i < size; i++)
			{
				var obj = Object.Instantiate(prefab);
				obj.Disable();
				_pool.Push(obj);
			}
		}
	}
}
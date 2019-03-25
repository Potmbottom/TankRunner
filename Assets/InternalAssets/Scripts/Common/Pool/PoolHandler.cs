using System;
using UnityEngine;

namespace InternalAssets.Scripts.Common.Pool
{
	public class PoolHandler
	{
		public int Index => _index;
		public Type Type => _type;

		private readonly Type _type;
		private readonly Type _prefabType;
		private readonly int _index;
		private readonly Pool<PoolObject> _pool;
		
		public PoolHandler(Type type, int index, PoolObject prefab, int count)
		{
			//Debug.Log("Register " + prefab.GetType().Name);
			
			_pool = new Pool<PoolObject>(prefab, count);
			_index = index;
			_type = type;
			_prefabType = prefab.GetType();
		}

		public void ReturnItem(PoolObject item)
		{
			var type = item.GetType();
			if (type != _prefabType)
			{
				Debug.LogError("Cant back type " + type.Name + " to pool of items " + _prefabType.Name);
				return;
			}
			
			//Debug.Log("Return item " + type.Name + " to pool of items " + _prefabType.Name + " type " + _type.Name + " index " + _index);
			
			_pool.Return(item);
		}
		
		public PoolObject GetItem()
		{
			return _pool.Get();
		}
	}
}
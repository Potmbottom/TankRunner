using System;
using System.Collections.Generic;
using System.Linq;
using Core.Management;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;

namespace InternalAssets.Scripts.Common.Pool
{
	public class PoolManager : IManager
	{
		private List<PoolHandler> _pools;

		public void Init()
		{
			_pools = new List<PoolHandler>();
		}

		public void RegisterPool<T>(int pointer, PoolObject prefab, int count)
		{
			var key = typeof(T);
			var exist = _pools.Any(x => x.Type == key && x.Index == pointer);
			if (exist)
			{
				Debug.Log("Already have registered pool " + key + " " + pointer);
				return;
			}
			
			var handler = new PoolHandler(key, pointer, prefab, count); 
			_pools.Add(handler);
		}

		public PoolObject GetItem<T>(int pointer)			
		{
			var key = typeof(T);
			var handler = GetHandler(key, pointer);
			var item = handler.GetItem();
			return item;
		}

		public void ReturnItem<T>(int pointer, PoolObject item)
		{
			var key = typeof(T);
			var handler = GetHandler(key, pointer);
			handler.ReturnItem(item);
		}

		private PoolHandler GetHandler(Type type, int index)
		{
			var handler = _pools.FirstOrDefault(x => x.Type == type && x.Index == index);

			if (handler != null) return handler;
			
			Debug.LogError("Cant get item " + type.Name + " " + index);
			return null;

		}

		public List<PoolHandler> GetPoolList()
		{
			return _pools;
		}
	}
}
using System;
using UnityEngine;

namespace InternalAssets.Scripts.Extensions
{
	public static class EventExtension
	{
		public static void SafeInvoke<T>(this Action<T> action, T value)
		{
			if(action == null) return;
			
			action.Invoke(value);
		}
		
		public static void SafeInvoke(this Action action)
		{
			if(action == null) return;
			
			action.Invoke();
		}
		
		public static void SafeInvoke<T, T1>(this Action<T, T1> action, T value, T1 value1)
		{
			if(action == null) return;
			
			action.Invoke(value, value1);
		}
	}
}
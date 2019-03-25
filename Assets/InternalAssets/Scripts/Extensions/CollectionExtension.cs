using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;
namespace InternalAssets.Scripts.Extensions
{
	public static class CollectionExtension
	{
		private static Random random = new Random();
     
		public static T GetRandomElement<T>(this IEnumerable<T> list)
		{
			// If there are no elements in the collection, return the default value of T
			if (list.Count() == 0)
				return default(T);
 
			return list.ElementAt(random.Next(list.Count()));
		}
	}
}
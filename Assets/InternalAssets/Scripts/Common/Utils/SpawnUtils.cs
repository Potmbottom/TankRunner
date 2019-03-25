using System.Collections.Generic;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Extensions;
using UnityEngine;

namespace InternalAssets.Scripts.Common.Utils
{
	public static class SpawnUtils
	{
		public static Vector3 GetRandomSpawnPosition(Transform center, int innerRange, int range)
		{
			var area = Random.Range(0, 4);

			switch (area)
			{
				case 0:
					return new Vector3(Random.Range(center.position.x - innerRange, center.position.x + innerRange), center.position.y,
						Random.Range(center.position.z + innerRange, center.position.z + range));
				case 1:
					return new Vector3(Random.Range(center.position.x - innerRange, center.position.x + innerRange), center.position.y,
						Random.Range(center.position.z - innerRange, center.position.z - range));
				case 2:
					return new Vector3(Random.Range(center.position.x + innerRange, center.position.x + range), center.position.y,
						Random.Range(center.position.z - innerRange,center.position.z + innerRange));
				case 3:
					return new Vector3(Random.Range(center.position.x - innerRange, center.position.x - range), center.position.y,
						Random.Range(center.position.z - innerRange,center.position.z + innerRange));
			}
			
			return new Vector3();
		}

		public static EnemyType GetRandomSpawnEnemyType()
		{
			var levelRecord = ScriptableUtils.GetCurrentLevelRecord();
			return levelRecord.Enemy.GetRandomElement();
		}

		public static EnvironmentType GetRandomSpawnEnvironmentType()
		{
			var levelRecord = ScriptableUtils.GetCurrentLevelRecord();
			return levelRecord.Environment.GetRandomElement();
		}
	}
}
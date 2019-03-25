using System.Collections.Generic;
using InternalAssets.Data;
using UnityEngine;

namespace InternalAssets.Resources.Database
{
	[CreateAssetMenu(fileName = "EnemyTable", menuName = "EnemyTable", order = 51)]
	public class EnemyTable : ScriptableObject
	{
		public List<EnemyRecord> Enemy;
	}
}
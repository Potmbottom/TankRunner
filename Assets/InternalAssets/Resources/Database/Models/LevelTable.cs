using System.Collections.Generic;
using InternalAssets.Data;
using UnityEngine;

namespace InternalAssets.Resources.Database
{
	[CreateAssetMenu(fileName = "LevelTable", menuName = "LevelTable", order = 51)]
	public class LevelTable : ScriptableObject
	{
		public List<LevelRecord> LevelRecords;
	}
}
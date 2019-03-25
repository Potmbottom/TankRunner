using System.Collections.Generic;
using InternalAssets.Data;
using UnityEngine;

namespace InternalAssets.Resources.Database
{
	[CreateAssetMenu(fileName = "PlayerTable", menuName = "PlayerTable", order = 51)]
	public class PlayerTable : ScriptableObject
	{
		public List<PlayerRecord> Records;
	}
}
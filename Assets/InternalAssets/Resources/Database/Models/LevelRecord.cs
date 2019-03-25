using System;
using System.Collections.Generic;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;

namespace InternalAssets.Data
{
	//[CreateAssetMenu(fileName = "LevelRecord", menuName = "LevelRecord", order = 51)]
	[Serializable]
	public class LevelRecord
	{
		public int BuildId;
		public PlayerType PlayerType;
		public List<EnemyType> Enemy;
		public List<EnvironmentType> Environment;
		public int EnemyMax;
		public int EnvironmentMax;
		public FollowedFieldView Field;
	}
}
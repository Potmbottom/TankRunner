using System.Collections.Generic;
using System.Linq;
using Core.Management;
using InternalAssets.Resources.Database;
using InternalAssets.Resources.Database.Manager;
using InternalAssets.Resources.Database.Models;
using InternalAssets.Scripts.Common.Enum;
using UnityEngine.SceneManagement;

namespace InternalAssets.Data
{
	public static class ScriptableUtils
	{		
		public static EnemyRecord GetEnemyRecord(EnemyType type)
		{
			var manager = CoreManager.Instance.GetData<DataManager>();
			var table = manager.GetScriptableObjectDictionary<EnemyTable>();
			return table.Enemy.FirstOrDefault(x => x.Type == type);
		}

		public static List<EnemyRecord> GetEnemyRecords()
		{
			var levelRecord = GetCurrentLevelRecord();
			return levelRecord.Enemy.Select(GetEnemyRecord).ToList();
		}
		
		public static List<EnvironmentRecord> GetEnvironmentRecords()
		{
			var levelRecord = GetCurrentLevelRecord();
			return levelRecord.Environment.Select(GetEnvironmentRecord).ToList();
		}

		public static LevelRecord GetCurrentLevelRecord()
		{
			var sceneBuildId = SceneManager.GetActiveScene().buildIndex;
			
			var manager = CoreManager.Instance.GetData<DataManager>();
			var table = manager.GetScriptableObjectDictionary<LevelTable>();
			return table.LevelRecords.FirstOrDefault(x => x.BuildId == sceneBuildId);
		}

		public static PlayerRecord GetCurrentPlayerRecord()
		{
			var level = GetCurrentLevelRecord();
			var player = GetPlayerRecord(level.PlayerType);
			return player;
		}

		public static CommonElementsRecord GetCommonElements()
		{
			var manager = CoreManager.Instance.GetData<DataManager>();
			var table = manager.GetScriptableObjectDictionary<CommonElementsRecord>();
			return table;
		}

		public static List<WeaponRecord> GetWeaponRecords()
		{
			var playerRecord = GetCurrentPlayerRecord();
			return playerRecord.Weapon.Select(GetWeaponRecord).ToList();
		}

		public static WeaponRecord GetWeaponRecord(WeaponType type)
		{
			var manager = CoreManager.Instance.GetData<DataManager>();
			var table = manager.GetScriptableObjectDictionary<WeaponTable>();
			return table.WeaponRecords.FirstOrDefault(x => x.Type == type);
		}

		public static PlayerRecord GetPlayerRecord(PlayerType type)
		{
			var manager = CoreManager.Instance.GetData<DataManager>();
			var table = manager.GetScriptableObjectDictionary<PlayerTable>();
			return table.Records.FirstOrDefault(x => x.Type == type);
		}

		public static EnvironmentRecord GetEnvironmentRecord(EnvironmentType type)
		{
			var manager = CoreManager.Instance.GetData<DataManager>();
			var table = manager.GetScriptableObjectDictionary<EnvironmentTable>();
			return table.Environment.FirstOrDefault(x => x.Type == type);
		}
		
	}
}
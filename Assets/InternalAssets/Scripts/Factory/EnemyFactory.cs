using InternalAssets.Data;
using InternalAssets.Scripts.Common.AiCommand;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.Model;

namespace InternalAssets.Scripts.Factory
{
	public static class EnemyFactory
	{
		public static AbstractEnemyModel GetEnemyModel(EnemyType type, IAiCommand command)
		{
			var record = ScriptableUtils.GetEnemyRecord(type);	
			switch (type)
			{
				case EnemyType.Skeleton:
					return new SmartZombieModel(type, command, record);
				case EnemyType.Zombie:
					return new ZombieModel(type, command, record);
			}

			return null;
		}
	}
}
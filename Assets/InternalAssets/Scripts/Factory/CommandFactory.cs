using InternalAssets.Data;
using InternalAssets.Scripts.Common.AiCommand;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.Factory
{
	public static class CommandFactory
	{
		public static IAiCommand GetAiCommand(EnemyType type)
		{
			var record = ScriptableUtils.GetEnemyRecord(type);
			var cType = record.Command;
			switch (cType)
			{
				case CommandType.TargetFollow:
					return new TargetFollow();
				case CommandType.BlindSearch:
					return new BlindSearch();
			}

			return null;
		}
	}
}
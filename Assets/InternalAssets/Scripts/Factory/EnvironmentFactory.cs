using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.Model;

namespace InternalAssets.Scripts.Factory
{
	public static class EnvironmentFactory
	{
		public static AbstractEnvironmentModel GetEnvironmentModel(EnvironmentType type)
		{
			var record = ScriptableUtils.GetEnvironmentRecord(type);
			switch (type)
			{
				case EnvironmentType.Box:
					return new BoxModel(type, record);
				case EnvironmentType.Tree:
					return new TreeModel(type, record);
			}

			return null;
		}
	}
}
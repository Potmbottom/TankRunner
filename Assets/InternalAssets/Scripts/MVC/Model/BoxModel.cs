using InternalAssets.Resources.Database.Models;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.MVC.Model
{
	public class BoxModel : AbstractEnvironmentModel
	{
		public BoxModel(EnvironmentType type, EnvironmentRecord record)
		{
			Type = type;
			Health = record.Health;
			Armor = record.Armor;
			Damage = record.Damage;
		}
	}
}
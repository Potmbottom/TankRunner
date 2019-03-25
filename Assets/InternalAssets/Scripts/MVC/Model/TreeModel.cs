using InternalAssets.Resources.Database.Models;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.MVC.Model
{
	public class TreeModel : AbstractEnvironmentModel
	{		
		public TreeModel(EnvironmentType type, EnvironmentRecord record)
		{
			Type = type;
			Health = record.Health;
			Armor = record.Armor;
			Damage = record.Damage;
		}
	}
}
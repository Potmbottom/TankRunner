using System;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.View;

namespace InternalAssets.Resources.Database.Models
{
	[Serializable]
	public class EnvironmentRecord
	{
		public int Health;
		public int Damage;
		public int Armor;
		public EnvironmentType Type;
		public CollisionView Prefab;
	}
}
using System;
using InternalAssets.Scripts.Common.Enemy;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.MVC.View;

namespace InternalAssets.Resources.Database.Models
{
	[Serializable]
	public class WeaponRecord
	{
		public WeaponType Type;
		public int Damage;
		public int FireRate;
		public float Cooldown;
		public float Speed;
		public ProjectileView ProjectilePrefab;
	}
}
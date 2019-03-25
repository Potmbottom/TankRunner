using InternalAssets.Resources.Database.Models;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.Common.Weapon
{
	public class Cannon : IWeapon
	{
		private readonly int _damage;
		private readonly int _fireRate;
		private readonly float _cooldown;
		private readonly float _speed;
		private readonly WeaponType _type;

		public Cannon(WeaponType type, WeaponRecord record)
		{
			_damage = record.Damage;
			_fireRate = record.FireRate;
			_type = type;
			_cooldown = record.Cooldown;
			_speed = record.Speed;
		}

		public float Cooldown()
		{
			return _cooldown;
		}

		public float ProjectileSpeed()
		{
			return _speed;
		}

		public int GetDamage()
		{
			return _damage;
		}

		public int GetFireRate()
		{
			return _fireRate;
		}

		public WeaponType GetWeaponType()
		{
			return _type;
		}
	}
}
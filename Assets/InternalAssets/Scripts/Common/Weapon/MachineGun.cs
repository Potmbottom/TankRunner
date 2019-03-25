using InternalAssets.Resources.Database.Models;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.Common.Weapon
{
	public class MachineGun : IWeapon
	{
		private readonly int _damage;
		private readonly int _fireRate;
		private readonly float _cooldown;
		private readonly float _speed;
		private readonly WeaponType _type;

		public MachineGun(WeaponType type, WeaponRecord record)
		{
			_damage = record.Damage;
			_fireRate = record.FireRate;
			_cooldown = record.Cooldown;
			_speed = record.Speed;
			_type = type;
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
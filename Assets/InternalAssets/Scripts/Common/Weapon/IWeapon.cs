using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.Common.Weapon
{
	public interface IWeapon
	{
		int GetDamage();
		int GetFireRate();
		float Cooldown();
		float ProjectileSpeed();
		WeaponType GetWeaponType();
	}
}
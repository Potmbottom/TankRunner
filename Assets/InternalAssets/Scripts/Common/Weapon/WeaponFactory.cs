using System.Collections.Generic;
using System.Linq;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enum;

namespace InternalAssets.Scripts.Common.Weapon
{
	public static class WeaponFactory
	{
		public static IWeapon GetWeapon(WeaponType type)
		{
			var record = ScriptableUtils.GetWeaponRecord(type);
			switch (type)
			{
				case WeaponType.Cannon:
					return new Cannon(type, record);
				case WeaponType.MachineGun:
					return new MachineGun(type, record);
			}

			return null;
		}

		public static List<IWeapon> GetList(IEnumerable<WeaponType> types)
		{
			return types.Select(GetWeapon).ToList();
		}
	}
}
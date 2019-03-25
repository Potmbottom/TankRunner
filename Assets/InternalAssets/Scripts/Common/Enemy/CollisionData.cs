using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Common.Pool;

namespace InternalAssets.Scripts.Common.Enemy
{
	public class CollisionData : PoolObject
	{
		public int Damage;
		public WeaponType Type;
	}
}
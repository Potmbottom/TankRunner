using UnityEngine;

namespace InternalAssets.Scripts.Common.Pool
{
	public class PoolObject : MonoBehaviour
	{
		public virtual void Activate()
		{
			gameObject.SetActive(true);
		}

		public virtual void Disable()
		{
			gameObject.SetActive(false);
		}
	}
}
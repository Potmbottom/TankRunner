using UnityEngine;

namespace InternalAssets.Scripts.Common.Camera
{
	public class CameraFollower : MonoBehaviour
	{
		private Transform _target;
		
		private void Update()
		{
			if(_target == null) return;

			transform.position = _target.transform.position;
		}

		public void Init(Transform target)
		{
			_target = target;
		}
		
	}
}
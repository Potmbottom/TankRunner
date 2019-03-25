using InternalAssets.Scripts.Common.Pool;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.View
{
	public class SkeletonView : CollisionView
	{
		private Rigidbody _rigidbody;
		private MeshRenderer _renderer;

		private float _time;
		
		private void Start()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_renderer = GetComponent<MeshRenderer>();
		}
		
		private void Update()
		{
			if (_rigidbody != null && _rigidbody.velocity != Vector3.zero)
			{
				transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);	
			} 
			
			if (!_renderer.isVisible)
			{
				_time += Time.deltaTime;
				if(_time > 3)
				InvokeOutOfCamera();
			}
			else
			{
				_time = 0;
			}
		}

		public override void Dispose()
		{
			_time = 0;
		}
	}
}
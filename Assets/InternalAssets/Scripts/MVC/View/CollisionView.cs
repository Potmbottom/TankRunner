using System;
using InternalAssets.Scripts.Common.Enemy;
using InternalAssets.Scripts.Common.Pool;
using InternalAssets.Scripts.Extensions;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.View
{
	[RequireComponent(typeof(Collider))]
	public class CollisionView : PoolObject
	{
		public event Action<CollisionData> ProjectileCollision;
		public event Action PlayerCollision;
		public event Action PlayerCollisionBegin;
		public event Action OutOfCamera;

		public void InvokeOutOfCamera()
		{
			OutOfCamera.SafeInvoke();
		}
		
		private void OnCollisionEnter(Collision collision)
		{		
			if (collision.gameObject.CompareTag("projectile"))
			{
				var data = collision.gameObject.GetComponent<CollisionData>();
				ProjectileCollision.SafeInvoke(data);
			}
			
			if (collision.gameObject.CompareTag("Player"))
			{
				PlayerCollisionBegin.SafeInvoke();
			};
		}

		private void OnCollisionStay(Collision other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				PlayerCollision.SafeInvoke();
			};
		}

		public virtual void Dispose(){}
	}
}
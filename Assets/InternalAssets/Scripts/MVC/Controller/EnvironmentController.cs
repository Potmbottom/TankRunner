using System;
using InternalAssets.Scripts.Common.Enemy;
using InternalAssets.Scripts.Extensions;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using UniRx;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class EnvironmentController : UnitController
	{
		public event Action<AbstractEnvironmentModel> Die;
		public event Action<AbstractEnvironmentModel> PlayerCollision;
		
		public readonly AbstractEnvironmentModel Model;
		public readonly CollisionView View;
		
		public EnvironmentController(AbstractEnvironmentModel model, CollisionView view) : base(model)
		{
			Model = model;
			View = view;
			
			Subscribe();
		}
		
		public void Destroy()
		{
			View.ProjectileCollision -= OnProjectileCollision;
			View.PlayerCollision -= OnPlayerCollision;
			View.OutOfCamera -= OnCameraOutOfView;
			View.Dispose();
		}

		private void Subscribe()
		{
			View.ProjectileCollision -= OnProjectileCollision;
			View.ProjectileCollision += OnProjectileCollision;
			
			View.PlayerCollisionBegin -= OnPlayerCollision;
			View.PlayerCollisionBegin += OnPlayerCollision;
			
			View.OutOfCamera -= OnCameraOutOfView;
			View.OutOfCamera += OnCameraOutOfView;
		}

		private void OnCameraOutOfView()
		{
			Die.SafeInvoke(Model);
		}

		private void OnPlayerCollision()
		{
			HitPlayer();
		}

		private void HitPlayer()
		{
			PlayerCollision.SafeInvoke(Model);
		}

		private void OnProjectileCollision(CollisionData data)
		{
			var alive = TakeDamage(data.Damage);
			if(alive) return;
			
			Die.SafeInvoke(Model);
		}
	}
}
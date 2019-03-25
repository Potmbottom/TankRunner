using System;
using Common.EventsSystem;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.AiCommand;
using InternalAssets.Scripts.Common.Enemy;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Extensions;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using UniRx;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class EnemyController : UnitController
	{
		public event Action<AbstractEnemyModel> PlayerCollision;
		public event Action<AbstractEnemyModel> Die;
		
		public readonly AbstractEnemyModel Model;
		public readonly CollisionView View;
		
		private readonly CommandExecutor _executor;
		private readonly MoveComponent _moveComponent;

		private bool _cooldown;
		
		public EnemyController(AbstractEnemyModel model, CollisionView view, Transform player, Rigidbody self) : base(model)
		{
			Model = model;
			View = view;
			
			_moveComponent = new MoveComponent(model);
			_moveComponent.Init(self);
			_executor = new CommandExecutor(model, _moveComponent, player, self);
			_executor.Execute();
			Subscribe();
		}

		public void Destroy()
		{
			_executor.Dispose();
			
			View.GetComponent<Rigidbody>().velocity = Vector3.zero;
			View.ProjectileCollision -= OnProjectileCollision;
			View.PlayerCollision -= OnPlayerCollision;
			View.OutOfCamera -= OnCameraOutOfView;
			View.Dispose();
		}

		private void Subscribe()
		{
			View.ProjectileCollision -= OnProjectileCollision;
			View.ProjectileCollision += OnProjectileCollision;
			
			View.PlayerCollision -= OnPlayerCollision;
			View.PlayerCollision += OnPlayerCollision;
			
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
			if(_cooldown) return;

			_cooldown = true;

			Observable.Timer(TimeSpan.FromSeconds(Model.Cooldown)).Subscribe(_ => { _cooldown = false; });
			PlayerCollision.SafeInvoke(Model);
		}

		private void OnProjectileCollision(CollisionData data)
		{
			var alive = TakeDamage(data.Damage);
			if(alive) return;

			var value = PlayerPrefs.GetInt("KillCount", 0); 
			PlayerPrefs.SetInt("KillCount", ++value);
			EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.EnemyDie);
			
			Die.SafeInvoke(Model);
		}
	}
}
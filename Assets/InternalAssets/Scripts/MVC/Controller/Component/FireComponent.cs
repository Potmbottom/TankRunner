using System;
using System.Collections.Generic;
using Common.EventsSystem;
using Core.Management;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enemy;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Common.Pool;
using InternalAssets.Scripts.Common.Weapon;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class FireComponent
	{
		private readonly AbstractObjectModel _model;

		private readonly Dictionary<WeaponType, bool> _cooldown;
		private readonly Dictionary<WeaponType, int> _fireRate;

		private readonly List<ProjectileView> _projectiles;
		private readonly CompositeDisposable _disposable;

		public FireComponent(AbstractObjectModel model)
		{
			_model = model;
			_projectiles = new List<ProjectileView>();
			_cooldown = new Dictionary<WeaponType, bool>();
			_fireRate = new Dictionary<WeaponType, int>();
			_disposable = new CompositeDisposable();
		}

		public void Dispose()
		{
			_disposable.Dispose();
			_projectiles.ForEach(UnsubscribeProjectile);
		}

		public void Fire(IWeapon weapon, PlayerView view)
		{
			if(!CanFire(weapon)) return;
			
			Cooldown(weapon.GetWeaponType(), weapon.Cooldown());
			
			switch (weapon.GetWeaponType())
			{
				case WeaponType.Cannon:
					CannonFire(weapon, view);
					break;
				case WeaponType.MachineGun:
					MachineGunFire(weapon, view);
					break;
			}
			
			EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.AmmoChange, weapon.GetWeaponType(), _fireRate[weapon.GetWeaponType()]);
		}

		private void Cooldown(WeaponType type, float time)
		{
			_cooldown[type] = true;
			Observable.Timer(TimeSpan.FromSeconds(time))
				.Subscribe(_ => { _cooldown[type] = false; });
		}

		private bool CanFire(IWeapon weapon)
		{
			var coldwn = HaveCooldown(weapon.GetWeaponType());
			var firerate = FireRate(weapon);
			return !coldwn && firerate;
		}

		private bool HaveCooldown(WeaponType type)
		{
			return _cooldown.ContainsKey(type) && _cooldown[type];
		}

		private bool FireRate(IWeapon weapon)
		{
			if (!_fireRate.ContainsKey(weapon.GetWeaponType())) return true;
			
			var value = _fireRate[weapon.GetWeaponType()];
			
//			Debug.Log("Current fire rate " + value);
			
			return value < weapon.GetFireRate();
		}
		
		private void CannonFire(IWeapon weapon, PlayerView view)
		{
			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			var projectile = (ProjectileView)poolManager.GetItem<WeaponType>((int) weapon.GetWeaponType());
			projectile.transform.position = view.CannonPivot.position;
			var direction = view.transform.forward;
			
			view.Cannon.Play();
			
			LaunchProjectile(projectile, direction, weapon, weapon.ProjectileSpeed());
		}
		
		private void MachineGunFire(IWeapon weapon, PlayerView view)
		{
			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			var projectile1 = (ProjectileView)poolManager.GetItem<WeaponType>((int) weapon.GetWeaponType());
			
			var pivot = Random.Range(0, 2);
			projectile1.transform.position = pivot == 0 ? view.MachineGunPivot1.position : view.MachineGunPivot2.position;
			
			var direction = view.transform.forward;
			
			view.MachineGun1.Play();
			view.MachineGun2.Play();
			
			LaunchProjectile(projectile1, direction, weapon, weapon.ProjectileSpeed());
		}

		private void LaunchProjectile(ProjectileView view, Vector3 direction, IWeapon weapon, float force)
		{
			view.Init(weapon.GetWeaponType());
			SubscribeProjectile(view);
			var rigid = view.GetRigidbody();

			rigid.AddForce(direction * force, ForceMode.Impulse);
			var data = view.gameObject.AddComponent<CollisionData>();
			data.Type = weapon.GetWeaponType();
			data.Damage = weapon.GetDamage();
			view.StartTimer();
			IncreaseFireRateValue(weapon.GetWeaponType());
			
			_projectiles.Add(view);
		}

		private void IncreaseFireRateValue(WeaponType type)
		{
			if (!_fireRate.ContainsKey(type))
			{
				_fireRate.Add(type, 1);
				return;
			}

			_fireRate[type]++;
		}

		private void SubscribeProjectile(ProjectileView view)
		{
			view.OnTimeEnd -= OnTimeEnd;
			view.OnTimeEnd += OnTimeEnd;
			view.EnemyCollision -= OnEnemyCollision;
			view.EnemyCollision += OnEnemyCollision;
		}

		private void UnsubscribeProjectile(ProjectileView view)
		{
			view.OnTimeEnd -= OnTimeEnd;
			view.EnemyCollision -= OnEnemyCollision;
		}

		private void OnEnemyCollision(ProjectileView view, WeaponType type)
		{
			ReturnProjectile(view, type);
		}

		private void OnTimeEnd(ProjectileView view, WeaponType type)
		{
			ReturnProjectile(view, type);
		}

		private void ReturnProjectile(ProjectileView view, WeaponType type)
		{
			UnsubscribeProjectile(view);
			view.GetRigidbody().velocity = Vector3.zero;
			view.Hide();
			Observable.Timer(TimeSpan.FromSeconds(0.5f)).Subscribe(_ =>
			{
				var poolManager = CoreManager.Instance.GetData<PoolManager>();
				poolManager.ReturnItem<WeaponType>((int)type, view);
				_fireRate[type]--;
			
				EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.AmmoChange, type, _fireRate[type]);	
			}).AddTo(_disposable);
		}
	}
}
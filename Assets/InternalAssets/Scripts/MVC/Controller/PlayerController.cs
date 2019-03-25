using System;
using Common.EventsSystem;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Camera;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Extensions;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class PlayerController : UnitController
	{
		public event Action<AbstractObjectModel> Die;
		
		private readonly FireComponent _fireComponent;
		private readonly PlayerModel _model;
		private CameraFollower _cameraFollower;
		
		private PlayerView _view;
		
		public PlayerController(PlayerModel model) : base(model)
		{
			_fireComponent = new FireComponent(model);
			_model = model;			
			InitPlayer();
		}

		public void Dispose()
		{
			_fireComponent.Dispose();
		}

		private void InitPlayer()
		{
			var playerRecord = ScriptableUtils.GetCurrentPlayerRecord();
			var commonRecord = ScriptableUtils.GetCommonElements();
			
			_view = Object.Instantiate(playerRecord.PlayerPrefab);
			_view.ModelUpdate(_model);
			var rigidbody = _view.GetComponent<Rigidbody>();
			InitMoveComponent(rigidbody);
			
			_cameraFollower = Object.Instantiate(commonRecord.Camera);
			_cameraFollower.Init(_view.transform);
			
			EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.WeaponSwap, _model.CurrentWeapon());
		}

		public void ApplyDamage(AbstractObjectModel model)
		{
			var alive = TakeDamage(model.GetDamage());

			EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.HpChange, _model.Health);
			
			if (!alive)
			{				
				var killCount = PlayerPrefs.GetInt("KillCount");
				var name = model.GetName();
				
				Debug.Log("Dispatch " + killCount + " " + name);
				
				EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.PlayerDie, name, killCount);
				
				Die.SafeInvoke(model);
			}
		}

		public void NextWeapon()
		{
			_model.NextWeapon();
			EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.WeaponSwap, _model.CurrentWeapon());
		}
		
		public void PrevWeapon()
		{
			_model.PrevWeapon();
			EventHolder<GameEventType>.Dispatcher.Broadcast(GameEventType.WeaponSwap, _model.CurrentWeapon());
		}

		public void Fire()
		{
			_fireComponent.Fire(_model.CurrentWeapon(), _view);
		}

		public PlayerView GetView()
		{
			return _view;
		}
		
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using Core.Management;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enemy;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Common.Pool;
using InternalAssets.Scripts.Common.Utils;
using InternalAssets.Scripts.Extensions;
using InternalAssets.Scripts.Factory;
using InternalAssets.Scripts.MVC.Controller;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using UniRx;
using UnityEngine;

namespace InternalAssets.Scripts.CommonManagers
{
	public class EnemySpawnManager : IDisposableManager
	{
		public event Action<AbstractEnemyModel> PlayerCollision;
	
		private readonly PoolManager _poolManager;
		private readonly List<EnemyController> _enemy;
		private readonly Transform _player;
		private readonly int _max;

		private CompositeDisposable _disposable;
		
		private int _count;
		
		public EnemySpawnManager(Transform playerTransform)
		{
			_disposable = new CompositeDisposable();
			_player = playerTransform;
			_enemy = new List<EnemyController>();
			_poolManager = new PoolManager();
			_max = ScriptableUtils.GetCurrentLevelRecord().EnemyMax;
		}

		public void Dispose()
		{
			_enemy.ForEach(x => x.Destroy());
			_disposable.Dispose();
		}
		
		public  void StartSpawn()
		{
			var common = ScriptableUtils.GetCommonElements();
			Observable.Timer(TimeSpan.FromSeconds(common.EnemySpawnTime))
				.Repeat() 
				.Subscribe(_ =>
				{
					if(_count >= _max) return;
					SpawnSingle();
				}).AddTo(_disposable);
		}

		private void SpawnSingle()
		{
			var type = SpawnUtils.GetRandomSpawnEnemyType();
			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			var common = ScriptableUtils.GetCommonElements();
			var view = (CollisionView)poolManager.GetItem<EnemyType>((int)type);

			var command = CommandFactory.GetAiCommand(type);
			var model = EnemyFactory.GetEnemyModel(type, command);
			var controller = new EnemyController(model, view, _player.transform, view.GetComponent<Rigidbody>());

			var position =
				SpawnUtils.GetRandomSpawnPosition(_player.transform, common.SpawnInnerRange, common.SpawnOutRange);
			view.transform.position = position;

			controller.PlayerCollision -= OnPlayerCollision;
			controller.PlayerCollision += OnPlayerCollision;
				
			controller.Die -= OnEnemyDie;
			controller.Die += OnEnemyDie;
				
			_enemy.Add(controller);
			_count++;
		}
		
		private void DestroyEnemy(AbstractEnemyModel model)
		{
			var controller = GetEnemyController(model);
			controller.PlayerCollision -= OnPlayerCollision;
			controller.Die -= OnEnemyDie;
			controller.Destroy();

			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			poolManager.ReturnItem<EnemyType>((int)controller.Model.EnemyType, controller.View);

			_count--;
		}
		
		private EnemyController GetEnemyController(AbstractEnemyModel model)
		{
			return _enemy.FirstOrDefault(x => x.Model == model);
		}
		
		private void OnPlayerCollision(AbstractEnemyModel model)
		{
			PlayerCollision.SafeInvoke(model);
		}

		private void OnEnemyDie(AbstractEnemyModel model)
		{
			DestroyEnemy(model);
		}
	}
}
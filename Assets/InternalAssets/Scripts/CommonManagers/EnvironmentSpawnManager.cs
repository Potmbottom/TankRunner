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

namespace InternalAssets.Scripts.CommonManagers
{
	public class EnvironmentSpawnManager : IDisposableManager
	{
		public event Action<AbstractEnvironmentModel> PlayerCollision;
	
		private readonly PoolManager _poolManager;
		private readonly List<EnvironmentController> _environment;
		private readonly int _max;

		private readonly PlayerView _player;
		
		private int _count;

		private CompositeDisposable _disposable;
		
		public EnvironmentSpawnManager(PlayerView player)
		{
			_player = player;
			_environment = new List<EnvironmentController>();
			_poolManager = new PoolManager();
			_max = ScriptableUtils.GetCurrentLevelRecord().EnvironmentMax;
			_disposable = new CompositeDisposable();
		}
		
		public  void StartSpawn()
		{
			var common = ScriptableUtils.GetCommonElements();
			Observable.Timer(TimeSpan.FromSeconds(common.EnvironmentSpawnTime))
				.Repeat() 
				.Subscribe(_ =>
				{
					if(_count >= _max) return;
					SpawnSingle();
				}).AddTo(_disposable);
		}

		private void SpawnSingle()
		{
			var type = SpawnUtils.GetRandomSpawnEnvironmentType();
			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			var common = ScriptableUtils.GetCommonElements();
			var view = (CollisionView)poolManager.GetItem<EnvironmentType>((int)type);

			var model = EnvironmentFactory.GetEnvironmentModel(type);
			var controller = new EnvironmentController(model, view);

			var position =
				SpawnUtils.GetRandomSpawnPosition(_player.transform, common.SpawnInnerRange, common.SpawnOutRange);
			view.transform.position = position;

			controller.PlayerCollision -= OnPlayerCollision;
			controller.PlayerCollision += OnPlayerCollision;
				
			controller.Die -= OnEnemyDie;
			controller.Die += OnEnemyDie;
				
			_environment.Add(controller);
			_count++;
		}
		
		private void DestroyEnemy(AbstractEnvironmentModel model)
		{
			var controller = GetController(model);
			controller.PlayerCollision -= OnPlayerCollision;
			controller.Die -= OnEnemyDie;
			controller.Destroy();

			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			poolManager.ReturnItem<EnvironmentType>((int)controller.Model.Type, controller.View);

			_count--;
		}
		
		private EnvironmentController GetController(AbstractEnvironmentModel model)
		{
			return _environment.FirstOrDefault(x => x.Model == model);
		}
		
		private void OnPlayerCollision(AbstractEnvironmentModel model)
		{
			PlayerCollision.SafeInvoke(model);
		}

		private void OnEnemyDie(AbstractEnvironmentModel model)
		{
			DestroyEnemy(model);
		}

		public void Dispose()
		{
			_disposable.Dispose();
			_environment.ForEach(x => x.Destroy());
		}
	}
}
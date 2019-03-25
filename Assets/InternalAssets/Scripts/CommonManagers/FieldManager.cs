using System;
using System.Collections.Generic;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enemy;
using InternalAssets.Scripts.Extensions;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using Object = UnityEngine.Object;

namespace InternalAssets.Scripts.CommonManagers
{
	public class FieldManager
	{
		public event Action<AbstractObjectModel> PlayerCollision;
		
		private readonly PlayerView _player;
		private readonly EnemySpawnManager _spawnManager;
		private readonly EnvironmentSpawnManager _environmentSpawnManager;
		private readonly RestrictionsSpawnManager _restrictionsSpawnManager;
		
		private FollowedFieldView _followedField;

		private List<IDisposableManager> _disposables;
		
		public FieldManager(PlayerView player)
		{
			_disposables = new List<IDisposableManager>();
			
			_player = player;
			_spawnManager = new EnemySpawnManager(_player.transform);
			_spawnManager.PlayerCollision -= OnPlayerCollision;
			_spawnManager.PlayerCollision += OnPlayerCollision;
			_spawnManager.StartSpawn();
			
			_environmentSpawnManager = new EnvironmentSpawnManager(_player);
			_environmentSpawnManager.PlayerCollision -= OnPlayerCollision;
			_environmentSpawnManager.PlayerCollision += OnPlayerCollision;
			_environmentSpawnManager.StartSpawn();
			
			_restrictionsSpawnManager = new RestrictionsSpawnManager(_player.transform.position);
			_restrictionsSpawnManager.Spawn();

			_disposables.Add(_spawnManager);
			_disposables.Add(_environmentSpawnManager);
				
			CreateFollowedField();
		}

		public void Dispose()
		{
			_disposables.ForEach(x => x.Dispose());
		}

		private void CreateFollowedField()
		{
			var levelRecord = ScriptableUtils.GetCurrentLevelRecord();
			_followedField = Object.Instantiate(levelRecord.Field);
			_followedField.SetPlayer(_player);
		}

		private void OnPlayerCollision(AbstractEnemyModel model)
		{
			PlayerCollision.SafeInvoke(model);
		}
		
		private void OnPlayerCollision(AbstractEnvironmentModel model)
		{
			PlayerCollision.SafeInvoke(model);
		}

		private void OnEnemyDie(AbstractEnemyModel model)
		{
			
		}
	}
}
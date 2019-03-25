using Core.Management;
using InternalAssets.Data;
using InternalAssets.Resources.Database.Manager;
using InternalAssets.Scripts.Common;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.Common.Pool;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class LevelController
	{
		private readonly LevelModel _container;
		private GameMediator _mediator;
		
		public LevelController(LevelModel container)
		{
			_container = container;
		}

		public void BuildLevel()
		{
			InitPlayerPrefs();
			InitManagers();
			InitPools();
			InitMediator();
		}

		private void InitMediator()
		{
			_mediator = new GameMediator(_container);
		}

		private void InitManagers()
		{
			var core = CoreManager.Instance;
			core.Init<PoolManager>();
			core.Init<DataManager>();
		}

		private void InitPools()
		{
			InitEnemy();
			InitEnvironment();
			InitProjectile();
		}

		private void InitEnemy()
		{
			var level = ScriptableUtils.GetCurrentLevelRecord();
			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			var enemy = ScriptableUtils.GetEnemyRecords();
			foreach (var item in enemy)
			{
				poolManager.RegisterPool<EnemyType>((int)item.Type, item.Prefab, level.EnemyMax);
			}
		}
		
		private void InitEnvironment()
		{
			var level = ScriptableUtils.GetCurrentLevelRecord();
			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			var environment = ScriptableUtils.GetEnvironmentRecords();
			foreach (var item in environment)
			{
				poolManager.RegisterPool<EnvironmentType>((int)item.Type, item.Prefab, level.EnvironmentMax);
			}
		}

		private void InitProjectile()
		{
			var poolManager = CoreManager.Instance.GetData<PoolManager>();
			var weapons = ScriptableUtils.GetWeaponRecords();
			foreach (var item in weapons)
			{
				poolManager.RegisterPool<WeaponType>((int)item.Type, item.ProjectilePrefab, item.FireRate);
			}
		}

		private void InitPlayerPrefs()
		{
			PlayerPrefs.DeleteAll();
		}
	}
}
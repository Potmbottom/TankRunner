using Common.EventsSystem;
using Core.Management;
using InternalAssets.Data;
using InternalAssets.Scripts.Common.Enum;
using InternalAssets.Scripts.CommonManagers;
using InternalAssets.Scripts.MVC.Controller;
using InternalAssets.Scripts.MVC.Model;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;

namespace InternalAssets.Scripts.Common
{
	public class GameMediator
	{
		private readonly InputController _inputController;
		private readonly FieldManager _fieldManager;
		private readonly PlayerController _playerController;
		
		public GameMediator(LevelModel container)
		{
			_inputController = new InputController(container.Input);

			var playerRecord = ScriptableUtils.GetCurrentPlayerRecord();
			var playerModel = new PlayerModel(playerRecord);
			_playerController = new PlayerController(playerModel);
			var player = _playerController.GetView();
			_fieldManager = new FieldManager(player);
			
			Subscribe();
		}

		private void Dispose()
		{
			Unsubscribe();
			_fieldManager.Dispose();
			_playerController.Dispose();
		}

		private void Subscribe()
		{
			_inputController.Move += OnMoveInput;
			_inputController.Rotate += OnRotateInput;
			_inputController.NextWeapon += OnNextWeaponInput;
			_inputController.PrevWeapon += OnPrevWeaponInput;
			_inputController.Fire += OnFireInput;

			_fieldManager.PlayerCollision += OnEnemyCollision;
			_playerController.Die += OnPlayerDie;
		}

		private void Unsubscribe()
		{
			_inputController.Move -= OnMoveInput;
			_inputController.Rotate -= OnRotateInput;
			_inputController.NextWeapon -= OnNextWeaponInput;
			_inputController.PrevWeapon -= OnPrevWeaponInput;
			_inputController.Fire -= OnFireInput;
			_fieldManager.PlayerCollision -= OnEnemyCollision;
			_playerController.Die -= OnPlayerDie;
		}

		private void OnPlayerDie(AbstractObjectModel model)
		{
			Dispose();
		}

		private void OnMoveInput(float value)
		{
			_playerController.Move(value);
		}

		private void OnRotateInput(Vector3 vec, float value)
		{
			_playerController.Rotate(vec, value);
		}

		private void OnFireInput()
		{
			_playerController.Fire();
		}

		private void OnNextWeaponInput()
		{
			_playerController.NextWeapon();
		}
		
		private void OnPrevWeaponInput()
		{
			_playerController.PrevWeapon();
		}

		private void OnEnemyCollision(AbstractObjectModel model)
		{
			_playerController.ApplyDamage(model);
		}
	}
}
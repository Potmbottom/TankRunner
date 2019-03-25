using InternalAssets.Scripts.MVC.Controller;
using InternalAssets.Scripts.MVC.Model;
using UnityEngine;

namespace InternalAssets.Scripts.Common.AiCommand
{
	public class CommandExecutor
	{
		private readonly AbstractEnemyModel _model;
		private readonly Transform _player;
		private readonly Rigidbody _self;
		private readonly MoveComponent _move;

		private IAiCommand _command;
		
		public CommandExecutor(AbstractEnemyModel model, MoveComponent move, Transform player, Rigidbody self)
		{
			_model = model;
			_player = player;
			_self = self;
			_move = move;
		}

		public void Execute()
		{
			_command = _model.Command;
			_command.End -= OnCommandFinish;
			_command.End += OnCommandFinish;
			
			_command.Init(_player, _self);
			_command.Execute(_move);
		}

		private void OnCommandFinish()
		{
			Execute();
		}

		public void Dispose()
		{
			_command.End -= OnCommandFinish;
			_command.Dispose();
		}
	}
}
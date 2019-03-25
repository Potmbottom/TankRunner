using System;
using InternalAssets.Scripts.MVC.Controller;
using UniRx;
using UnityEngine;

namespace InternalAssets.Scripts.Common.AiCommand
{
	public class TargetFollow : IAiCommand
	{
		public event Action End;

		private Transform _player;
		private Rigidbody _self;
		private MoveComponent _move;

		private CompositeDisposable _disposable;
		
		public TargetFollow()
		{
			_disposable = new CompositeDisposable();
		}

		public void Init(Transform player, Rigidbody self)
		{
			_self = self;
			_player = player;
		}

		public void Dispose()
		{	
			_disposable.Dispose();
		}

		public void Execute(MoveComponent moveComponent)
		{
			_move = moveComponent;
			
			Observable.Timer(TimeSpan.FromSeconds(0.1)).Repeat().Subscribe(_ =>
			{
				var direction = (_player.transform.position - _self.transform.position).normalized;
				_move.MoveInDirection(direction);
			}).AddTo(_disposable);
		}
		
		

	}
}
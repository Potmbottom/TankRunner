using System;
using InternalAssets.Scripts.MVC.Controller;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace InternalAssets.Scripts.Common.AiCommand
{
	public class BlindSearch : IAiCommand
	{
		public event Action End;

		private Transform _player;
		private Rigidbody _self;
		private MoveComponent _move;
		
		private CompositeDisposable _disposable;
		
		public BlindSearch()
		{
			_disposable = new CompositeDisposable();
		}

		public void Init(Transform player, Rigidbody self)
		{
			_self = self;
			_player = player;
			
			_disposable = new CompositeDisposable();
		}

		public void Dispose()
		{
			_disposable.Dispose();
		}

		public void Execute(MoveComponent moveComponent)
		{
			_move = moveComponent;
			
			Observable.Timer(TimeSpan.FromSeconds(1)).Repeat().Subscribe(_ =>
			{
				var direction = Random.onUnitSphere;
				direction.y = 0f;
				_move.MoveInDirection(direction);
			}).AddTo(_disposable);
		}
	}
}
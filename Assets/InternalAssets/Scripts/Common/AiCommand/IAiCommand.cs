using System;
using InternalAssets.Scripts.MVC.Controller;
using UnityEngine;

namespace InternalAssets.Scripts.Common.AiCommand
{
	public interface IAiCommand
	{
		event Action End;
		void Execute(MoveComponent move);
		void Init(Transform player, Rigidbody self);
		void Dispose();
	}
}
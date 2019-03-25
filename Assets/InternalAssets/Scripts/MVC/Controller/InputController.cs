using System;
using InternalAssets.Scripts.Extensions;
using InternalAssets.Scripts.MVC.View;
using UnityEngine;

namespace InternalAssets.Scripts.MVC.Controller
{
	public class InputController
	{
		private readonly InputView _view;

		public event Action<float> Move;
		public event Action<Vector3, float> Rotate;
		public event Action Fire;
		public event Action NextWeapon;
		public event Action PrevWeapon;
		
		public InputController(InputView view)
		{
			_view = view;
			
			Subscribe();
		}

		private void Subscribe()
		{
			_view.HorizontalAxis += OnHorizontalAxis;
			_view.VerticalAxis += OnVerticalAxis;
			_view.Fire += OnFire;
			_view.WeaponChange += OnChangeWeapon;
		}

		private void OnHorizontalAxis(float value)
		{
			var direction = value > 0 ? Vector3.up : Vector3.down; 
			Rotate.SafeInvoke(direction, Math.Abs(value));
		}
		
		private void OnVerticalAxis(float value)
		{			
			Move.SafeInvoke(value);
		}
		
		private void OnChangeWeapon(float value)
		{
			Debug.Log("OnChangeWeapon " + value);
			if (value > 0)
			{
				NextWeapon.SafeInvoke();
				return;
			}
			
			PrevWeapon.SafeInvoke();
		}
		
		private void OnFire()
		{
			Fire.SafeInvoke();
		}
	}
}